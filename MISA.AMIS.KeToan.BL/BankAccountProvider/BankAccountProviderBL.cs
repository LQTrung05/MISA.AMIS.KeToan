using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            var validateData = ValidateRequiredData(listData);
            if (!validateData.Success)
            {
                throw new MISAValidateException(validateData.ErrorDetails);
            }
            return _bankAccountProviderDL.InsertBatch(providerID, listData);

        }
        /// <summary>
        /// Hàm validate danh sách tài khoản ngân hàng trước khi thêm vào database
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// /// CreatedBy: LQTrung (12/11/2022)
        private ServiceResponse ValidateRequiredData(List<BankAccountProvider> bankAccountList)
        {
            //Lấy danh sách các thuộc tính 
            foreach(var bankAccount in bankAccountList)
            {
                var properties = typeof(BankAccountProvider).GetProperties();
                var validateFailures = new List<string>();
                foreach (var property in properties)
                {
                    //Lấy giá trị của từng thuộc tính
                    var propertyValue = property.GetValue(bankAccount);
                    //Lấy các thuộc tính có Attribute [Require]
                    var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                    
                    if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                    {
                        validateFailures.Add(requiredAttribute.ErrorMessage);
                    }
                }
                if (validateFailures.Count > 0)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        ErrorDetails = validateFailures
                    };
                }
            }
            
            return new ServiceResponse
            {
                Success = true
            };

        }

        /// <summary>
        /// API xóa các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteRecords(Guid providerID)
        {
            return _bankAccountProviderDL.DeleteRecords(providerID);    
        }
    }
}
