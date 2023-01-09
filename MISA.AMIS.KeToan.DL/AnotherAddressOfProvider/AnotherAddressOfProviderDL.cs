﻿using Dapper;
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
    public class AnotherAddressOfProviderDL:BaseDL<AnotherAddressOfProvider>, IAnotherAddressOfProviderDL
    {
        /// <summary>
        /// Lấy danh sách các địa chỉ khác của một nhà cung cấp
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <returns>Danh sách các địa chỉ khác</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public List<AnotherAddressOfProvider> GetAllRecordByProviderID(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.GET_ANOTHERADDRESS_BY_PROVIDERID;

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add("@ProviderID", providerID);

            //Khởi tạo kết nối tới DB MySQL
            var records = new List<AnotherAddressOfProvider>();
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB, dùng dapper
                records = (List<AnotherAddressOfProvider>)mySqlConnection.Query<AnotherAddressOfProvider>(storeProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
            }
            //Xử lý kết quả khi DB trả về kết quả
            //Nếu thành công thì trả về dữ liệu cho BL
            return records;
        }

        /// <summary>
        /// Thêm mới nhiều địa chỉ khác vào một nhà cung cấp 
        /// </summary>
        /// <param name="providerID">ID của nhà cung cấp</param>
        /// <param name="anotherAddressList">Danh sách địa chỉ khác thuộc nhà cung cấp</param>
        /// <returns></returns>
        public bool InsertMultilAnotherAddress(Guid providerID, List<AnotherAddressOfProvider> listData)
        {
            //Khai báo 1 biến đếm
            int count = 0;
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.INSERT, "AnotherAddressOfProvider");
            foreach (var record in listData)
            {
                //Chuẩn bị tham số đầu vào
                record.GetType().GetProperty("ProviderID").SetValue(record, providerID);
                record.GetType().GetProperty($"{typeof(AnotherAddressOfProvider).Name}ID").SetValue(record, Guid.NewGuid());

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

            
        }


        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (24/12/2022)
        public Guid DeleteARecord(Guid providerID)
        {
            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = Procedure.DELETE_ANOTHERADDRESS_BY_PROVIDERID;

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
