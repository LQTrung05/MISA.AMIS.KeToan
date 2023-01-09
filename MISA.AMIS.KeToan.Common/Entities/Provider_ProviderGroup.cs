using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class Provider_ProviderGroup
    {
        /// <summary>
        /// ID của bảng nhà cung cấp
        /// </summary>
        public Guid ProviderID { get; set; }

        /// <summary>
        /// ID của nhóm nhà cung cấp
        /// </summary>
        public Guid ProviderGroupID { get; set; }
    }
}
