using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Receipt
    {
        /// <summary>
        /// ID của khoản thu chi
        /// </summary>
        public Guid ReceiptID { get; set; }

        /// <summary>
        /// Ngày hạch toán
        /// </summary>
        public DateTime? PostedDate { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string? VoucherNo { get; set; }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public float? TotalMount { get; set; }

        /// <summary>
        /// Đối tượng
        /// </summary>
        public string? SubjectName { get; set; }

        /// <summary>
        /// Lý do chi
        /// </summary>
        public string? Particular { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public string? VoucherType { get; set; }

        /// <summary>
        /// Chi nhánh lập chứng từ
        /// </summary>
        public string? Branch { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian chỉnh sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }

    }
}
