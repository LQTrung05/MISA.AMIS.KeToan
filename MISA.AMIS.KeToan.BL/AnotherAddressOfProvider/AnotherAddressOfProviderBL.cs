using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class AnotherAddressOfProviderBL:BaseBL<AnotherAddressOfProvider>, IAnotherAddressOfProviderBL
    {
        #region Field
        private IAnotherAddressOfProviderDL _anotherAddressOfProviderDL;
        #endregion

        #region Constructor


        /// <summary>
        /// Lấy danh sách các địa chỉ khác của một nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <returns>Danh sách các địa chỉ khác</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public AnotherAddressOfProviderBL(IAnotherAddressOfProviderDL anotherAddressOfProviderDL) : base(anotherAddressOfProviderDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _anotherAddressOfProviderDL = anotherAddressOfProviderDL;
        }

        #endregion
        public List<AnotherAddressOfProvider> GetAllRecordByProviderID(Guid providerID)
        {
            return _anotherAddressOfProviderDL.GetAllRecordByProviderID(providerID);
        }
        /// <summary>
        /// Thêm mới nhiều địa chỉ khác vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="anotherAddressList">Danh sách địa chỉ khác thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilAnotherAddress(Guid providerID, List<AnotherAddressOfProvider> listData)
        {
            return _anotherAddressOfProviderDL.InsertMultilAnotherAddress(providerID, listData);
        }
        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteARecord(Guid providerID)
        {
            return _anotherAddressOfProviderDL.DeleteARecord(providerID);
        }
    }
}
