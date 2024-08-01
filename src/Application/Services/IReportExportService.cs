namespace Application.Services
{
    public interface IReportExportService
    {
        Task<byte[]> ExportToPdfAsync<T>(T reportData);
        Task<byte[]> ExportToExcelAsync<T>(T reportData);
        Task<byte[]> ExportToCsvAsync<T>(T reportData);
    }
}
