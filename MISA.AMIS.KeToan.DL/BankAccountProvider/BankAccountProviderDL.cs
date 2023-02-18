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
            //1. Chuẩn hóa List tài khoản ngân hàng để đưa vào DB.
            foreach (var bankAccount in listData)
            { 
                bankAccount.ProviderID = providerID;
                bankAccount.BankAccountProviderID = Guid.NewGuid();
            }

            //2. Sau khi chuẩn hóa thì thực hiện chạy câu lệnh Insert
                //Khởi tạo kết nối tới DB MySQL
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                mySqlConnection.Open();
                var trans = mySqlConnection.BeginTransaction();
                try
                {

                    mySqlConnection.Execute(@"insert bankaccountprovider(BankAccountProviderID, ProviderID, BankAccountNumber, BankAccountName, BankBranch, OpenedAt, CreatedDate) 
                            values(@BankAccountProviderID, @ProviderID, @BankAccountNumber, @BankAccountName, @BankBranch, @OpenedAt, @CreatedDate)", listData, transaction: trans);

                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }

        }

        /// <summary>
        /// API xóa các tài khoản ngân hàng của 1 nhà cung cấp được chọn
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteRecords(Guid providerID)
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
