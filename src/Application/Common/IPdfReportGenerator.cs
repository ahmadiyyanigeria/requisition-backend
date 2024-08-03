namespace Application.Common
{
    public interface IPdfReportGenerator<T>
    {
        Task<byte[]> GeneratePdfAsync(T reportData);
    }
}
