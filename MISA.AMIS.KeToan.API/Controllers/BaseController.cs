using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.Common.Resources;
using MySqlConnector;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor
        public BaseController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion


        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy: LQTrung(11/11/2022)
        [HttpGet]
        public IActionResult GetAllRecord()
        {
            try
            {
                var records = _baseBL.GetAllRecord();
                //Xử lý kết quả khi DB trả về kết quả
                //Nếu thành công thì trả về dữ liệu cho FE
                if (records != null)
                    return StatusCode(StatusCodes.Status200OK, records);

                return StatusCode(StatusCodes.Status500InternalServerError, new List<T>());
            }
            catch (Exception ex)
            {
                return ShowException(ex);
            }
        }

        /// <summary>
        /// API lấy thông tin chi tiết 1 bản ghi
        /// </summary>
        /// <param name="recordID"> ID của bản ghi muốn lấy</param>
        /// <returns>Trả về thông tin chi tiết của 1 record</returns>
        /// /// CreatedBy: LQTrung(11/11/2022)
        [HttpGet]
        [Route("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);
                if (record != null)
                    return StatusCode(StatusCodes.Status200OK, record);
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {

                return ShowException(ex);
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa được thêm</returns>
        /// Created by: LQTrung (12/11/2022)
        [HttpPost]
        public IActionResult InsertARecord([FromBody] T record)
        {
            try
            { 
                // Xử lý nghiệp vụ
                var recordID = _baseBL.InsertARecord(record);
                // Xử lý kết quả trả về
                if (recordID != null)
                    return StatusCode(StatusCodes.Status201Created, recordID);

                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            catch (MISAValidateException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Error
                    {
                        DevMsg = e.Message,
                        UsersMsg = ResourceVN.Error_Exception,
                        MoreInfo = "https://openapi.misa.com.vn/errorcode",
                        TraceId = Guid.NewGuid(),
                        Data = e.ErrorDetails
                    });
            }
            catch( MySqlException e2)
            {
                Console.WriteLine(e2);
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Error
                    {
                        DevMsg = e2.Message,
                        UsersMsg = ResourceVN.Error_Exception,
                        MoreInfo = "https://openapi.misa.com.vn/errorcode",
                        TraceId = Guid.NewGuid(),
                        //Data = e.ErrorDetails
                    });
            }
            catch (Exception e)
            {
               return ShowException(e);
            }
        }

        /// <summary>
        /// Sửa một bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn sửa</param>
        /// <param name="record">Thông tin mới của bản ghi </param>
        /// <returns>ID của bản ghi vừa sửa đổi</returns>
        /// Created by: LQTrung(12/11/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateARecord(
            [FromRoute] Guid recordID,
            [FromBody] T record)
        {
            try
            {
                var result = _baseBL.UpdateARecord(recordID, record);
                if (result != null)
                    return StatusCode(200, result);
                else 
                    return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            catch (MISAValidateException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Error
                    {
                        DevMsg = e.Message,
                        UsersMsg = ResourceVN.Error_Exception,
                        MoreInfo = "https://openapi.misa.com.vn/errorcode",
                        TraceId = Guid.NewGuid(),
                        Data = e.ErrorDetails
                    });
            }
            catch (Exception e)
            {
                return ShowException(e);
            }

        }

        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteARecord([FromRoute] Guid recordID)
        {
            try
            {
                int numberOfRowsAffected = _baseBL.DeleteARecord(recordID);
                //Xử lý kết quả trả về 
                if (numberOfRowsAffected > 0)
                    return StatusCode(StatusCodes.Status200OK, recordID);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return ShowException(ex);
            }
        }

        /// <summary>
        /// Hàm hiển thị thông báo lỗi
        /// </summary>
        /// <param name="ex">Exception nhận được</param>
        /// <returns>Trả về một đối tượng gồm các thông báo lỗi đến người dùng, thông báo lỗi đến dev</returns>
        protected IActionResult ShowException(Exception ex)
        {
            var error = new Error
            {
                DevMsg = ex.Message,
                UsersMsg = ResourceVN.Error_Exception,
                MoreInfo = ResourceVN.MoreInfo_Exception,
                TraceId = Guid.NewGuid() 
            };
            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }

    }
}
