using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : BaseController<Receipt>
    {
        #region Field
        private IReceiptBL _receiptBL;

        #endregion

        #region Constructor
        public ReceiptsController(IReceiptBL receiptBL) : base(receiptBL)
        {
            _receiptBL = receiptBL;
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
        public IActionResult SearchAndPagingReceipt(
            [FromQuery] string? keyword,
            [FromQuery] int limit = 10,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var result = (SearchAndPaging)_receiptBL.SearchAndPagingReceipt(keyword, limit, pageNumber);
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
