using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class BankAccountProviderBL:BaseBL<BankAccountProvider>, IBankAccountProviderBL
    {
        #region Field
        private IBankAccountProviderDL _bankAccountProviderDL;
        #endregion

        #region Constructor

        public BankAccountProviderBL(IBankAccountProviderDL providerGroupDL) : base(providerGroupDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _bankAccountProviderDL = providerGroupDL;
        }
        #endregion

        /// <summary>
        /// API lấy các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn lấy</param>
        /// <returns>Danh sách các tài khoản ngân hàng của một nhà cung cấp</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public List<BankAccountProvider> GetAllRecordByProviderID(Guid providerID)
        {
            return _bankAccountProviderDL.GetAllRecordByProviderID(providerID);
        }

        /// <summary>
        /// Thêm nhiều tài khoản ngân hàng của một nhà cung cấp
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (27/12/2022)
        public bool InsertBatch(Guid providerID, List<BankAccountProvider> listData)
        {
            return _bankAccountProviderDL.InsertBatch(providerID, listData);

        }

        /// <summary>
        /// API xóa các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteARecord(Guid providerID)
        {
            return _bankAccountProviderDL.DeleteARecord(providerID);    
        }
    }
}
