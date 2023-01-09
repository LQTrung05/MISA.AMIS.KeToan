using MISA.AMIS.KeToan.Common.MISAAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Provider
    {
        /// <summary>
        /// ID của bảng nhà cung cấp
        /// </summary>
        [Key]
        public Guid ProviderID { get; set; }

        /// <summary>
        /// ID của điều khoản thanh toán
        /// </summary>
        public Guid? PaymentTermID { get; set; }

        /// <summary>
        /// Mã của điều khoản thanh toán
        /// </summary>
        public string? PaymentTermCode { get; set; }

        /// <summary>
        /// ID của tài khoản công nợ phải trả
        /// </summary>
        public Guid? AccountPayableID { get; set; }

        /// <summary>
        /// Số tài khoản công nợ phải trả
        /// </summary>
        public string? AccountPayableNumber { get; set; }

        /// <summary>
        /// ID của tài khoản công nợ phải thu nếu nhà cung cấp là khách hàng
        /// </summary>
        public Guid? AccountReceivableID { get; set; }

        /// <summary>
        /// Số tài khoản công nợ phải thu nếu nhà cung cấp là khách hàng
        /// </summary>
        public string? AccountReceivableNumber { get; set; }

        /// <summary>
        /// Mã nhân viên mua hàng
        /// </summary>
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        [Required(ErrorMessage ="Mã nhà cung cấp không để trống")]
        [RegularExpression(@"(?=^.{0,20}$)^[a-zA-Z0-9]+[0-9]$",
            ErrorMessage = "Mã nhà cung cấp phải kết thúc là chữ số và không nhiều hơn 20 kí tự.")]
        public string ProviderCode{ get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        [Required(ErrorMessage = "Mã nhà cung cấp không để trống")]
        public string ProviderName { get; set; }

        /// <summary>
        /// Nhà cung cấp là một tổ chức hoặc là 1 cá nhân
        /// </summary>
        public int? ProviderType { get; set; }

        /// <summary>
        /// Nhà cung cấp là khách hàng
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// Mã số thuế của nhà cung cấp
        /// </summary>
        public string? TaxCode{ get; set; }

        /// <summary>
        /// Địa chỉ của nhà cung cấp
        /// </summary>
        public string? Address{ get; set; }

        /// <summary>
        /// Số điện thoại của nhà cung cấp, nếu là cung cấp là tổ chức
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Website của nhà cung cấp, nếu là cung cấp là tổ chức
        /// </summary>
        public string? Website { get; set; }
        
        /// <summary>
        /// Xưng hô với nhà cung cấp, nếu nhà cung cấp là cá nhân
        /// </summary>
        public string? Prefix { get; set; }

        /// <summary>
        /// Họ tên của người liên hệ nếu nhà cung cấp là tổ chức
        /// </summary>
        public string? ContactFullname { get; set; }

        /// <summary>
        /// Email của người liên hệ nếu nhà cung cấp là tổ chức
        /// </summary>
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "Email liên hệ sai định dạng, vui lòng kiểm tra lại.")]
        public string? ContactEmail { get; set; }

        /// <summary>
        /// Số điện thoại của người liên hệ nếu nhà cung cấp là tổ chức
        /// </summary>
        public string? ContactPhone { get; set; }

        /// <summary>
        /// Email liên hệ nếu nhà cung cấp là cá nhân
        /// </summary>
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "Email nhà cung cấp cá nhân sai định dạng, vui lòng kiểm tra lại.")]
        public string? EmailOfIndividual { get; set; }

        /// <summary>
        /// Số điện thoại liên hệ nếu nhà cung cấp là cá nhân
        /// </summary>
        public string? PhoneOfIndividual { get; set; }

        /// <summary>
        /// Số điện thoại bàn liên hệ nếu nhà cung cấp là cá nhân
        /// </summary>
        public string? LandPhoneOfIndividual { get; set; }

        /// <summary>
        /// Đại diện theo pháp luật của nhà cung cấp
        /// </summary>
        public string? LegalRepresentative { get; set; }

        /// <summary>
        /// Số chứng minh thư của nhà cung cấp nếu nhà cung cấp là cá nhân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh thư của nhà cung cấp nếu nhà cung cấp là cá nhân
        /// </summary>
        [DateTimeValid(ErrorMessage = "Ngày cấp không được lớn hơn ngày hiện tại, vui lòng kiểm tra lại.")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi  cấp chứng minh thư của nhà cung cấp nếu nhà cung cấp là cá nhân
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Họ tên của người nhận hóa đơn điện tử nếu nhà cung cấp là khách hàng
        /// </summary>
        public string? FullNameInvoiceRecipient { get; set; }

        /// <summary>
        /// Email của người nhận hóa đơn điện tử nếu nhà cung cấp là khách hàng
        /// </summary>
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "Email người nhận hóa đơn sai định dạng, vui lòng kiểm tra lại.")]
        public string? EmailInvoiceRecipient{ get; set; }

        /// <summary>
        /// Số điện thoại của người nhận hóa đơn điện tử nếu nhà cung cấp là khách hàng
        /// </summary>
        public string? PhoneInvoiceRecipient { get; set; }

        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        public int? DueTime{ get; set; }

        /// <summary>
        /// Số nợ tối đa
        /// </summary>
        public float? MaximumDebt { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Tên quốc gia của nhà cung cấp trong mục địa chỉ khác
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Tên tỉnh/thành phố của nhà cung cấp trong mục địa chỉ khác
        /// </summary>
        public string? Province { get; set; }
        /// <summary>
        /// Tên quận/huyện của nhà cung cấp trong mục địa chỉ khác
        /// </summary>
        public string? District { get; set; }

        /// <summary>
        /// Tên phường/xã của nhà cung cấp trong mục địa chỉ khác
        /// </summary>
        public string? Village { get; set; }

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
