using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class Provider_ProviderGroupsController : BaseController<Provider_ProviderGroup>
    {
        #region Field
        private IProvider_ProviderGroupBL _provider_ProviderGroupBL;
        #endregion

        #region Constructor

        public Provider_ProviderGroupsController(IProvider_ProviderGroupBL provider_ProviderGroupBL) : base(provider_ProviderGroupBL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _provider_ProviderGroupBL = provider_ProviderGroupBL;
        }
        #endregion

        /// <summary>
        /// API lấy danh sách tất cả nhóm nhà cung cấp của 1 nhà cung cấp
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy: LQTrung(25/12/2022)
        [HttpGet("GetAllRecordsByID")]
        public IActionResult GetAllRecordByID(Guid providerID)
        {
            try
            {
                var records = _provider_ProviderGroupBL.GetAllRecordByID(providerID);
                //Xử lý kết quả khi DB trả về kết quả
                //Nếu thành công thì trả về dữ liệu cho FE
                if (records != null)
                    return StatusCode(StatusCodes.Status200OK, records);

                return StatusCode(StatusCodes.Status500InternalServerError, new List<ProviderGroupListByProviderID>());
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UsersMsg = ResourceVN.Error_not_get_records,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }


        /// <summary>
        /// Thêm mới nhiều nhóm nhà cung cấp vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="listProviderGroupID">Danh sách các nhóm nhà cung cấp thuộc nhà cung cấp</param>
        /// <returns></returns>
        [HttpPost("{providerID}")]
        public IActionResult InsertMultilProviderGroup([FromRoute] Guid providerID, [FromBody] List<Guid> listProviderGroupID)
        {
            try
            {
                var numberOfRowsAffected = _provider_ProviderGroupBL.InsertMultilProviderGroup(providerID, listProviderGroupID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected == true)
                    return StatusCode(StatusCodes.Status200OK, listProviderGroupID);

                var error = new Error
                {
                    DevMsg = ResourceVN.Error_not_insert_batch,
                    UsersMsg = ResourceVN.Error_not_insert_batch,
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
                    UsersMsg = ResourceVN.Error_not_insert_batch,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode",
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);

            }
        }


        /// <summary>
        /// API xóa tất cả nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp </param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (27/12/2022)
        [HttpDelete("DeleteBatch/{providerID}")]
        public IActionResult DeleteBatchProviderGroupByProviderID([FromRoute] Guid providerID)
        {
            try
            {
                int numberOfRowsAffected = _provider_ProviderGroupBL.DeleteBatchProviderGroupByProviderID(providerID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected > 0)
                    return StatusCode(StatusCodes.Status200OK, providerID);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    DevMsg = ex.Message,
                    UsersMsg = ResourceVN.Error_not_delete_provider_group,
                    MoreInfo = ResourceVN.MoreInfo_Exception,
                    TraceId = Guid.NewGuid()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

    }
}
