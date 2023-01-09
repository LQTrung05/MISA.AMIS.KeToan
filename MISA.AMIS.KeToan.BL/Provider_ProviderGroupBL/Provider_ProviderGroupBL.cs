using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class Provider_ProviderGroupBL:BaseBL<Provider_ProviderGroup>, IProvider_ProviderGroupBL
    {
        #region Field
        private IProvider_ProviderGroupDL _provider_ProviderGroupDL;
        #endregion

        #region Constructor

        public Provider_ProviderGroupBL(IProvider_ProviderGroupDL provider_ProviderGroupDL) : base(provider_ProviderGroupDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _provider_ProviderGroupDL = provider_ProviderGroupDL;
        }

        #endregion
        /// <summary>
        /// Thêm mới nhiều nhóm nhà cung cấp vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="listProviderGroupID">Danh sách các nhóm nhà cung cấp thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilProviderGroup(Guid providerID, List<Guid> listProviderGroupID)
        {
            return _provider_ProviderGroupDL.InsertMultilProviderGroup(providerID, listProviderGroupID);
        }

        /// <summary>
        /// Lấy danh sách các nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(15/12/2022)
        public IEnumerable<ProviderGroupListByProviderID> GetAllRecordByID(Guid providerID)
        {
            return _provider_ProviderGroupDL.GetAllRecordByID(providerID);

        }

        /// <summary>
        /// API xóa tất cả nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp </param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (27/12/2022)
        public int DeleteBatchProviderGroupByProviderID(Guid providerID)
        {
            return _provider_ProviderGroupDL.DeleteBatchProviderGroupByProviderID(providerID);
        }
    }
}
