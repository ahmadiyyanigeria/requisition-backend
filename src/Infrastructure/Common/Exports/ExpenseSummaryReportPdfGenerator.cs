using Application.Common;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using static Application.Queries.GetExpenseSummaryReport;

namespace Infrastructure.Common.Exports
{
    public class ExpenseSummaryReportPdfGenerator : IPdfReportGenerator<ExpenseSummaryReport>
    {
        public async Task<byte[]> GeneratePdfAsync(ExpenseSummaryReport reportData)
        {
            try
            {
                using var memoryStream = new MemoryStream();

                // Create PDF writer and document
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Title
                document.Add(new Paragraph("Expense Summary Report")
                    .SetFontSize(18)
                    .SetBold()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                // Summary Table
                var summaryTable = new Table(2);
                summaryTable.AddCell("Total Requests");
                summaryTable.AddCell(reportData.TotalRequests.ToString());
                summaryTable.AddCell("Total Approved");
                summaryTable.AddCell(reportData.TotalApproved.ToString());
                summaryTable.AddCell("Total Rejected");
                summaryTable.AddCell(reportData.TotalRejected.ToString());
                summaryTable.AddCell("Total In Progress");
                summaryTable.AddCell(reportData.TotalInProgress.ToString());
                summaryTable.AddCell("Total Pending");
                summaryTable.AddCell(reportData.TotalPending.ToString());
                summaryTable.AddCell("Total Processed");
                summaryTable.AddCell(reportData.TotalProcessed.ToString());

                document.Add(new Paragraph("Summary").SetFontSize(16).SetBold());
                document.Add(summaryTable);

                // Amount Table
                var amountTable = new Table(2);
                amountTable.AddCell("Total Amount Requested");
                amountTable.AddCell(reportData.TotalAmountRequested.ToString("C"));
                amountTable.AddCell("Total Amount Approved");
                amountTable.AddCell(reportData.TotalAmountApproved.ToString("C"));

                document.Add(new Paragraph("Amounts").SetFontSize(16).SetBold());
                document.Add(amountTable);

                // Approval Times Table
                var approvalTimesTable = new Table(2);
                approvalTimesTable.AddCell("Average Approval Time");
                approvalTimesTable.AddCell(reportData.AverageApprovalTime.ToString("F2") + " days");

                foreach (var (type, time) in reportData.ApprovalTimesByTypes)
                {
                    approvalTimesTable.AddCell($"Approval Time by Type ({type})");
                    approvalTimesTable.AddCell(time.ToString("F2") + " days");
                }

                document.Add(new Paragraph("Approval Times").SetFontSize(16).SetBold());
                document.Add(approvalTimesTable);

                // Save the document
                document.Close();
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"PDF generation error: {ex.Message}");
                throw new Exception("An error occurred while generating the PDF.", ex);
            }
        }
    }
}
