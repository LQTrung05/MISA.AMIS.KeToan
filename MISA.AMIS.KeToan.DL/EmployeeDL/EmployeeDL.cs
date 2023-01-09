using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public bool DeleteRecords(List<Guid> listEmployeeID)
        {
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.DELETEBATCH_EMPLOYEE;

            var listRemove = "";
            foreach (var employeeID in listEmployeeID)
            {
                if (listRemove == "")
                {
                    listRemove += "'" + employeeID + "'";
                }
                else
                {
                    listRemove += "," + "'" + employeeID + "'";
                }
            }
            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@ListID", listRemove);

            //Khởi tạo kết nối tới DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var trans = mySqlConnection.BeginTransaction();
                try
                {
                    var result = mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
                    if(result == listEmployeeID.Count)
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    var error = new Error()
                    {
                        DevMsg = ex.Message,
                        UsersMsg = ResourceVN.Error_not_delete_batch,
                    };
                    return false;

                }
            }
        }

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách nhân viên tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public SearchAndPaging SearchAndPagingEmployee(string? keyword, int limit = 20, int pageNumber = 1)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureSearchAndPaging = String.Format(Procedure.SEARCH_AND_PAGING, "employee");
            string procedureGetTotalEmployee = String.Format(Procedure.GET_TOTAL_RECORD, "employee");

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@keyword", keyword);
            //Lấy limit bản ghi trong 1 trang
            parameters.Add("@Limit", limit);
            //Lấy từ trang số bao nhiêu
            int offset = limit * (pageNumber - 1);
            parameters.Add("@Offset", offset);

            var parameter = new DynamicParameters();
            parameter.Add("@keyword", keyword);


            // Thuc hien goi vao DB
            var employees = mySqlConnection.Query(procedureSearchAndPaging, parameters, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = mySqlConnection.QueryFirstOrDefault<int>(procedureGetTotalEmployee, parameter, commandType: System.Data.CommandType.StoredProcedure);

            int totalPage = 0;
            totalPage = (totalRecord % limit == 0) ? (totalRecord / limit) : (totalRecord / limit + 1);

            var result = new SearchAndPaging()
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = employees
            };
            return result;
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public string GetEmployeeCodeMax()
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureGetCodeMax = Procedure.GET_EMPLOYEE_CODE_MAX;

            //Thực hiện câu lệnh SQL
            string codeMax = mySqlConnection.QueryFirstOrDefault<string>(procedureGetCodeMax, commandType: System.Data.CommandType.StoredProcedure);

            //Xử lý chuỗi trả về
            //1.Cắt lấy phần chữ
            string prefix = Regex.Match(codeMax, "^\\D+").Value;
            //2.Cắt lấy phần số
            string number = Regex.Replace(codeMax, "^\\D+", "");
            //3.Tăng lên 1 đơn vị
            int i = int.Parse(number) + 1;
            //4.Ghép chuỗi
            string newCode = prefix + i.ToString(new string('0', number.Length));
            return newCode;
        }

        /// <summary>
        /// Hàm kiểm tra trùng mã khi thêm mới hoặc cập nhật nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên muốn thêm hoặc sửa</param>
        /// <param name="employeeCode"> EmployeeCode của nhân viên muốn thêm hoặc sửa</param>
        /// <returns>
        /// true nếu không trùng
        /// false nếu trùng mã
        /// </returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public bool CheckUpDuplicateCode(Guid employeeID, string employeeCode)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureGetAllEmployeeCode = Procedure.GET_EMPLOYEE_CHECK_DUPLICATE_CODE;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeCode", employeeCode);

            //Thực hiện câu lệnh SQL
            var resultEmployeeCode = mySqlConnection.QueryFirstOrDefault(procedureGetAllEmployeeCode, parameter, commandType: System.Data.CommandType.StoredProcedure);

            //Nếu EmployeeCode trùng nhau
            if (resultEmployeeCode != null)
            {
                //Kiểm tra EmployeeID xem có trùng nhau không
                if (resultEmployeeCode.EmployeeID != employeeID)
                    return false;
            }
            return true;
        }

    }

}
