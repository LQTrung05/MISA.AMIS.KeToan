using MISA.AMIS.KeToan.API.Entities.Enums;

namespace MISA.AMIS.KeToan.API.Entities
{
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// ID đơn vị phòng ban
        /// </summary>
        public Guid DerpartmentID { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime IdentityDate { get; set; }

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
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Nhà cung cấp: 1 là nhà cung cấp, 0 không là nhà cung cấp
        /// </summary>
        public bool IsProvider { get; set; }
        //Thêm các trường khác trong database vào đây

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian sửa đổi gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }

        

    }

}

