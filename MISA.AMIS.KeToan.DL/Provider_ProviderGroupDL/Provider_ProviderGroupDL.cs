using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class Provider_ProviderGroupDL:BaseDL<Provider_ProviderGroup>,IProvider_ProviderGroupDL
    {
        /// <summary>
        /// Thêm mới nhiều nhóm nhà cung cấp vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="listProviderGroupID">Danh sách các nhóm nhà cung cấp thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilProviderGroup(Guid providerID, List<Guid> listProviderGroupID)
        {
            var listRemove = "";
            foreach (var item in listProviderGroupID)
            {
                if (listRemove == "")
                {
                    listRemove += item;
                }
                else
                {
                    listRemove += "," + item ;
                }
            }
            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@ProviderID", providerID);
            parameters.Add("@DataList", listRemove);

            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.INSERT_BATCH_PROVIDER_PROVIDERGROUP;

            //Khởi tạo kết nối tới DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var trans = mySqlConnection.BeginTransaction();
                try
                {
                    var result = mySqlConnection.Execute(storeProcedureName, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        trans.Commit();
                        return true;
                    }
                    else
                    {
                        trans.Rollback();
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    //Return false ở đây thì lỗi sẽ k đc hiển thị ra đâu
                    var error = new Error()
                    {
                        DevMsg = ex.Message,
                        UsersMsg = ResourceVN.Error_not_insert_batch,
                    };
                    trans.Rollback();
                    return false;

                }
            }

        }

        /// <summary>
        /// Lấy danh sách các nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(15/12/2022)
        public IEnumerable<ProviderGroupListByProviderID> GetAllRecordByID(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.GETALL_PROVIDERGROUP_BY_PROVIDERID;

            //Khởi tạo kết nối tới DB MySQL
            var records = new List<ProviderGroupListByProviderID>();

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@ProviderID", providerID);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB, dùng dapper
                records = (List<ProviderGroupListByProviderID>)mySqlConnection.Query<ProviderGroupListByProviderID>(storeProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
            }
            //Xử lý kết quả khi DB trả về kết quả
            //Nếu thành công thì trả về dữ liệu cho FE
            return records;
        }

        /// <summary>
        /// API xóa tất cả nhóm nhà cung cấp thuộc 1 nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp </param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (27/12/2022)
        public int DeleteBatchProviderGroupByProviderID(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.DELETE_BATCH_PROVIDERGROUP_BY_PROVIDERID;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add($"@ProviderID", providerID);

            //Chạy câu lệnh SQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName,
                parameter, commandType: System.Data.CommandType.StoredProcedure);
                //Xử lý kết quả trả về 
                return numberOfRowsAffected;
            }
        }
    }
}
