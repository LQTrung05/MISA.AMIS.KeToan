using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class ProviderGroupBL:BaseBL<ProviderGroup>, IProviderGroupBL
    {
        #region Field
        private IProviderGroupDL _providerGroupDL;
        #endregion

        #region Constructor

        public ProviderGroupBL(IProviderGroupDL providerGroupDL) : base(providerGroupDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _providerGroupDL = providerGroupDL;
        }
       
        #endregion
    }
}
