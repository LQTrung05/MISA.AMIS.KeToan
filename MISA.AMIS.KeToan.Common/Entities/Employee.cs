using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.MISAAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MISA.AMIS.KeToan.Common.Resources;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Employee 
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage ="Mã nhân viên không được để trống, vui lòng kiểm tra lại.")]
        [RegularExpression(@"(?=^.{0,20}$)^[a-zA-Z0-9]+[0-9]$", 
            ErrorMessage = "Mã nhân viên phải kết thúc là chữ số và không nhiều hơn 20 kí tự.")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        //[Required(ErrorMessageResourceType = typeof(ResourceVN),
        //    ErrorMessageResourceName = ("EmployeeNameIsRequired"))]
        [Required(ErrorMessage = "Tên nhân viên không được để trống, vui lòng kiểm tra lại.")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [DateTimeValid(ErrorMessage = "Ngày sinh không được lớn hơn ngày hiện tại, vui lòng kiểm tra lại.")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Mã giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }

        /// <summary>
        /// ID đơn vị phòng ban
        /// </summary>
        [Required(ErrorMessage = "Phòng ban không để trống, vui lòng kiểm tra lại.")]
        public Guid DerpartmentID { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        [DateTimeValid(ErrorMessage = "Ngày cấp không được lớn hơn ngày hiện tại, vui lòng kiểm tra lại.")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại bàn
        /// </summary>
        public string? LandPhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "Email sai định dạng, vui lòng kiểm tra lại." )]
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Khách hàng: 1 là khách hàng, 0 không là khách hàng
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// Nhà cung cấp: 1 là nhà cung cấp, 0 không là nhà cung cấp
        /// </summary>
        public int? IsProvider { get; set; }
        //Thêm các trường khác trong database vào đây

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

