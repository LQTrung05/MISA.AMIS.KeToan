using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnotherAddressOfProviderController : BaseController<AnotherAddressOfProvider>
    {
        #region Field
        private IAnotherAddressOfProviderBL _anotherAddressOfProviderBL;
        #endregion

        #region Constructor
        public AnotherAddressOfProviderController(IAnotherAddressOfProviderBL anotherAddressOfProviderBL) : base(anotherAddressOfProviderBL)
        {
            _anotherAddressOfProviderBL = anotherAddressOfProviderBL;
        }
        #endregion
        /// <summary>
        /// API lấy danh sách các địa chỉ khác của nhà cung cấp theo ID
        /// </summary>
        /// <param name="providerID">Nhà cung cấp muốn lấy địa chỉ</param>
        /// <returns>Trả về danh sách địa chỉ của 1 nhà cung cấp</returns>
        [HttpGet("GetAllByProviderID")]
        public IActionResult GetAllRecordByProviderID([FromQuery] Guid providerID)
        {
            try
            {
                var result = _anotherAddressOfProviderBL.GetAllRecordByProviderID(providerID);
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
        public IActionResult InsertMultilAnotherAddress([FromRoute] Guid providerID, [FromBody] List<AnotherAddressOfProvider> listData)
        {
            try
            {
                var result = _anotherAddressOfProviderBL.InsertMultilAnotherAddress(providerID, listData);
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
        /// API xóa các địa chỉ khác của 1 nhà cung cấp theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của nhà cung cấp muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        [HttpDelete("{providerID}/deleteARecord")]
        public IActionResult DeleteARecord([FromRoute] Guid providerID)
        {
            try
            {
                var numberOfRowsAffected = _anotherAddressOfProviderBL.DeleteARecord(providerID);
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
