using Microsoft.Exchange.WebServices.Data;

using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        //Đối với tên các field, thường sử dụng _ ở dưới biến để thể hiện nó private
        private IEmployeeDL _employeeDL;
        
        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public int DeleteRecords( ListEmployeeID listEmployeeID)
        {
            return _employeeDL.DeleteRecords(listEmployeeID);   
        }

        //private ServiceResponse ValidateRequestData(Employee employee)
        //{
        //    //Lấy danh sách các thuộc tính 
        //    var properties = typeof(EmployeeBL).GetProperties();
        //    var validateFailures = new List<string>();
        //    foreach (var property in properties)
        //    {
        //        //Lấy giá trị của từng thuộc tính
        //        var propertyValue = property.GetValue(employee);
        //        var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));

        //        //Kiểm tra xem thuộc tính đó có Required hay không, nếu Required thì xem nó có trống không
        //        if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
        //        {
        //            validateFailures.Add(requiredAttribute.ErrorMessage);
        //        }
        //    }
        //    if (validateFailures.Count > 0)
        //    {
        //        return new ServiceResponse
        //        {
        //            Result = false,
        //            ErrorDetails = validateFailures
        //        };
        //    }
        //    return new ServiceResponse
        //    {
        //        Success = "true",

        //    };
        //}




    }
}
