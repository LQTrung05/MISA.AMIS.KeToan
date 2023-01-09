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
    public class ProviderDL : BaseDL<Provider>, IProviderDL
    {
        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging SearchAndPagingProvider(string? keyword, int limit = 20, int pageNumber = 1)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureSearchAndPaging = String.Format(Procedure.SEARCH_AND_PAGING, "provider");
            string procedureGetTotalProvider = String.Format(Procedure.GET_TOTAL_RECORD, "provider");

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
            var providerList = mySqlConnection.Query(procedureSearchAndPaging, parameters, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = mySqlConnection.QueryFirstOrDefault<int>(procedureGetTotalProvider, parameter, commandType: System.Data.CommandType.StoredProcedure);

            int totalPage = 0;
            totalPage = (totalRecord % limit == 0) ? (totalRecord / limit) : (totalRecord / limit + 1);

            var result = new SearchAndPaging()
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = providerList
            };
            return result;
        }

        /// <summary>
        /// Xóa nhiều nhà cung cấp theo danh sách ID truyền vào
        /// </summary>
        /// <param name="providerIDList">Danh sách ID của các nhà cung cấp muốn xóa</param>
        /// <returns>Số nhà cung cấp bị xóa thành công</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        public bool DeleteRecords(List<Guid> providerIDList)
        {
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.DELETEBATCH_PROVIDER;

            var listRemove = "";
            foreach (var providerID in providerIDList)
            {
                if (listRemove == "")
                {
                    listRemove += "'" + providerID + "'";
                }
                else
                {
                    listRemove += "," + "'" + providerID + "'";
                }
            }
            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@ProviderIDList", listRemove);

            //Khởi tạo kết nối tới DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var trans = mySqlConnection.BeginTransaction();
                try
                {
                    var result = mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
                    if (result == providerIDList.Count)
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
        /// Hàm kiểm tra trùng mã khi thêm mới hoặc cập nhật nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn thêm hoặc sửa</param>
        /// <param name="providerCode"> EmployeeCode của nhà cung cấp muốn thêm hoặc sửa</param>
        /// <returns>
        /// true nếu không trùng
        /// false nếu trùng mã
        /// </returns>
        /// CreatedBy: LQTrung(29/12/2022)
        public bool CheckUpDuplicateCode(Guid providerID, string providerCode)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureGetAllEmployeeCode = Procedure.PROVIDER_CHECK_DUPLICATE_CODE;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@ProviderCode", providerCode);

            //Thực hiện câu lệnh SQL
            var resultProviderCode = mySqlConnection.QueryFirstOrDefault(procedureGetAllEmployeeCode, parameter, commandType: System.Data.CommandType.StoredProcedure);

            //Nếu ProviderCode trùng nhau
            if (resultProviderCode != null)
            {
                //Kiểm tra ProviderID xem có trùng nhau không
                if (resultProviderCode.ProviderID != providerID)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Hàm kiểm tra trùng mã số thuế khi thêm mới hoặc cập nhật nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn thêm hoặc sửa</param>
        /// <param name="taxCode"> TaxCode của nhà cung cấp muốn thêm hoặc sửa</param>
        /// <returns>
        /// true nếu không trùng
        /// false nếu trùng mã
        /// </returns>
        /// CreatedBy: LQTrung(29/12/2022)
        public bool CheckUpDuplicateTaxCode(Guid providerID, string taxCode)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureGetAllEmployeeCode = Procedure.PROVIDER_CHECK_DUPLICATE_TAXCODE;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@TaxCode", taxCode);

            //Thực hiện câu lệnh SQL
            var resultTaxCode = mySqlConnection.QueryFirstOrDefault(procedureGetAllEmployeeCode, parameter, commandType: System.Data.CommandType.StoredProcedure);

            //Nếu ProviderCode trùng nhau
            if (resultTaxCode != null)
            {
                //Kiểm tra ProviderID xem có trùng nhau không
                if (resultTaxCode.ProviderID != providerID)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhà cung lớn nhất đã tăng lên 1 đơn vị so với mã lớn nhất ở database</returns>
        /// CreatedBy: LQTrung (29/12/2022)
        public string GetProviderCodeMax()
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureGetCodeMax = Procedure.PROVIDER_GET_PROVIDERCODE_MAX;

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
        /// Lọc nhà cung cấp theo nhiều điều kiện
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging FilterByMultipleConditions(int? providerType,
            string? province, string? district, string? village, string? providerGroupCode, int limit = 10, int pageNumber = 1)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureFilterByMultipleConditions = Procedure.PROVIDER_FILTER_BY_MULTIPLE_CONDITIONS;
            string procedureGetTotalProvider = Procedure.PROVIDER_GET_TOTAL_PROVIDER_BY_FILTER;

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@ProviderType", providerType);
            parameters.Add("@Province", province);
            parameters.Add("@District", district);
            parameters.Add("@Village", village);
            parameters.Add("@ProviderGroupCode", providerGroupCode);

            //Lấy limit bản ghi trong 1 trang
            parameters.Add("@Limit", limit);
            //Lấy từ trang số bao nhiêu
            int offset = limit * (pageNumber - 1);
            parameters.Add("@Offset", offset);

            var parameterGetTotalRecord = new DynamicParameters();
            parameterGetTotalRecord.Add("@ProviderType", providerType);
            parameterGetTotalRecord.Add("@Province", province);
            parameterGetTotalRecord.Add("@District", district);
            parameterGetTotalRecord.Add("@Village", village);
            parameterGetTotalRecord.Add("@ProviderGroupCode", providerGroupCode);


            // Thuc hien goi vao DB
            var providerList = mySqlConnection.Query(procedureFilterByMultipleConditions, parameters, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = mySqlConnection.QueryFirstOrDefault<int>(procedureGetTotalProvider, parameterGetTotalRecord, commandType: System.Data.CommandType.StoredProcedure);

            int totalPage = 0;
            totalPage = (totalRecord % limit == 0) ? (totalRecord / limit) : (totalRecord / limit + 1);

            var result = new SearchAndPaging()
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = providerList
            };
            return result;
        }

    }
}
