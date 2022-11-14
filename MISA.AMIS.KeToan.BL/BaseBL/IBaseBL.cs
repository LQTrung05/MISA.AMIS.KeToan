using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(11/11/2022)
        public IEnumerable<T> GetAllRecord();

        /// <summary>
        /// Lấy thông tin chi tiết 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy thông tin</param>
        /// <returns>Thông tin chi tiết 1 bản ghi được chọn</returns>
        /// Created by: LQTrung(11/11/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (12/11/2022)
        public Guid InsertARecord(T record);

        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="employeeID">ID của bản ghi sẽ sửa</param>
        /// <param name="employee">Đối tượng bản ghi sẽ sửa</param>
        /// <returns>ID của bản ghi vừa sửa xong</returns>
        /// CreatedBy: LQTrung (1/11/2022)
        public Guid UpdateARecord(Guid recordID, T record);

        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public int DeleteARecord(Guid recordID);
    }
}
