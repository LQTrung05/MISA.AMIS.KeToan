using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class AnotherAddressOfProvider
    {
        /// <summary>
        /// ID của địa chỉ khác của nhà cung cấp
        /// </summary>
        public Guid AnotherAddressOfProviderID { get; set; }

        /// <summary>
        /// ID của nhà cung cấp có địa chỉ khác
        /// </summary>
        public Guid? ProviderID { get; set; }

        /// <summary>
        /// Thông tin chi tiết địa chỉ khác
        /// </summary>
        public string? AnotherAddress { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian sửa đổi gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
