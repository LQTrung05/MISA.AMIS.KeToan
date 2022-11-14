using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Constants
{
    public class Procedure
    {
        //Rule viết tên biến constant trong c# là như sau

        /// <summary>
        /// Formant tên procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Format tên procedure lấy 1 bản ghi theo id
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetDetailTheEmployee";

        /// <summary>
        /// Format tên procedure thêm mới 1 bản ghi
        /// </summary>
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// Format tên procedure chỉnh sửa 1 bản ghi
        /// </summary>
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// Format tên procedure xóa 1 bản ghi
        /// </summary>
        public static string DELETE = "Proc_{0}_Delete";
    }
}
