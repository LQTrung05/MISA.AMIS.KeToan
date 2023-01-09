using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class AccountReceivableBL:BaseBL<AccountReceivable>, IAccountReceivableBL
    {
        #region Field
        private IAccountReceivableDL _accountReceivableDL;
        #endregion

        #region Constructor
        public AccountReceivableBL(IAccountReceivableDL accountPayableDL) : base(accountPayableDL)
        {
            _accountReceivableDL = accountPayableDL;
        }
        # endregion
    }
}
