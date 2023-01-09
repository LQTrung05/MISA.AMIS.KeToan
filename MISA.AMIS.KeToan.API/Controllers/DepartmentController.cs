using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class DepartmentController : BaseController<Department>
    {
        #region Field
        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor
        public DepartmentController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }
        #endregion
    }
}
