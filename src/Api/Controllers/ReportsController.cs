using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Application.Queries.GetExpenseSummaryReport;

namespace Api.Controllers
{
    [Route("api/v{version:apiVersion}/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IReportExportService _reportExportService;

        public ReportsController(IMediator mediator, IReportExportService reportExportService)
        {
            _mediator = mediator;
            _reportExportService = reportExportService;
        }

        [HttpGet("{reportType}")]
        public async Task<IActionResult> GetReport([FromQuery] Query query, string reportType, [FromQuery] string format)
        {
            byte[] fileBytes;
            string contentType;
            string fileName;

            // Create a query based on the reportType
           /* Type reportDataType = null;
            switch (reportType.ToLower())
            {
                case "expense-summary":
                    break;
              *//*  case "approval-time":
                    query = new GetApprovalTimeReportQuery();
                    break;
                case "pending-requisitions":
                    query = new GetPendingRequisitionsReportQuery();
                    break;
                case "purchase-order-summary":
                    query = new GetPurchaseOrderSummaryReportQuery();
                    break;
                case "cash-advance":
                    query = new GetCashAdvanceReportQuery();
                    break;*//*
                default:
                    return BadRequest("Invalid report type.");
            }*/

            // Get the report data
            var reportData = await _mediator.Send(query);


            // Export based on format
            switch (format.ToLower())
            {
                case "pdf":
                    fileBytes = await _reportExportService.ExportToPdfAsync(reportData);
                    contentType = "application/pdf";
                    fileName = $"{reportType}.pdf";
                    break;
                case "excel":
                    fileBytes = await _reportExportService.ExportToExcelAsync(reportData);
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = $"{reportType}.xlsx";
                    break;
                case "csv":
                    fileBytes = await _reportExportService.ExportToCsvAsync(reportData);
                    contentType = "text/csv";
                    fileName = $"{reportType}.csv";
                    break;
                default:
                    return BadRequest("Invalid format specified.");
            }

            return File(fileBytes, contentType, fileName);
        }
    }
}
