using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class AccountReceivable
    {
        /// <summary>
        /// ID của tài khoản công nợ phải thu
        /// </summary>
        public Guid AccountReceivableID { get; set; }

        /// <summary>
        /// Số tài khoản công nợ phải thu
        /// </summary>
        public string AccountReceivableNumber { get; set; }

        /// <summary>
        /// Tên tài khoản công nợ phải thu
        /// </summary>
        public string AccountName { get; set; }

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
