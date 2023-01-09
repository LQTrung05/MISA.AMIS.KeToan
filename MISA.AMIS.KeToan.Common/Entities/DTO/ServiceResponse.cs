using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities.DTO
{
    public class ServiceResponse
    {
        /// <summary>
        /// Kết quả trả về
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Danh sách các lỗi nếu có
        /// </summary>
        public List<string> ErrorDetails { get; set; }

        #region Constructor
        
        public ServiceResponse()
        {

        }
        public ServiceResponse( bool result, List<string> detail)
        {
            Success = result;
            ErrorDetails = detail;
        }
       
        #endregion
    }
}
