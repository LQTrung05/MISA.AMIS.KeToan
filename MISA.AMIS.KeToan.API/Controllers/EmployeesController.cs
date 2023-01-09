using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dapper;
using MySqlConnector;
using MISA.AMIS.KeToan.Common.Entities;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Resources;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Enums;

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
        /// <param name="pageNumber">Lấy danh sách nhân viên ở trang số bao nhiêu</param>
        /// <returns>
        /// 200:Danh sách nhân viên tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(12/11/2022)
        [HttpGet]
        [Route("SearchAndPaging")]
        public IActionResult SearchAndPagingEmployee(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 20,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = (SearchAndPaging)_employeeBL.SearchAndPagingEmployee(keyword, limit, pageNumber);
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return ShowException(ex);

            }
        }

        /// <summary>
        /// Xóa hàng loạt nhân viên
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200 khi xóa thành công</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        /// Xóa hàng loạt không sử dụng method HttpDelete, mà sử dụng HttpPost, và vì xóa nhiều nên phải truyền vào
        /// FormBody vì số lượng tham số có thể nhiều hơn độ dài cho phép của FormRoute
        [HttpPost("DeleteBatch")]
        public IActionResult DeleteRecords([FromBody] List<Guid> listEmployeeID)
        {
            try
            {
                var numberOfRowsAffected = _employeeBL.DeleteRecords(listEmployeeID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected == true)
                    return StatusCode(StatusCodes.Status200OK, listEmployeeID);

                var error = new Error
                {
                    DevMsg = ResourceVN.Error_not_delete_batch,
                    UsersMsg = ResourceVN.Error_not_delete_batch,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UsersMsg = ResourceVN.Error_not_delete_batch,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetEmployeeCodeMax()
        {
            try
            {
                string newEmployeeCode = _employeeBL.GetEmployeeCodeMax();
                //Xử lý kết quả trả về 
                if (newEmployeeCode != null)
                    return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
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

        /// <summary>
        /// Xuất khẩu danh sách nhân viên
        /// </summary>
        /// <returns>File excel chứa danh sách nhân viên</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        [HttpGet("ExportExcelFile")]
        public IActionResult ExportExcelFile(CancellationToken cancellationToken)
        {
            try
            {
                var stream = _employeeBL.ExportExcelFile();
                string excelName = $"Danh_sach_nhan_vien.xlsx";

                return File(stream, "application/octet-stream", excelName);

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
    }
}
