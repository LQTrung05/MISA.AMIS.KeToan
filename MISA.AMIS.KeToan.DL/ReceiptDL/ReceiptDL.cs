using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class ReceiptDL: BaseDL<Receipt>, IReceiptDL
    {
        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging SearchAndPagingReceipt(string? keyword, int limit = 20, int pageNumber = 1)
        {
            //Khởi tạo kết nối tới DB MySQL
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            //Chuẩn bị câu lệnh SQL
            string procedureSearchAndPaging = String.Format(Procedure.SEARCH_AND_PAGING, "receipt");
            string procedureGetTotalReceipt = String.Format(Procedure.GET_TOTAL_RECORD, "receipt");

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@keyword", keyword);
            //Lấy limit bản ghi trong 1 trang
            parameters.Add("@Limit", limit);
            //Lấy từ trang số bao nhiêu
            int offset = limit * (pageNumber - 1);
            parameters.Add("@Offset", offset);

            var parameter = new DynamicParameters();
            parameter.Add("@keyword", keyword);


            // Thuc hien goi vao DB
            var receiptList = mySqlConnection.Query(procedureSearchAndPaging, parameters, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = mySqlConnection.QueryFirstOrDefault<int>(procedureGetTotalReceipt, parameter, commandType: System.Data.CommandType.StoredProcedure);

            int totalPage = 0;
            totalPage = (totalRecord % limit == 0) ? (totalRecord / limit) : (totalRecord / limit + 1);

            var result = new SearchAndPaging()
            {
                TotalRecord = totalRecord,
                TotalPage = totalPage,
                Data = receiptList
            };
            return result;
        }

    }
}
