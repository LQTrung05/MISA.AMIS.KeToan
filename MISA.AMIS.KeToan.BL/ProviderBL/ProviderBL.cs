using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.DL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL
{
    public class ProviderBL:BaseBL<Provider>, IProviderBL
    {
        #region Field
        private IProviderDL _providerDL;
        #endregion

        #region Constructor
        public ProviderBL(IProviderDL providerDL) : base(providerDL)
        {
            _providerDL = providerDL;
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
        public SearchAndPaging SearchAndPagingProvider(string? keyword, int limit = 20, int pageNumber = 1)
        {
            return _providerDL.SearchAndPagingProvider(keyword, limit, pageNumber);
        }
        
        /// <summary>
        /// Xóa nhiều nhà cung cấp theo danh sách ID truyền vào
        /// </summary>
        /// <param name="providerIDList">Danh sách ID của các nhà cung cấp muốn xóa</param>
        /// <returns>Số nhà cung cấp bị xóa thành công</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        public bool DeleteRecords(List<Guid> providerIDList)
        {
            return _providerDL.DeleteRecords(providerIDList);
        }

        /// <summary>
        /// Hàm validate dữ liệu riêng
        /// </summary>
        /// <param name="record"></param>
        /// CreatedBy: LQTrung (29/12/2022)
        protected override void ValidateSeparate(Provider provider)
        {
            var isDuplicateProviderCode = _providerDL.CheckUpDuplicateCode(provider.ProviderID, provider.ProviderCode);
            var isDuplicateTaxCode = _providerDL.CheckUpDuplicateTaxCode(provider.ProviderID, provider.TaxCode);

            var validateFailures = new List<string>();
            if (!isDuplicateProviderCode)
            {
                validateFailures.Add("Mã nhà cung cấp đã tồn tại, vui lòng kiểm tra lại.");
                var err = new ServiceResponse
                {
                    Success = false,
                    ErrorDetails = validateFailures
                };
                throw new MISAValidateException(err.ErrorDetails);
            }

            if (!isDuplicateTaxCode)
            {
                validateFailures.Add("Mã số thuế đã tồn tại, vui lòng kiểm tra lại.");
                var err = new ServiceResponse
                {
                    Success = false,
                    ErrorDetails = validateFailures
                };
                throw new MISAValidateException(err.ErrorDetails);
            }
        }

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhà cung lớn nhất đã tăng lên 1 đơn vị so với mã lớn nhất ở database</returns>
        /// CreatedBy: LQTrung (29/12/2022)
        public string GetProviderCodeMax()
        {
            return _providerDL.GetProviderCodeMax();
        }

        /// <summary>
        /// Xuất khẩu danh sách nhà cung cấp theo trang
        /// </summary>
        /// <returns>File excel chứa danh sách nhà cung cấp</returns>
        /// CreatedBy: LQTrung(5/1/2023)
        //public MemoryStream ExportProviderExcelFile(string? keyword, int limit = 10, int pageNumber = 1)
        //{
        //    var list = SearchAndPagingProvider(keyword,limit,pageNumber);
        //    var stream = new MemoryStream();
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    using (var package = new ExcelPackage(stream))
        //    {
        //        var workSheet = package.Workbook.Worksheets.Add("Sheet1");

        //        ///đặt tên cho sheet
        //        workSheet.Name = "DANH SÁCH NHÀ CUNG CẤP";
        //        // fontsize mặc định cho cả sheet
        //        workSheet.Cells.Style.Font.Size = 11;
        //        // font family mặc định cho cả sheet
        //        workSheet.Cells.Style.Font.Name = "Times New Roman";

        //        string[] arrColumnHeader = {
        //            "STT",
        //            "Mã nhà cung cấp",
        //            "Tên nhà cung cấp",
        //            "Mã số thuế",
        //            "Địa chỉ",
        //            "Số điện thoại",
        //            "Đại diện theo pháp luật",
        //            "Số ngày được nợ",
        //            "Nợ tối đa"
        //        };
        //        // lấy ra số lượng cột cần dùng dựa vào số lượng header
        //        var countColHeader = arrColumnHeader.Count();

        //        // gán tiêu đề cho cell đầu tiên 
        //        workSheet.Cells[1, 1].Value = "DANH SÁCH NHÀ CUNG CẤP";
        //        // merge các column lại từ column 1 đến số column header
        //        workSheet.Cells[1, 1, 1, countColHeader].Merge = true;
        //        workSheet.Cells["A2:I2"].Merge = true;

        //        // style cho dòng title của sheet
        //        workSheet.Row(1).Height = 20;
        //        workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
        //        workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Size = 16;
        //        workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Name = "Arial";

        //        //style cho dòng title của bảng danh sách nhân viên
        //        workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        workSheet.Row(3).Style.Font.Bold = true;
        //        workSheet.Row(3).Style.Font.Name = "Arial";
        //        workSheet.Row(3).Style.Font.Size = 10;

        //        //Xét width cho các cột trong bảng
        //        int[] widthOfColumns = new int[] { 15, 32, 15, 20, 20, 25, 25, 25 };
        //        for (int i = 0; i < widthOfColumns.Length; i++)
        //        {
        //            workSheet.Column(i + 2).Width = widthOfColumns[i];
        //        }

        //        //Căn trái cột STT
        //        workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        //Căn giữa cột DateOfBirth
        //        workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
        //        workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


        //        // căn giữa ô title của Sheet
        //        workSheet.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        workSheet.Cells[1, 1, 1, countColHeader].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //        int colIndex = 1;
        //        int rowIndex = 3;

        //        //tạo các header từ column header đã tạo từ bên trên
        //        foreach (var item in arrColumnHeader)
        //        {
        //            var cell = workSheet.Cells[rowIndex, colIndex];

        //            //set màu thành gray
        //            var fill = cell.Style.Fill;
        //            fill.PatternType = ExcelFillStyle.Solid;
        //            fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(204, 204, 204));

        //            //căn chỉnh các border
        //            var border = cell.Style.Border;
        //            border.Bottom.Style =
        //                border.Top.Style =
        //                border.Left.Style =
        //                border.Right.Style = ExcelBorderStyle.Thin;
        //            //gán giá trị
        //            cell.Value = item;

        //            colIndex++;
        //        }

        //        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
        //        foreach (var item in list.Data)
        //        {
        //            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
        //            colIndex = 1;
        //            // rowIndex tương ứng từng dòng dữ liệu
        //            rowIndex++;

        //            //gán giá trị cho từng cell                      
        //            workSheet.Cells[rowIndex, colIndex++].Value = rowIndex - 3;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.ProviderCode;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.ProviderName;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.TaxCode;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.Address;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.Phone;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.LegalRepresentative;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.DueTime;
        //            workSheet.Cells[rowIndex, colIndex++].Value = item.MaximumDebt;
        //        }

        //        var modelCells = workSheet.Cells["A3"];
        //        var modelRows = list.Data.Count() + 3;
        //        string modelRange = "A3:I" + modelRows.ToString();
        //        var modelTable = workSheet.Cells[modelRange];

        //        // Tạo border cho bảng 
        //        modelTable.Style.Border.Top.Style = modelTable.Style.Border.Left.Style
        //                                          = modelTable.Style.Border.Right.Style
        //                                          = modelTable.Style.Border.Bottom.Style
        //                                          = ExcelBorderStyle.Thin;
        //        package.Save();
        //    }
        //    stream.Position = 0;
        //    return stream;
        //}

        public MemoryStream ExportProviderExcelFile()
        {
            var list = GetAllRecord();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                ///đặt tên cho sheet
                workSheet.Name = "DANH SÁCH NHÀ CUNG CẤP";
                // fontsize mặc định cho cả sheet
                workSheet.Cells.Style.Font.Size = 11;
                // font family mặc định cho cả sheet
                workSheet.Cells.Style.Font.Name = "Times New Roman";

                string[] arrColumnHeader = {
                    "STT",
                    "Mã nhà cung cấp",
                    "Tên nhà cung cấp",
                    "Mã số thuế",
                    "Địa chỉ",
                    "Số điện thoại",
                    "Đại diện theo pháp luật",
                    "Số ngày được nợ",
                    "Nợ tối đa"
                };
                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = arrColumnHeader.Count();

                // gán tiêu đề cho cell đầu tiên 
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÀ CUNG CẤP";
                // merge các column lại từ column 1 đến số column header
                workSheet.Cells[1, 1, 1, countColHeader].Merge = true;
                workSheet.Cells["A2:I2"].Merge = true;

                // style cho dòng title của sheet
                workSheet.Row(1).Height = 20;
                workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Size = 16;
                workSheet.Cells[1, 1, 1, countColHeader].Style.Font.Name = "Arial";

                //style cho dòng title của bảng danh sách nhân viên
                workSheet.Row(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(3).Style.Font.Bold = true;
                workSheet.Row(3).Style.Font.Name = "Arial";
                workSheet.Row(3).Style.Font.Size = 10;

                //Xét width cho các cột trong bảng
                int[] widthOfColumns = new int[] { 15, 32, 15, 20, 20, 25, 25, 25 };
                for (int i = 0; i < widthOfColumns.Length; i++)
                {
                    workSheet.Column(i + 2).Width = widthOfColumns[i];
                }

                //Căn trái cột STT
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //Căn giữa cột DateOfBirth
                workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Column(9).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                // căn giữa ô title của Sheet
                workSheet.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[1, 1, 1, countColHeader].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                int colIndex = 1;
                int rowIndex = 3;

                //tạo các header từ column header đã tạo từ bên trên
                foreach (var item in arrColumnHeader)
                {
                    var cell = workSheet.Cells[rowIndex, colIndex];

                    //set màu thành gray
                    var fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(204, 204, 204));

                    //căn chỉnh các border
                    var border = cell.Style.Border;
                    border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;
                    //gán giá trị
                    cell.Value = item;

                    colIndex++;
                }

                // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                foreach (var item in list)
                {
                    // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                    colIndex = 1;
                    // rowIndex tương ứng từng dòng dữ liệu
                    rowIndex++;

                    //gán giá trị cho từng cell                      
                    workSheet.Cells[rowIndex, colIndex++].Value = rowIndex - 3;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.ProviderCode;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.ProviderName;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.TaxCode;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.Address;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.Phone;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.LegalRepresentative;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.DueTime;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.MaximumDebt;
                }

                var modelCells = workSheet.Cells["A3"];
                var modelRows = list.Count() + 3;
                string modelRange = "A3:I" + modelRows.ToString();
                var modelTable = workSheet.Cells[modelRange];

                // Tạo border cho bảng 
                modelTable.Style.Border.Top.Style = modelTable.Style.Border.Left.Style
                                                  = modelTable.Style.Border.Right.Style
                                                  = modelTable.Style.Border.Bottom.Style
                                                  = ExcelBorderStyle.Thin;
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Lọc nhà cung cấp theo nhiều điều kiện
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy từ trang thứ mấy</param>
        /// <returns>
        /// 200:Danh sách các nhà cung cấp tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(25/12/2022)
        public SearchAndPaging FilterByMultipleConditions(int? providerType,
            string? province, string? district, string? village, string? providerGroupCode, int limit = 10, int pageNumber = 1)
        {
            return _providerDL.FilterByMultipleConditions(providerType, province, district, village, providerGroupCode,limit, pageNumber);

        }
    }
}
