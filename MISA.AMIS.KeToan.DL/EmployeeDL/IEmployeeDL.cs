using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    //internal là trong cùng 1 project thì mới có thể truy cập được vào interface này thôi
    // thế nên nếu muốn các project khác cũng có thể truy cập được vào thì dùng public, vì sau này tầng DL có tương tác 
    // với cả tầng BL nữa mà, cả project Common nữa
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        public int DeleteRecords(ListEmployeeID listEmployeeID);
    }
}
