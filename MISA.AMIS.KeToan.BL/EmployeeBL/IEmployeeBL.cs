using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public bool DeleteRecords(List<Guid> listEmployeeID);

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy danh sách nhân viên ở trang số bao nhiêu</param>
        /// <returns>
        /// 200:Danh sách nhân viên tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public SearchAndPaging SearchAndPagingEmployee(string? keyword, int limit = 20, int pageNumber = 1);

        /// <summary>
        /// Lấy mã nhân viên lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public string GetEmployeeCodeMax();

        /// <summary>
        /// Xuất khẩu danh sách nhân viên
        /// </summary>
        /// <returns>File excel chứa danh sách nhân viên</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public MemoryStream ExportExcelFile();



    }
}
