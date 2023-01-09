using Microsoft.Exchange.WebServices.Data;

using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceResponse = MISA.AMIS.KeToan.Common.Entities.DTO.ServiceResponse;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;
using MISA.AMIS.KeToan.Common.Exceptions;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            //Giá trị của filed này chính là 1 objec thuộc interface IEmployeeDL
            _employeeDL = employeeDL;
        }

        #endregion

        /// <summary>
        /// Xóa nhiều nhân viên theo danh sách ID truyền vào
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Số nhân viên bị xóa thành công</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public bool DeleteRecords(List<Guid> listEmployeeID)
        {
            return _employeeDL.DeleteRecords(listEmployeeID);
        }

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Lấy danh sách nhân viên ở trang số bao nhiêu</param>
        /// <returns>
        /// 200:Danh sách nhân viên tìm thấy theo điều kiện, tổng số bản ghi 
        /// </returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public SearchAndPaging SearchAndPagingEmployee(string? keyword, int limit = 20, int pageNumber = 1)
        {
            return _employeeDL.SearchAndPagingEmployee(keyword, limit, pageNumber);
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất có trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: LQTrung (12/11/2022)
        public string GetEmployeeCodeMax()
        {
            return _employeeDL.GetEmployeeCodeMax();
        }

        /// <summary>
        /// Xuất khẩu danh sách nhân viên
        /// </summary>
        /// <returns>File excel chứa danh sách nhân viên</returns>
        /// CreatedBy: LQTrung(12/11/2022)
        public MemoryStream ExportExcelFile()
        {
            var list = GetAllRecord();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                ///đặt tên cho sheet
                workSheet.Name = "DANH SÁCH NHÂN VIÊN";
                // fontsize mặc định cho cả sheet
                workSheet.Cells.Style.Font.Size = 11;
                // font family mặc định cho cả sheet
                workSheet.Cells.Style.Font.Name = "Times New Roman";

                string[] arrColumnHeader = {
                    "STT",
                    "Mã nhân viên",
                    "Tên nhân viên",
                    "Giới tính",
                    "Ngày sinh",
                    "Chức danh",
                    "Tên đơn vị",
                    "Số tài khoản",
                    "Tên ngân hàng"
                };
                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = arrColumnHeader.Count();

                // gán tiêu đề cho cell đầu tiên 
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
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
                    workSheet.Column(i+2).Width = widthOfColumns[i];
                }

                //Căn trái cột STT
                workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //Căn giữa cột DateOfBirth
                workSheet.Column(5).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                    fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(204,204,204));

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
                    workSheet.Cells[rowIndex, colIndex++].Value = item.EmployeeCode;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.EmployeeName;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.GenderName;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.DateOfBirth.GetValueOrDefault().ToString("dd/MM/yyyy");
                    workSheet.Cells[rowIndex, colIndex++].Value = item.PositionName;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.DepartmentName;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.BankAccountNumber;
                    workSheet.Cells[rowIndex, colIndex++].Value = item.BankName;
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
        /// Hàm validate dữ liệu riêng
        /// </summary>
        /// <param name="record"></param>
        /// CreatedBy: LQTrung (12/11/2022)
        protected override void ValidateSeparate(Employee employee)
        {
            var isDuplicate = _employeeDL.CheckUpDuplicateCode(employee.EmployeeID, employee.EmployeeCode);
            var validateFailures = new List<string>();
            if (!isDuplicate)
            {
                validateFailures.Add("Mã nhân viên đã tồn tại, vui lòng kiểm tra lại.");
                var err = new ServiceResponse
                {
                    Success = false,
                    ErrorDetails = validateFailures
                };
                throw new MISAValidateException(err.ErrorDetails);
            }
        }
    }
}
