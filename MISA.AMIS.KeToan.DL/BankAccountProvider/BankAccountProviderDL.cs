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
    public class BankAccountProviderDL:BaseDL<BankAccountProvider>, IBankAccountProviderDL
    {
        /// <summary>
        /// API lấy các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn lấy</param>
        /// <returns>Danh sách các tài khoản ngân hàng của một nhà cung cấp</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public List<BankAccountProvider> GetAllRecordByProviderID(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName =Procedure.GET_BANKACCOUNTPROVIDER_BY_PROVIDERID;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@ProviderID", providerID);

            //Khởi tạo kết nối tới DB MySQL
            var records = new List<BankAccountProvider>();
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB, dùng dapper
                records = (List<BankAccountProvider>)mySqlConnection.Query<BankAccountProvider>(storeProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
            }
            //Xử lý kết quả khi DB trả về kết quả
            //Nếu thành công thì trả về dữ liệu cho BL
            return records;
        }

        /// <summary>
        /// Thêm nhiều tài khoản ngân hàng của một nhà cung cấp
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (27/12/2022)
        public bool InsertBatch(Guid providerID, List<BankAccountProvider> listData)
        {
            //Khai báo 1 biến đếm
            int count = 0;
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.INSERT, "BankAccountProvider");
            foreach (var record in listData)
            {
                //Chuẩn bị tham số đầu vào
                record.GetType().GetProperty("ProviderID").SetValue(record, providerID);
                record.GetType().GetProperty($"{typeof(BankAccountProvider).Name}ID").SetValue(record, Guid.NewGuid());

                //Khởi tạo kết nối tới DB MySQL
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    //Thực hiện gọi vào DB
                    int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, record, commandType: System.Data.CommandType.StoredProcedure);
                    if (numberOfRowsAffected > 0)
                        count++;
                    else
                        return false;
                }
            }
            if (count == listData.Count)
                return true;
            else return false;

            //    string ConnectionString = "server=192.168.1xxx";
            //    StringBuilder sCommand = new StringBuilder("INSERT INTO User (FirstName, LastName) VALUES ");
            //    //using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
            //    using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))

            //    {
            //        List<string> Rows = new List<string>();
            //        for (int i = 0; i < 100000; i++)
            //        {
            //            Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString("test"), MySqlHelper.EscapeString("test")));
            //        }
            //        sCommand.Append(string.Join(",", Rows));
            //        sCommand.Append(";");
            //        mySqlConnection.Open();
            //        using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mySqlConnection))
            //        {
            //            myCmd.CommandType = CommandType.Text;
            //            myCmd.ExecuteNonQuery();
            //        }
            //    }
        }

        /// <summary>
        /// API xóa các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteARecord(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.DELTETE_BANKACCOUNTPROVIDER_BY_PROVIDERID;
            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@ProviderID", providerID);

            //Chạy câu lệnh SQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName,
                parameter, commandType: System.Data.CommandType.StoredProcedure);
                //Xử lý kết quả trả về 
                return providerID;
            }
        }

    }
}
