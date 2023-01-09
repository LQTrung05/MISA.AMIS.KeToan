using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.MISAAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeValid : ValidationAttribute
    {
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Ghi đè hàm IsValid kiểm tra ngày tháng nhập vào lớn hơn ngày hiện tại không
        /// </summary>
        /// <param name="value">Ngày tháng nhập vào</param>
        /// <returns>
        /// true nếu ngày tháng nhập vào không lớn hơn ngày hiện tại
        /// false nếu ngày tháng nhập vào lớn hơn ngày hiện tại
        /// </returns>
        public override bool IsValid(object? value)
        {
            var dateTime = Convert.ToDateTime(value);
            bool result = true;
            if (dateTime != null)
            {
                if (dateTime > DateTime.Now)
                    result = false;
                else result = true;
            }
            return result;
        }
    }
}
