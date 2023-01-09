using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class AccountPayable
    {
        /// <summary>
        /// ID của tài khoản công nợ phải trả
        /// </summary>
        [Key]
        public Guid AccountPayableID { get; set; }

        /// <summary>
        /// Số tài khoản công nợ phải trả
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Tên tài khoản công nợ phải trả
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
