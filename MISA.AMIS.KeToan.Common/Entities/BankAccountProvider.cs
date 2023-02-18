using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class BankAccountProvider
    {
        /// <summary>
        /// ID của tài khoản ngân hàng
        /// </summary>
        [Key]
       public Guid BankAccountProviderID { get; set; }

        /// <summary>
        /// ID của nhà cung cấp có tài khoản ngân hàng tương ứng
        /// </summary>
        public Guid? ProviderID { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        [Required(ErrorMessage ="Số tài khoản không để trống")]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankAccountName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// Nơi mở tài khoản ngân hàng 
        /// </summary>
        public string? OpenedAt { get; set; }

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
