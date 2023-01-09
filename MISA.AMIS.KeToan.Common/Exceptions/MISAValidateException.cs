using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Exceptions
{
    public class MISAValidateException: Exception
    {
        public List<string> ErrorDetails { get; set; }

        public MISAValidateException(List<string> errorDetails)
        {
            ErrorDetails = errorDetails;
        }

    }
}
