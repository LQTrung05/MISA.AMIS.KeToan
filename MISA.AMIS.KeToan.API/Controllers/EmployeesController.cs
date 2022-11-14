using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dapper;
using MySqlConnector;
using MISA.AMIS.KeToan.Common.Entities;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        #region Field
        private IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion


        /// <summary>
        /// API tìm kiếm, phân trang
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên muốn tìm kiếm</param>
        /// <param name="employeeName">Tên nhân viên muốn tìm kiếm</param>
        /// <param name="phoneNumber">Số điện thoại muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Lấy từ vị trí bao nhiêu</param>
        /// <returns>
        /// 200:Danh sách nhân viên tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        [HttpGet]
        [Route("SearchAndPaging")]
        public IActionResult SearchAndPagingEmployee(
            [FromQuery] string? employeeCode,
            [FromQuery] string? employeeName,
            [FromQuery] string? phoneNumber,
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0)
        {
            try
            {
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                string procedureSearchAndPaging = "Proc_employee_SearchAndPaging";
                string procedureGetTotalEmployee = "Proc_employee_GetTotalEmployee";

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);
                parameters.Add("@EmployeeName", employeeName);
                parameters.Add("@PhoneNumber", phoneNumber);
                parameters.Add("@Limit", limit);
                parameters.Add("@Offset", offset);


                // Thuc hien goi vao DB
                var employees = mySqlConnection.Query(procedureSearchAndPaging, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var totalRecord = mySqlConnection.QueryFirstOrDefault(procedureGetTotalEmployee, commandType: System.Data.CommandType.StoredProcedure);
                if (employees != null && totalRecord != null)
                {
                    var result = new
                    {
                        totalRecord,
                        employees
                    };
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                //return ShowException(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);


            }

        }

        
        /// <summary>
        /// Xóa hàng loạt nhân viên
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200 khi xóa thành công</returns>
        /// CreatedBy: LQTrung(5/11/2022)
        /// Xóa hàng loạt không sử dụng method HttpDelete, mà sử dụng HttpPost, và vì xóa nhiều nên phải truyền vào
        /// FormBody vì số lượng tham số có thể nhiều hơn độ dài cho phép của FormRoute
        [HttpPost("DeleteBatch")]
        public IActionResult DeleteRecords( [FromBody] ListEmployeeID listEmployeeID)
        {
            try
            {
                int numberOfRowsAffected = _employeeBL.DeleteRecords(listEmployeeID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected > 0)
                    return StatusCode(StatusCodes.Status200OK, listEmployeeID);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UsersMsg = ResourceVN.Error_Exception,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }
        //public IActionResult DeleteMultipleEmployees([FromBody] ListEmployeeID listEmployeeID)
        //{
        //    //Khởi tạo kết nối tới DB
        //    string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
        //    var mySqlConnection = new MySqlConnection(connectionString);
        //    mySqlConnection.Open();
        //    //Chuẩn bị câu lệnh SQL
        //    var storeProcedureName = "Proc_employee_Delete";

        //    var trans = mySqlConnection.BeginTransaction();
        //    //var count = listEmployeeID.EmployeeIDs.Count();

        //    try
        //    {
        //        //Chuẩn bị tham số đầu vào
        //        foreach (var employeeID in listEmployeeID.EmployeeIDs)
        //        {
        //            var parameters = new DynamicParameters();
        //            parameters.Add("@EmployeeID", employeeID);
        //            mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
        //        }
        //        trans.Commit();

        //        return StatusCode(200, listEmployeeID.EmployeeIDs.Count);
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        //return ShowException(ex);
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

        //    }
        //}


    }
}
