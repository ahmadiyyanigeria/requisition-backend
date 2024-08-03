using Application.Common;
using static Application.Queries.GetExpenseSummaryReport;

namespace Infrastructure.Common.Exports
{
    public class PdfReportGeneratorFactory
    {
        private readonly Dictionary<Type, object> _generators;

        public PdfReportGeneratorFactory()
        {
            _generators = new Dictionary<Type, object>
        {
            { typeof(ExpenseSummaryReport), new ExpenseSummaryReportPdfGenerator() },
            { typeof(object), new ExpenseSummaryReportPdfGenerator() }
            // Add other report generators here
        };
        }

        public IPdfReportGenerator<T> GetGenerator<T>()
        {
            var reportType = typeof(T);
            if (_generators.TryGetValue(reportType, out var generator))
            {
                return (IPdfReportGenerator<T>)generator;
            }

            // Logging or debugging information
            Console.WriteLine($"No generator found for report type {reportType.FullName}");
            foreach (var registeredType in _generators.Keys)
            {
                Console.WriteLine($"Registered type: {registeredType.FullName}");
            }

            throw new InvalidOperationException($"No generator found for report type {reportType.FullName}");
        }
    }
}
