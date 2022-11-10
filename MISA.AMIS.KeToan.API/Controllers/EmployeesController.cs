using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MISA.AMIS.KeToan.API.Entities;
using Dapper;
using MySqlConnector;
using MISA.AMIS.KeToan.API.Entities.DTO;
using MISA.AMIS.KeToan.API.Entities.BaseClass;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// CreatedBy: LQTrung(1/11/2022)
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_GetAll";

                //Thực hiện gọi vào DB, dùng dapper
                var employees = mySqlConnection.Query(storeProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả khi DB trả về kết quả
                //Nếu thành công thì trả về dữ liệu cho FE
                if (employees != null)
                    return StatusCode(StatusCodes.Status200OK, employees);

                return StatusCode(StatusCodes.Status500InternalServerError, new List<Employee>());
            }
            catch (Exception ex)
            {
                return ShowException(ex);
            }
        }

        /// <summary>
        /// API lấy thông tin chi tiết 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {

            try
            {
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_GetDetailTheEmployee";

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);

                // Thuc hien goi vao DB
                var employee = mySqlConnection.QueryFirstOrDefault(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (employee != null)
                    return StatusCode(StatusCodes.Status200OK, employee);

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                return ShowException(ex);

            }
        }

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
                return ShowException(ex);


            }

        }

        /// <summary>
        /// API thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>
        /// 201: Thêm mới thành công
        /// 400: Dữ liệu người dùng nhập vào không hợp lệ
        /// 500: Lỗi phía back end
        /// </returns>
        /// CreatedBy: LQTrung(01/11/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                //Khởi tạo EmployeeID khi thêm mới 1 nhân viên
                var employeeID = Guid.NewGuid();
                employee.EmployeeID = employeeID;
                var error = new Error();
                var errorList = new Dictionary<string, string>();

                //Bước 1: Validate dữ liệu truyền vào, trả về mã 400 và dữ liệu kèm theo
                // Mã nhân viên k để trống
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorList.Add("EmployeeCode", Resources.ResourceVN.EmployeeCodeIsRequired);
                }
                //Mã nhân viên không được trùng nhau 
                else if (!CheckEmployeeCode(employee.EmployeeCode))
                {
                    errorList.Add("EmployeeCode", Resources.ResourceVN.EmployeeCodeDuplicate);

                }

                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorList.Add("EmployeeName", Resources.ResourceVN.EmployeeNameIsRequired);
                }
                if (!IsValidEmail(employee.Email))
                {
                    errorList.Add("Email", Resources.ResourceVN.EmailInvalid);

                }
                if (errorList.Count > 0)
                {
                    error.UsersMsg = Resources.ResourceVN.InputInvalid;
                    error.Data = errorList;
                    return BadRequest(error);
                }
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_Insert";

                //Mở kết nối 
                mySqlConnection.Open();

                //Đọc các tham số đầu vào của store
                var sqlCommand = mySqlConnection.CreateCommand();
                sqlCommand.CommandText = storeProcedureName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    //Tên của tham số
                    var parameterName = parameter.ParameterName;
                    //Tên của thuộc tính sẽ bỏ đi kí tự @
                    var propertyName = parameterName.Replace("@", "");

                    var entityProperty = employee.GetType().GetProperty(propertyName);
                    if (entityProperty != null)
                    {
                        var propertyValue = employee.GetType().GetProperty(propertyName).GetValue(employee);
                        //Gán giá trị cho các parameter
                        parameters.Add(parameterName, propertyValue);
                    }
                    else
                        parameters.Add(parameterName, null);
                }
                mySqlConnection.Close();
                //ID tự sinh, nên bên front end không thể truyền lên được
                //parameters.Add("@EmployeeID", employeeID);
                //parameters.Add("") truyền tất cả tham số vào đây

                //Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, employee, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfRowsAffected > 0)
                    return StatusCode(201, employeeID);
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                return ShowException(ex);


            }
        }

        /// <summary>
        /// API thêm mới nhiều nhân viên
        /// </summary>
        /// <param name="listEmployee">Danh sách nhân viên muốn thêm mới</param>
        /// <returns>
        /// 201 nếu thêm mới thành công và trả về số nhân viên vừa thêm mới
        /// 500 nếu lỗi
        /// </returns>
        [HttpPost("InsertBatch")]
        public IActionResult InsertMultipleEmployee([FromBody] List<Employee> listEmployee)
        {
            //Khởi tạo kết nối tới DB MySQL
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
            var mySqlConnection = new MySqlConnection(connectionString);

            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = "Proc_employee_Insert";

            //Mở kết nối 
            mySqlConnection.Open();
            var trans = mySqlConnection.BeginTransaction();
            try
            {
                foreach (var employee in listEmployee)
                {
                    //Khởi tạo EmployeeID khi thêm mới 1 nhân viên
                    employee.EmployeeID = Guid.NewGuid();
                    //var error = new Error();
                    //var errorList = new Dictionary<string, string>();

                    ////Bước 1: Validate dữ liệu truyền vào, trả về mã 400 và dữ liệu kèm theo
                    //// Mã nhân viên k để trống
                    //if (string.IsNullOrEmpty(employee.EmployeeCode))
                    //{
                    //    errorList.Add("EmployeeCode", Resources.ResourceVN.EmployeeCodeIsRequired);
                    //}
                    ////Mã nhân viên không được trùng nhau 
                    //else if (!CheckEmployeeCode(employee.EmployeeCode))
                    //{
                    //    errorList.Add("EmployeeCode", Resources.ResourceVN.EmployeeCodeDuplicate);

                    //}

                    //if (string.IsNullOrEmpty(employee.EmployeeName))
                    //{
                    //    errorList.Add("EmployeeName", Resources.ResourceVN.EmployeeNameIsRequired);
                    //}
                    //if (!IsValidEmail(employee.Email))
                    //{
                    //    errorList.Add("Email", Resources.ResourceVN.EmailInvalid);

                    //}
                    //if (errorList.Count > 0)
                    //{
                    //    error.UsersMsg = Resources.ResourceVN.InputInvalid;
                    //    error.Data = errorList;
                    //    return BadRequest(error);
                    //}

                    //Đọc các tham số đầu vào của store
                    var sqlCommand = mySqlConnection.CreateCommand();
                    sqlCommand.CommandText = storeProcedureName;
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlCommandBuilder.DeriveParameters(sqlCommand);

                    //Chuẩn bị tham số đầu vào
                    var parameters = new DynamicParameters();
                    foreach (MySqlParameter parameter in sqlCommand.Parameters)
                    {
                        //Tên của tham số
                        var parameterName = parameter.ParameterName;
                        //Tên của thuộc tính sẽ bỏ đi kí tự @
                        var propertyName = parameterName.Replace("@", "");

                        var entityProperty = employee.GetType().GetProperty(propertyName);
                        if (entityProperty != null)
                        {
                            var propertyValue = employee.GetType().GetProperty(propertyName).GetValue(employee);
                            //Gán giá trị cho các parameter
                            parameters.Add(parameterName, propertyValue);
                        }
                        else
                            parameters.Add(parameterName, null);
                    }
                    //Thực hiện gọi vào DB
                    mySqlConnection.Execute(storeProcedureName, employee, trans, commandType: System.Data.CommandType.StoredProcedure);
                }
                trans.Commit();
                return StatusCode(StatusCodes.Status201Created, listEmployee.Count);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return StatusCode(500, ex);
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
        public IActionResult DeleteMultipleEmployees([FromBody] ListEmployeeID listEmployeeID)
        {
            //Khởi tạo kết nối tới DB
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
            var mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = "Proc_employee_Delete";

            var trans = mySqlConnection.BeginTransaction();
            //var count = listEmployeeID.EmployeeIDs.Count();

            try
            {
                //Chuẩn bị tham số đầu vào
                foreach (var employeeID in listEmployeeID.EmployeeIDs)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@EmployeeID", employeeID);
                    mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
                }
                trans.Commit();
                return StatusCode(200, listEmployeeID.EmployeeIDs.Count);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return ShowException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin một nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên sẽ sửa</param>
        /// <param name="employee">Đối tượng nhân viên sẽ sửa</param>
        /// <returns>ID của nhân viên vừa sửa xong</returns>
        /// CreatedBy: LQTrung (1/11/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmployee(
            [FromRoute] Guid employeeID,
            [FromBody] Employee employee)
        {
            //Validate dữ liệu
            try
            {
                var error = new Error();
                var errorList = new Dictionary<string, string>();

                //Bước 1: Validate dữ liệu truyền vào, trả về mã 400 và dữ liệu kèm theo
                // Mã nhân viên k để trống
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    errorList.Add("EmployeeCode", Resources.ResourceVN.EmployeeCodeIsRequired);
                }
                //Tên không để trống
                if (string.IsNullOrEmpty(employee.EmployeeName))
                {
                    errorList.Add("EmployeeName", Resources.ResourceVN.EmployeeNameIsRequired);
                }
                //Email sai định dạng
                //if (!IsValidEmail(employee.Email))
                //{
                //    errorList.Add("Email", Resources.ResourceVN.EmailInvalid);

                //}
                if (errorList.Count > 0)
                {
                    error.UsersMsg = Resources.ResourceVN.InputInvalid;
                    error.Data = errorList;
                    return BadRequest(error);
                }
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_Update";

                //Mở kết nối 
                mySqlConnection.Open();

                //Đọc các tham số đầu vào của store
                var sqlCommand = mySqlConnection.CreateCommand();
                sqlCommand.CommandText = storeProcedureName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters(sqlCommand);

                //Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                //employee.EmployeeID = employeeID;
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    //Tên của tham số
                    var parameterName = parameter.ParameterName;
                    //Tên của thuộc tính sẽ bỏ đi kí tự @
                    employee.EmployeeID = employeeID;
                    var propertyName = parameterName.Replace("@", "");

                    var entityProperty = employee.GetType().GetProperty(propertyName);
                    if (entityProperty != null)
                    {
                        var propertyValue = employee.GetType().GetProperty(propertyName).GetValue(employee);
                        //Gán giá trị cho các parameter
                        parameters.Add(parameterName, propertyValue);
                    }
                    else
                        parameters.Add(parameterName, null);
                }
                //mySqlConnection.Close();

                //Thực hiện gọi vào DB
                var numberOfRowsAffected = mySqlConnection.QueryFirstOrDefault(storeProcedureName, employee, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfRowsAffected != null)
                    return StatusCode(200, employee);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, employee);
            }
            catch (MySqlException ex1)
            {
                return StatusCode(400, new
                {
                    DevMsg = ex1.Message,
                    UserMsg = "Mã nhân viên đã tồn tại"
                });
            }
            catch (Exception ex)
            {
                return ShowException(ex);

            }

        }

        /// <summary>
        /// API xóa 1 nhân viên theo ID được chọn
        /// </summary>
        /// <param name="employeeID">ID của nhân viên muốn xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// CreatedBy: LQTrung (1/11/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                //Khởi tạo kết nối tới DB MySQL
                string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh SQL
                var storeProcedureName = "Proc_employee_Delete";

                //Chuẩn bị tham số đầu vào
                var parameter = new DynamicParameters();
                parameter.Add("@EmployeeID", employeeID);

                //Chạy câu lệnh SQL
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName,
                    parameter, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về 
                if (numberOfRowsAffected > 0)
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return ShowException(ex);


            }
        }

        /// <summary>
        /// Hàm kiểm tra mã nhân viên bị trùng
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// True nếu không trùng, False nếu bị trùng 
        /// CreatedBy: LQTrung(6/11/2022)
        /// </returns>
        public bool CheckEmployeeCode(string employeeCode)
        {
            string connectionString = "Server=localhost;Port=3306;Database=misa.web09.tcdn.lqtrung;Uid=root;Pwd=123@;";
            var mySqlConnection = new MySqlConnection(connectionString);
            var sqlCheck = "SELECT EmployeeCode FROM employee WHERE EmployeeCode = @employeeCode";
            var parameter = new DynamicParameters();
            parameter.Add("@EmployeeCode", employeeCode);
            var result = mySqlConnection.QueryFirstOrDefault(sqlCheck, parameter);
            if (result != null)
                return false;
            return true;
        }

        /// <summary>
        /// Hàm kiểm tra định dạng email 
        /// </summary>
        /// <param name="email">Email nhập vào</param>
        /// <returns>
        /// true nếu email đúng định dạng, false nếu sai định dạng
        /// </returns>
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm hiển thị thông báo lỗi
        /// </summary>
        /// <param name="ex">Exception nhận được</param>
        /// <returns>Trả về một đối tượng gồm các thông báo lỗi đến người dùng, thông báo lỗi đến dev</returns>
        public IActionResult ShowException(Exception ex)
        {
            var error = new Error
            {
                DevMsg = ex.Message,
                UsersMsg = Resources.ResourceVN.Error_Exception,
                Data = ex.Data,
                MoreInfo = "https://openapi.misa.com.vn/errorcode",
                TraceId = Guid.NewGuid()
            };
            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }
}
