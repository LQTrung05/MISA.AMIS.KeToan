﻿using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Hàm lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LQTrung(11/11/2022)
        public IEnumerable<T> GetAllRecord()
        {
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            //Khởi tạo kết nối tới DB MySQL
            var records = new List<T>();
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB, dùng dapper
                records = (List<T>)mySqlConnection.Query<T>(storeProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }
            //Xử lý kết quả khi DB trả về kết quả
            //Nếu thành công thì trả về dữ liệu cho FE
            return records;
        }

        /// <summary>
        /// Lấy thông tin chi tiết 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy thông tin</param>
        /// <returns>Thông tin chi tiết 1 bản ghi được chọn</returns>
        /// Created by: LQTrung(11/11/2022)
        public T GetRecordByID(Guid recordID)
        {
            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.GET_BY_ID, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add($"@{typeof(T).Name}ID", recordID);

            //Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thuc hien goi vao DB
                var record = mySqlConnection.QueryFirstOrDefault<T>(storeProcedureName, parameter, commandType: System.Data.CommandType.StoredProcedure);
                return record;

            }
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi cần thêm</param>
        /// <returns>ID của bản ghi vừa thêm</returns>
        /// Created by: LQTrung (12/11/2022)
        public Guid InsertARecord(T record)
        {

            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.INSERT, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                parameters.Add($"@{property.Name}", propertyValue);
            }

            var recordID = Guid.NewGuid();
            parameters.Add($"@{typeof(T).Name}ID", recordID);
            //Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, record, commandType: System.Data.CommandType.StoredProcedure);
                return recordID;
            }

        }

        /// <summary>
        /// Sửa thông tin một bản ghi
        /// </summary>
        /// <param name="employeeID">ID của bản ghi sẽ sửa</param>
        /// <param name="employee">Đối tượng bản ghi sẽ sửa</param>
        /// <returns>ID của bản ghi vừa sửa xong</returns>
        /// CreatedBy: LQTrung (1/11/2022)
        public Guid UpdateARecord(
             Guid recordID,
             T record)
        {

            //Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.UPDATE, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                parameters.Add($"@{property.Name}", propertyValue);
            }
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            //Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB
                var numberOfRowsAffected = mySqlConnection.QueryFirstOrDefault(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return recordID;
            }

        }

        /// <summary>
        /// API xóa 1 bản ghi theo ID được chọn
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn xóa</param>
        /// <returns>số bản ghi vừa xóa</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public int DeleteARecord(Guid recordID)
        {

            //Chuẩn bị câu lệnh SQL
            var storeProcedureName = String.Format(Procedure.DELETE, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameter = new DynamicParameters();
            parameter.Add($"@{typeof(T).Name}ID", recordID);

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

