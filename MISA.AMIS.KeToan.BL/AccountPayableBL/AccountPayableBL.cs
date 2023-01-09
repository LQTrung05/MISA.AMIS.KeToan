using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class AccountPayableBL:BaseBL<AccountPayable>, IAccountPayableBL
    {
        #region Field
        private IAccountPayableDL _accountPayableDL;
        #endregion

        #region Constructor
        public AccountPayableBL(IAccountPayableDL accountPayableDL) : base(accountPayableDL)
        {
            _accountPayableDL = accountPayableDL;
        }
        #endregion
    }
}
