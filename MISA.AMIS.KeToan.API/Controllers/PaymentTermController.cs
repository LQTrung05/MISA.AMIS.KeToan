using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentTermController : BaseController<PaymentTerm>
    {
        #region Field
        private IPaymentTermBL _paymentTermBL;
        #endregion

        #region Constructor
        public PaymentTermController(IPaymentTermBL paymentTermBL) : base(paymentTermBL)
        {
            _paymentTermBL = paymentTermBL;
        }
        #endregion
    }
}
