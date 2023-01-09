using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountPayableController : BaseController<AccountPayable>
    {
        #region Field
        private IAccountPayableBL _accountPayableBL;

        #endregion

        #region Constructor
        public AccountPayableController(IAccountPayableBL accountPayableBL) : base(accountPayableBL)
        {
            _accountPayableBL = accountPayableBL;
        }
        #endregion
    }
}
