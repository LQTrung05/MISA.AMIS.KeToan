using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IAnotherAddressOfProviderDL:IBaseDL<AnotherAddressOfProvider>
    {
        /// <summary>
        /// Lấy danh sách các địa chỉ khác của một nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <returns>Danh sách các địa chỉ khác</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public List<AnotherAddressOfProvider> GetAllRecordByProviderID(Guid providerID);

        /// <summary>
        /// Thêm mới nhiều địa chỉ khác vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="anotherAddressList">Danh sách địa chỉ khác thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilAnotherAddress(Guid providerID, List<AnotherAddressOfProvider> listData);

        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteARecord(Guid providerID);
    }
}
