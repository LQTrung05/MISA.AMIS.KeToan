using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProviderGroupController : BaseController<ProviderGroup>
    {
        #region Field
        private IProviderGroupBL _providerGroupBL;

        #endregion

        #region Constructor
        public ProviderGroupController(IProviderGroupBL providerGroupBL) : base(providerGroupBL)
        {
            _providerGroupBL = providerGroupBL;
        }
        #endregion
    }
}
