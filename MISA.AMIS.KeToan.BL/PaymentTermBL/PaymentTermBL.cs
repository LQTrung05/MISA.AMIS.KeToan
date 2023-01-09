using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class PaymentTermBL:BaseBL<PaymentTerm>, IPaymentTermBL
    {
        #region Field
        private IPaymentTermDL _paymentTermDL;
        #endregion

        #region Constructor

        public PaymentTermBL(IPaymentTermDL paymentTermDL) : base(paymentTermDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _paymentTermDL = paymentTermDL;
        }

        #endregion
    }
}
