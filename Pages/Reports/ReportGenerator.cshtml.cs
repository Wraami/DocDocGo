using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DocDocGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportGeneratorModel : PageModel
    {
        private readonly IRepository<ReportModel> _dbContext;
        private readonly IRepository<ReportTypeModel> _reportTypeContext;
        private readonly IRepository<PatientModel> _patientcontext;

        public IEnumerable<PatientModel> Patients { get; set; }

        public ReportGeneratorModel(IRepository<ReportModel> dbContext, IRepository<ReportTypeModel> typeDbContext, IRepository<PatientModel> patientDbContext)
        {
            _dbContext = dbContext;
            _reportTypeContext = typeDbContext;
            _patientcontext = patientDbContext;
        }

        [BindProperty]
        public ReportModel ReportModel { get; set; }
        public IEnumerable<ReportTypeModel> ReportTypes { get; set; }

        public async Task<IActionResult> OnGetAsync(int reportId)
        {
            Patients = await _patientcontext.GetAsync();
            ReportTypes = await _reportTypeContext.GetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostExportToExcelAsync(int reportId, string fileName)
        {
            var report = await _dbContext.GetByIdAsync(reportId);

            if (report == null)
            {
                return NotFound();
            }

            using (var package = new ExcelPackage())
            {
                // Create worksheet
                var worksheet = package.Workbook.Worksheets.Add("Report");

                worksheet.Cells["A1"].Value = "Report ID";
                worksheet.Cells["B1"].Value = "Patient ID";
                worksheet.Cells["C1"].Value = "Created On";
                worksheet.Cells["D1"].Value = "Reporting Status";
                worksheet.Cells["E1"].Value = "By Staff Member";
                worksheet.Cells["F1"].Value = "Report Type";

                worksheet.Cells["A2"].Value = report.ReportId;
                worksheet.Cells["B2"].Value = report.PatientId;
                worksheet.Cells["C2"].Value = report.CreatedAt;
                worksheet.Cells["D2"].Value = report.Status;
                worksheet.Cells["E2"].Value = report.InitialStaffName;
                worksheet.Cells["F2"].Value = report.ReportTypeModel;

                var stream = new MemoryStream(package.GetAsByteArray());

                stream.Position = 0;

                // Return the Excel file as a FileStreamResult
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx"); //uses the entered filename for the worksheet.
            }
        }

        public async Task<IActionResult> OnPostCreateReportAsync(ReportModel report)
        {
            if (ModelState.IsValid)
            {
                report.CreatedAt = DateTime.Now;
                report.LastUpdated = DateTime.Now;

                await _dbContext.CreateAsync(report);

                return RedirectToPage("/Reports/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostExportToCSVAsync(ReportModel report)
        {
            return Page();
        }

    }
}
