using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.Common.MISAAttributes;
using MISA.AMIS.KeToan.DL;

namespace MISA.AMIS.KeToan.BL
{
    public class BaseBL<T> :IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(11/11/2022)
        public IEnumerable<T> GetAllRecord()
        {
            return _baseDL.GetAllRecord();
        }

        /// <summary>
        /// Lấy thông tin chi tiết 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy thông tin</param>
        /// <returns>Thông tin chi tiết 1 bản ghi được chọn</returns>
        /// Created by: LQTrung(11/11/2022)
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (12/11/2022)
        public Guid InsertARecord(T record)
        {
            //Validate dữ liệu
            //Validate phần chung
            var validateResult = ValidateRequestData(record);
            if(!validateResult.Success)
            {
                throw new MISAValidateException(validateResult.ErrorDetails);
            }
            //Validate phần riêng
            ValidateSeparate(record);

            return _baseDL.InsertARecord(record);
        }

        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="employeeID">ID của bản ghi sẽ sửa</param>
        /// <param name="employee">Đối tượng bản ghi sẽ sửa</param>
        /// <returns>ID của bản ghi vừa sửa xong</returns>
        /// CreatedBy: LQTrung (1/11/2022)
        public Guid UpdateARecord( Guid recordID, T record)
        {
            //Validate dữ liệu
            //Validate phần chung
            var validateResult = ValidateRequestData(record);
            if (!validateResult.Success)
            {
                throw new MISAValidateException(validateResult.ErrorDetails);
            }
            record.GetType().GetProperty($"{typeof(T).Name}ID").SetValue(record, recordID);

            //Validate phần riêng
            ValidateSeparate(record);
            return _baseDL.UpdateARecord(recordID, record);
        }

        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public int DeleteARecord(Guid recordID)
        {
            return _baseDL.DeleteARecord(recordID);   
        }

        /// <summary>
        /// Hàm validate chung dữ liệu nhập vào
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// /// CreatedBy: LQTrung (12/11/2022)
        private ServiceResponse ValidateRequestData(T record)
        {
            //Lấy danh sách các thuộc tính 
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                //Lấy giá trị của từng thuộc tính
                var propertyValue = property.GetValue(record);
                //Lấy các thuộc tính có Attribute [Require]
                var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
                //Lấy các thuộc tính có Attribute [RegularExpression]
                var regularExpressionAttribute = (RegularExpressionAttribute?)Attribute.GetCustomAttribute(property, typeof(RegularExpressionAttribute));
                //Lấy các thuộc tính có Attribute [DateTimeValid]
                var dateTimeValide = (DateTimeValid?)Attribute.GetCustomAttribute(property, typeof(DateTimeValid));
                
                if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
                {
                    validateFailures.Add(requiredAttribute.ErrorMessage);
                }
                if( regularExpressionAttribute != null && !regularExpressionAttribute.IsValid(propertyValue?.ToString() ))
                {
                    validateFailures.Add(regularExpressionAttribute.ErrorMessage);
                }
                if (dateTimeValide != null && !dateTimeValide.IsValid(propertyValue?.ToString()))
                {
                    validateFailures.Add(dateTimeValide.ErrorMessage);
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
            return new ServiceResponse
            {
                Success = true
            };

        }

        /// <summary>
        /// Hàm validate dữ liệu riêng
        /// </summary>
        /// <param name="record"></param>
        /// CreatedBy: LQTrung (12/11/2022)
        protected virtual void ValidateSeparate(T record)
        {

        }
    }
}
