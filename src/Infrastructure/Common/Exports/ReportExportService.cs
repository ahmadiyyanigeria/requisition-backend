using Application.Services;
using OfficeOpenXml;
using System.Text;

namespace Infrastructure.Common.Exports
{
    public class ReportExportService :IReportExportService
    {
        private readonly PdfReportGeneratorFactory _factory;

        public ReportExportService(PdfReportGeneratorFactory factory)
        {
            _factory = factory;
        }

        public async Task<byte[]> ExportToPdfAsync<T>(T reportData)
        {
            var generator = _factory.GetGenerator<T>();
            return await generator.GeneratePdfAsync(reportData);
        }

      /*  public async Task<byte[]> ExportToPdfAsync<T>(T reportData)
        {
            var factory = new PdfReportGeneratorFactory();
            var generator = factory.GetGenerator<T>();
            return await generator.GeneratePdfAsync(reportData);
        }*/
        
        public Task<byte[]> ExportToExcelAsync<T>(T reportData)
        {
            return Task.Run(() =>
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Report");
                    PopulateWorksheet(worksheet, reportData);

                    return package.GetAsByteArray();
                }
            });
        }

        private void PopulateWorksheet<T>(ExcelWorksheet worksheet, T reportData)
        {
            // Implement the logic to populate the worksheet with data from reportData
            // This is an example implementation; customize as needed
            worksheet.Cells[1, 1].Value = "Column1";
            worksheet.Cells[1, 2].Value = "Column2";
            // Add data rows based on reportData
        }

        public Task<byte[]> ExportToCsvAsync<T>(T reportData)
        {
            return Task.Run(() =>
            {
                var sb = new StringBuilder();
                var properties = typeof(T).GetProperties();

                // Add header
                sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

                // Add data rows
                foreach (var item in (IEnumerable<T>)Convert.ChangeType(reportData, typeof(IEnumerable<T>)))
                {
                    var values = properties.Select(p => p.GetValue(item, null)?.ToString() ?? string.Empty);
                    sb.AppendLine(string.Join(",", values));
                }

                return Encoding.UTF8.GetBytes(sb.ToString());
            });
        }
    }
}
