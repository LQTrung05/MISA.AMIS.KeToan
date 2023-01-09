using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities.DTO
{
    public class SearchAndPaging
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Tổng số trang 
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// Danh sách các bản ghi trong 1 trang
        /// </summary>
        public IEnumerable<dynamic> Data { get; set; }
        
    }
}
