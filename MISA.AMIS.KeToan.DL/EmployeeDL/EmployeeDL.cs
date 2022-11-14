using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : BaseDL<Employee>,  IEmployeeDL
    {
        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        public int DeleteRecords(ListEmployeeID listEmployeeID)
        {
            //Khởi tạo kết nối tới DB
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            mySqlConnection.Open();
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = String.Format(Procedure.DELETE, typeof(Employee).Name);

            var trans = mySqlConnection.BeginTransaction();

            try
            {
                //Chuẩn bị tham số đầu vào
                foreach (var employeeID in listEmployeeID.EmployeeIDs)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@EmployeeID", employeeID);
                    mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
                }
                trans.Commit();
                //Trả về true false hoặc danh sách id bị xóa
                return listEmployeeID.EmployeeIDs.Count;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                var error = new Error()
                {
                    DevMsg = ex.Message,
                    UsersMsg = "Có lỗi, không thể xóa hàng loạt bản ghi"
                };
                //return ShowException(ex);
                return 0;

            }
        }
    }
}
