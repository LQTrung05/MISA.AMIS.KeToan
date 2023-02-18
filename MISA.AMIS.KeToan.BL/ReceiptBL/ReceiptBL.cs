using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class ReceiptBL:BaseBL<Receipt>, IReceiptBL
    {
        #region Field
        private IReceiptDL _receiptDL;
        #endregion

        #region Constructor
        public ReceiptBL(IReceiptDL receiptDL) : base(receiptDL)
        {
            _receiptDL = receiptDL;
        }
        #endregion

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
            return _receiptDL.SearchAndPagingReceipt(keyword, limit, pageNumber);
        }

    }
}
