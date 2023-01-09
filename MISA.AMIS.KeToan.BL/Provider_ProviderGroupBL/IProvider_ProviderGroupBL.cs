using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public interface IProvider_ProviderGroupBL:IBaseBL<Provider_ProviderGroup>
    {
        /// <summary>
        /// Thêm mới nhiều nhóm nhà cung cấp vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="listProviderGroupID">Danh sách các nhóm nhà cung cấp thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilProviderGroup(Guid providerID, List<Guid> listProviderGroupID);

        /// <summary>
        /// Lấy danh sách các nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(15/12/2022)
        public IEnumerable<ProviderGroupListByProviderID> GetAllRecordByID(Guid providerID);

        /// <summary>
        /// API xóa tất cả nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp </param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (27/12/2022)
        public int DeleteBatchProviderGroupByProviderID(Guid providerID);
    }
}
