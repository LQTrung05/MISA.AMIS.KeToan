using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class ProviderGroup
    {
        /// <summary>
        /// ID của nhóm nhà cung cấp
        /// </summary>
        [Key]
        public Guid ProviderGroupID { get; set; }

        /// <summary>
        /// ID nhóm nhà cung cấp là cha của nhóm nhà cung cấp hiện tại
        /// </summary>
        public Guid? ProviderParentID { get; set; }

        /// <summary>
        /// Mã nhóm nhà cung cấp
        /// </summary>
        [Required(ErrorMessage ="Mã nhóm nhà cung cấp không được để trống")]
        public string ProviderGroupCode { get; set; }

        /// <summary>
        /// Tên nhóm nhà cung cấp
        /// </summary>
        [Required(ErrorMessage = "Tên nhóm nhà cung cấp không được để trống")]
        public string ProviderGroupName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

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
