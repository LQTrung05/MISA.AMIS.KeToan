using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IProviderBL:IBaseBL<Provider>
    {
        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging SearchAndPagingProvider(string? keyword, int limit = 20, int pageNumber = 1);

        /// <summary>
        /// Xóa nhiều nhà cung cấp theo danh sách ID truyền vào
        /// </summary>
        /// <param name="providerIDList">Danh sách ID của các nhà cung cấp muốn xóa</param>
        /// <returns>Số nhà cung cấp bị xóa thành công</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        public bool DeleteRecords(List<Guid> providerIDList);

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhà cung lớn nhất đã tăng lên 1 đơn vị so với mã lớn nhất ở database</returns>
        /// CreatedBy: LQTrung (29/12/2022)
        public string GetProviderCodeMax();

        /// <summary>
        /// Xuất khẩu danh sách nhà cung cấp theo trang
        /// </summary>
        /// <returns>File excel chứa danh sách nhà cung cấp</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        //public MemoryStream ExportProviderExcelFile(string? keyword, int limit = 10, int pageNumber = 1);
        public MemoryStream ExportProviderExcelFile();
        /// <summary>
        /// Lọc nhà cung cấp theo nhiều điều kiện
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging FilterByMultipleConditions(int? providerType,
            string? province, string? district, string? village, string? providerGroupCode, int limit = 10, int pageNumber = 1);
    }
}
