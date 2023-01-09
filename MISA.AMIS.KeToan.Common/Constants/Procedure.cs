using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Constants
{
    public class Procedure
    {
        //Rule viết tên biến constant trong c# là như sau

        /// <summary>
        /// Format tên procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Format tên procedure lấy 1 bản ghi theo id
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetDetailARecord";

        /// <summary>
        /// Format tên procedure thêm mới 1 bản ghi
        /// </summary>
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// Format tên procedure chỉnh sửa 1 bản ghi
        /// </summary>
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// Format tên procedure xóa 1 bản ghi
        /// </summary>
        public static string DELETE = "Proc_{0}_Delete";

        /// <summary>
        /// Format tên procedure tìm kiếm và phân trang
        /// </summary>
        public static string SEARCH_AND_PAGING = "Proc_{0}_SearchAndPaging";

        /// <summary>
        /// Format tên procedure lấy tổng số bản ghi
        /// </summary>
        public static string GET_TOTAL_RECORD = "Proc_{0}_GetTotalRecord";

        /// <summary>
        /// Format procedure lấy mã nhân viên lớn nhất
        /// </summary>
        public static string GET_EMPLOYEE_CODE_MAX = "Proc_employee_GetEmployeeCodeMax";

        /// <summary>
        /// Format procedure lấy tất cả mã nhân viên
        /// </summary>
        public static string GET_EMPLOYEE_CHECK_DUPLICATE_CODE = "Proc_employee_CheckDuplicateCode";

        /// <summary>
        /// Format procedure xóa hàng loạt nhân viên
        /// </summary>
        public const string DELETEBATCH_EMPLOYEE = "Proc_employee_DeleteBatch";

        #region PROVIDER
        /// <summary>
        /// Procedure xóa hàng loạt nhà cung cấp theo danh sách ID
        /// </summary>
        public const string DELETEBATCH_PROVIDER = "Proc_provider_DeleteBatch";

        /// <summary>
        /// Procedure kiểm tra mã nhà cung cấp có trùng hay không
        /// </summary>
        public const string PROVIDER_CHECK_DUPLICATE_CODE = "Proc_provider_CheckDuplicateProviderCode";

        /// <summary>
        /// Procedure kiểm tra mã số thuế có trùng không
        /// </summary>
        public const string PROVIDER_CHECK_DUPLICATE_TAXCODE = "Proc_provider_CheckDuplicateTaxCode";

        /// <summary>
        /// Procedure lấy mã nhà cung cấp lớn nhất
        /// </summary>
        public const string PROVIDER_GET_PROVIDERCODE_MAX = "Proc_provider_GetProviderCoderMax";

        /// <summary>
        /// Procedure lọc theo nhiều điều kiện
        /// </summary>
        public const string PROVIDER_FILTER_BY_MULTIPLE_CONDITIONS = "Proc_provider_FilterByMultipleConditions";

        /// <summary>
        /// Procedure đếm tổng số bản ghi hợp lệ theo các điều kiện truyền vào
        /// </summary>
        public const string PROVIDER_GET_TOTAL_PROVIDER_BY_FILTER = "Proc_provider_GetTotalProviderByFilter";
        #endregion

        #region PROVIDER_PROVIDERGROUP
        /// <summary>
        /// Procedure thêm nhiều nhóm nhà cung cấp của 1 nhà cung cấp, 1 nhà cung cấp có thể thuộc nhiều nhóm nhà cung cấp, nên phải thêm nhiều
        /// </summary>
        public const string INSERT_BATCH_PROVIDER_PROVIDERGROUP = "Proc_providerprovidergroup_InsertBatch";

        /// <summary>
        /// Procedure lấy tất cả nhóm nhà cung cấp mà 1 nhà cung cấp thuộc về
        /// </summary>
        public const string GETALL_PROVIDERGROUP_BY_PROVIDERID = "Proc_providerprovidergroup_GetAll_By_ProviderID";

        /// <summary>
        /// Procedure xóa tất cả nhóm nhà cung cấp thuộc một nhà cung cấp
        /// </summary>
        public const string DELETE_BATCH_PROVIDERGROUP_BY_PROVIDERID = "Proc_providerprovidergroup_Delete";
        #endregion

        /// <summary>
        /// Procedure lấy danh sách các tài khoản ngân hàng của 1 nhà cung cấp
        /// </summary>
        public const string GET_BANKACCOUNTPROVIDER_BY_PROVIDERID = "Proc_bankaccountprovider_GetAll";

        /// <summary>
        /// Procedure lấy danh sách địa chỉ khác của 1 nhà cung cấp
        /// </summary>
        public const string GET_ANOTHERADDRESS_BY_PROVIDERID = "Proc_anotheraddressofprovider_GetAll";

        /// <summary>
        /// Procedure xóa các địa chỉ khác của một nhà cung cấp
        /// </summary>
        public const string DELETE_ANOTHERADDRESS_BY_PROVIDERID = "Proc_anotheraddressofprovider_Delete";

        /// <summary>
        /// Procedure xóa các tài khoản ngân hàng của một nhà cung cấp
        /// </summary>
        public const string DELTETE_BANKACCOUNTPROVIDER_BY_PROVIDERID = "Proc_bankaccountprovider_Delete";

    }

}
