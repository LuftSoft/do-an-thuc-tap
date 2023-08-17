using Aspose.Pdf;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using phone_shop_server.Business.APIResponse;
using phone_shop_server.Business.Converter;
using phone_shop_server.Business.DTO.Address;
using phone_shop_server.Business.DTO.Brand;
using phone_shop_server.Business.DTO.Phone;
using phone_shop_server.Business.Service;
using phone_shop_server.Database.Entity;
using phone_shop_server.Database.Enum;
using phone_shop_server.Database.Repository;
using phone_shop_server.Util;
using System.Data;

namespace phone_shop_server.API
{
    [ApiController]
    [Route("api/v{version:apiVersion}/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        public ReportController(
            IReportRepository reportRepository
            )
        {
            _reportRepository = reportRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28,28,28,40)}
            };
            var pdfPage = document.Pages.Add();
            Table table = new Table
            {
                ColumnWidths = "25% 25% 25% 25%",
                DefaultCellPadding = new MarginInfo(5,10,5,10),
                Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)
            };
            string url = "https://i.pinimg.com/736x/7c/b5/49/7cb5492889809cb8303b76b80759f0df.jpg";
            string imageFile = Path.Combine(url, "Image-to-PDF.png");

            // The path to output PDF File.
            DataTable dataTable = new DataTable();
            Image img = new Image();
            img.File = imageFile;
            dataTable.Columns.Add("Weight", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Breed", typeof(string));
            dataTable.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            dataTable.Rows.Add(12, "Koko", "Shar Pei", DateTime.Now);
            dataTable.Rows.Add(12, "Fido", "Bullmastiff", DateTime.Now);
            dataTable.Rows.Add(121, "Alex", "Anatolian Shepherd Dog", DateTime.Now);
            dataTable.Rows.Add(12, "Charles", "Cavalier King Charles Spaniel", DateTime.Now);
            dataTable.Rows.Add(12, "Candy", "Yorkshire Terrier", DateTime.Now);
            table.ImportDataTable(dataTable, true, 0, 0);
            document.Pages[1].Paragraphs.Add(table);
            using(var stream = new MemoryStream())
            {
                document.Save(stream);
                return new FileContentResult(stream.ToArray(), "application/pdf")
                {
                    FileDownloadName = "testfile.pdf"
                };
            };
        }

        [HttpGet("pdf/month/{year}")]
        public async Task<IActionResult> GetByMonthPDF(int year)
        {
            try
            {
                var today = DateTime.Now;
                List<MonthStat> result = (await _reportRepository.MONTHStat(year)).ToList();
                var document = new Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
                };
                var pdfPage = document.Pages.Add();
                Table table = new Table
                {
                    ColumnWidths = "25% 25% 25% 25%",
                    DefaultCellPadding = new MarginInfo(5, 10, 5, 10),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)
                };

                //add title
                TextStamp textStamp = new TextStamp($"Thống kê doanh thu năm {today.Year} theo tháng");
                // Set properties of the stamp
                textStamp.TopMargin = 10;
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment = VerticalAlignment.Top;
                // The path to output PDF File.
                DataTable dataTable = new DataTable();
                Image img = new Image();
                dataTable.Columns.Add("Tháng", typeof(string));
                dataTable.Columns.Add("Tổng đơn hàng", typeof(int));
                dataTable.Columns.Add("Tổng sản phẩm", typeof(int));
                dataTable.Columns.Add("Doanh thu", typeof(double));
                int total_order = 0;
                int total_product = 0;
                double total_price = 0;
                // Here we add five DataRows.
                foreach (var item in result)
                {
                    dataTable.Rows.Add("Tháng "+item.Month, item.TotalOrder, item.TotalProduct, item.TotalPrice);
                    total_order += item.TotalOrder;
                    total_product += item.TotalProduct;
                    total_price += item.TotalPrice;
                }
                dataTable.Rows.Add("Tổng cộng", total_order, total_product, total_price);
                table.ImportDataTable(dataTable, true, 0, 0);
                document.Pages[1].Paragraphs.Add(table);
                document.Pages[1].AddStamp(textStamp);
                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return File(stream.ToArray(), "application/pdf", "testfile.pdf");
                };
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse() 
                { 
                    code = "ERROR",
                    message = ex.Message,
                });
            }
        }

        [HttpGet("pdf/thirty-day")]
        public async Task<IActionResult> GetByDayPDF()
        {
            try
            {
                List<DateStat> result = (await _reportRepository.getStatByConfig("day")).ToList();
                var document = new Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
                };
                var pdfPage = document.Pages.Add();
                Table table = new Table
                {
                    ColumnWidths = "25% 25% 25% 25%",
                    DefaultCellPadding = new MarginInfo(5, 10, 5, 10),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)
                };
                //add title
                TextStamp textStamp = new TextStamp($"Thống kê doanh thu 30 ngày gần nhất");
                // Set properties of the stamp
                textStamp.TopMargin = 10;
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment = VerticalAlignment.Top;
                // The path to output PDF File.
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Ngày", typeof(string));
                dataTable.Columns.Add("Tổng đơn hàng", typeof(int));
                dataTable.Columns.Add("Tổng sản phẩm", typeof(int));
                dataTable.Columns.Add("Doanh thu", typeof(double));
                int total_order = 0;
                int total_product = 0;
                double total_price = 0;
                // Here we add five DataRows.
                foreach (var item in result)
                {
                    dataTable.Rows.Add(item.Date, item.TotalOrder, item.TotalProduct, item.TotalPrice);
                    total_order += item.TotalOrder;
                    total_product += item.TotalProduct;
                    total_price += item.TotalPrice;
                }
                dataTable.Rows.Add("Tổng cộng", total_order, total_product, total_price);
                table.ImportDataTable(dataTable, true, 0, 0);
                document.Pages[1].Paragraphs.Add(table);
                document.Pages[1].AddStamp(textStamp);
                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return File(stream.ToArray(), "application/pdf", "testfile.pdf");
                };
            }
            catch (Exception ex)
            {
                return Ok(new APIResponse()
                {
                    code = "ERROR",
                    message = ex.Message,
                });
            }
        }
        [HttpGet("month/{year}")]
        public async Task<APIResponse> GetByMonth(int year)
        {
            try
            {
                List<MonthStat> result = (await _reportRepository.MONTHStat(year)).ToList();
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
            }
        }
        [HttpGet("thirty-day")]
        public async Task<APIResponse> GetBy30Day()
        {
            try
            {
                List<DateStat> result = (await _reportRepository.getStatByConfig("day")).ToList();
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
            }
        }
        [HttpGet("day")]
        public async Task<APIResponse> getbyday(string day)
        {
            try
            {
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = await _reportRepository.getStatByDATE(day)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
            }
        }
        [HttpPost("range-day")]
        public async Task<APIResponse> GetByRangeDay(RangeDate rangeDate)
        {
            try
            {
                List<DateStat> result = (await _reportRepository.getStatByDateRange(rangeDate.startDate, rangeDate.endDate)).ToList();
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.SUCCESS.ToString(),
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new APIResponse()
                {
                    code = phone_shop_server.Database.Enum.StatusCode.ERROR.ToString(),
                    message = ex.Message,
                };
            }
        }
    }
    public class RangeDate
    {
        public string startDate { set; get; }
        public string endDate { set; get; }
    }
}
