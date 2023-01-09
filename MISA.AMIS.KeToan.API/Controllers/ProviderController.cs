using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProviderController : BaseController<Provider>
    {
        #region Field
        private IProviderBL _providerBL;

        #endregion

        #region Constructor
        public ProviderController(IProviderBL providerBL) : base(providerBL)
        {
            _providerBL = providerBL;
        }
        #endregion

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
        [HttpGet]
        [Route("SearchAndPaging")]
        public IActionResult SearchAndPagingProvider(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = (SearchAndPaging)_providerBL.SearchAndPagingProvider(keyword, limit, pageNumber);
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
        /// Xóa nhiều nhà cung cấp theo danh sách ID truyền vào
        /// </summary>
        /// <param name="providerIDList">Danh sách ID của các nhà cung cấp muốn xóa</param>
        /// <returns>Số nhà cung cấp bị xóa thành công</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        [HttpPost("DeleteBatch")]
        public IActionResult DeleteRecords([FromBody] List<Guid> providerIDList)
        {
            try
            {
                var numberOfRowsAffected = _providerBL.DeleteRecords(providerIDList);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected == true)
                    return StatusCode(StatusCodes.Status200OK, providerIDList);

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
        /// Lấy mã nhà cung cấp lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhà cung lớn nhất đã tăng lên 1 đơn vị so với mã lớn nhất ở database</returns>
        /// CreatedBy: LQTrung (29/12/2022)
        [HttpGet("NewProviderCode")]
        public IActionResult GetProviderCodeMax()
        {
            try
            {
                string newProviderCode = _providerBL.GetProviderCodeMax();
                //Xử lý kết quả trả về 
                if (newProviderCode != null)
                    return StatusCode(StatusCodes.Status200OK, newProviderCode);
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
        /// Xuất khẩu danh sách nhà cung cấp theo trang
        /// </summary>
        /// <returns>File excel chứa danh sách nhà cung cấp</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        //[HttpGet("ExportProviderExcelFile")]
        //public IActionResult ExportProviderExcelFile(CancellationToken cancellationToken,
        //    [FromQuery] string? keyword,
        //    [FromQuery] int limit = 10,
        //    [FromQuery] int pageNumber = 1)
        //{
        //    try
        //    {
        //        var stream = _providerBL.ExportProviderExcelFile(keyword, limit, pageNumber);
        //        string excelName = $"Danh_sach_nha_cung_cap.xlsx";

        //        return File(stream, "application/octet-stream", excelName);

        //    }
        //    catch (Exception ex)
        //    {
        //        var error = new Error
        //        {
        //            DevMsg = ex.Message,
        //            UsersMsg = ResourceVN.Error_Exception,
        //            MoreInfo = "https://openapi.misa.com.vn/errorcode",
        //            TraceId = Guid.NewGuid()
        //        };
        //        return StatusCode(StatusCodes.Status500InternalServerError, error);

        //    }
        //}

        [HttpGet("ExportProviderExcelFile")]
        public IActionResult ExportProviderExcelFile(CancellationToken cancellationToken)
        {
            try
            {
                var stream = _providerBL.ExportProviderExcelFile();
                string excelName = $"Danh_sach_nha_cung_cap.xlsx";

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
        [HttpGet]
        [Route("FilterByMultipleConditions")]
        public IActionResult FilterByMultipleConditions(
            [FromQuery] int? providerType,
            [FromQuery] string? province,
            [FromQuery] string? district,
            [FromQuery] string? village,
             [FromQuery] string? providerGroupCode,
            [FromQuery] int limit = 10,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = (SearchAndPaging)_providerBL.FilterByMultipleConditions(providerType, province, district, village, providerGroupCode,  limit, pageNumber);
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return ShowException(ex);

            }
        }

    }
}
