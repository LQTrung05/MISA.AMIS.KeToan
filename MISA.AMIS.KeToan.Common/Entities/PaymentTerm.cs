using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class PaymentTerm
    {
        /// <summary>
        /// ID của điều khoản thanh toán
        /// </summary>
        [Key]
        public Guid PaymentTermID { get; set; }

        /// <summary>
        /// Mã điều khoản thanh toán
        /// </summary>
        [Required(ErrorMessage ="Mã điều khoản không để trống")]
        public string PaymentTermCode { get; set; }

        /// <summary>
        /// Tên điều khoản thanh toán
        /// </summary>
        [Required(ErrorMessage = "Tên điều khoản không để trống")]
        public string PaymentTermName { get; set; }

        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        public int? DueTime { get; set; }

        /// <summary>
        /// Thời hạn được hưởng chiết khấu
        /// </summary>
        public int? DiscountTime { get; set; }

        /// <summary>
        /// Mức chiết khấu dược hưởng
        /// </summary>
        public float? DiscountRate { get; set; }

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
