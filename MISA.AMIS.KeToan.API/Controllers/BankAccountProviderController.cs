using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BankAccountProviderController : BaseController<BankAccountProvider>
    {
        #region Field
        private IBankAccountProviderBL _bankAccountProviderBL;
        #endregion

        #region Constructor
        public BankAccountProviderController(IBankAccountProviderBL bankAccountProviderBL) : base(bankAccountProviderBL)
        {
            _bankAccountProviderBL = bankAccountProviderBL;
        }
        #endregion

        /// <summary>
        /// Lấy danh sách các tài khoản ngân hàng của 1 nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <returns>Status code 200 khi xóa thành công</returns>
        /// CreatedBy: LQTrung(24/12/2022)
        /// Xóa hàng loạt không sử dụng method HttpDelete, mà sử dụng HttpPost, và vì xóa nhiều nên phải truyền vào
        /// FormBody vì số lượng tham số có thể nhiều hơn độ dài cho phép của FormRoute
        [HttpGet("GetAllByProviderID")]
        public IActionResult GetAllRecordByProviderID([FromQuery] Guid providerID)
        {
            try
            {
                var result = _bankAccountProviderBL.GetAllRecordByProviderID(providerID);
                //Xử lý kết quả trả về 
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);

                var error = new Error
                {
                    DevMsg = ResourceVN.Error_Exception,
                    UsersMsg = ResourceVN.Error_Exception,
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
                    UsersMsg = ResourceVN.Error_Exception,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }


        /// <summary>
        /// Thêm nhiều tài khoản ngân hàng của một nhà cung cấp
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (27/12/2022)
        [HttpPost("{providerID}")]
        public IActionResult InsertBatch([FromRoute] Guid providerID, [FromBody] List<BankAccountProvider> listData)
        {
            try
            {
                var result = _bankAccountProviderBL.InsertBatch(providerID, listData);
                //Xử lý kết quả trả về 
                if (result)
                    return StatusCode(StatusCodes.Status200OK);

                var error = new Error
                {
                    DevMsg = ResourceVN.Error_Exception,
                    UsersMsg = ResourceVN.Error_Exception,
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
                    UsersMsg = ResourceVN.Error_Exception,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        /// <summary>
            /// API xóa các tài khoản ngân hàng của 1 nhà cung cấp được chọn
            /// </summary>
            /// <param name="providerID">ID của nhà cung cấp muốn xóa</param>
            /// <returns>số bản ghi vừa xóa</returns>
            /// CreatedBy: LQTrung (24/12/2022)
        [HttpDelete("{providerID}/deleteARecord")]
        public IActionResult DeleteARecord([FromRoute] Guid providerID)
        {
            try
            {
                var numberOfRowsAffected = _bankAccountProviderBL.DeleteARecord(providerID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected != null)
                    return StatusCode(StatusCodes.Status200OK, providerID);
                var error = new Error
                {
                    DevMsg = ResourceVN.Error_Exception,
                    UsersMsg = ResourceVN.Error_Exception,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
            catch (Exception ex)
            {
                return ShowException(ex);
            }
        }
    }
}
