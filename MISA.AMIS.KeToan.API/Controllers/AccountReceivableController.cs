using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountReceivableController : BaseController<AccountReceivable>
    {
        #region Field
        private IAccountReceivableBL _accountReceivableBL;

        #endregion

        #region Constructor
        public AccountReceivableController(IAccountReceivableBL accountReceivableBL) : base(accountReceivableBL)
        {
            _accountReceivableBL = accountReceivableBL;
        }
        #endregion
    }
}
