using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportGeneratorModel : PageModel
    {
        private readonly IRepository<ReportModel> _dbContext;
        private readonly IRepository<ReportTypeModel> _reportTypeContext;
        private readonly IRepository<PatientModel> _patientcontext;

        public IEnumerable<PatientModel> Patients { get; set; }
        public IEnumerable<ReportTypeModel> ReportTypes { get; set; }

        public ReportGeneratorModel(IRepository<ReportModel> dbContext, IRepository<ReportTypeModel> typeDbContext, IRepository<PatientModel> patientDbContext)
        {
            _dbContext = dbContext;
            _reportTypeContext = typeDbContext;
            _patientcontext = patientDbContext;
        }

        [BindProperty]
        public ReportModel NewReportModel { get; set; }
        public async Task<IActionResult> OnGetAsync(int reportId)
        {
            Patients = await _patientcontext.GetAsync();
            ReportTypes = await _reportTypeContext.GetAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateReport()
        {
            if (ModelState.IsValid)
            {
               var ReportData = new ReportModel
                {
                    ReportDescription = NewReportModel.ReportDescription,
                    PatientId = NewReportModel.PatientId,
                    InitialStaffName = NewReportModel.InitialStaffName,
                    Status = NewReportModel.Status,
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                await _dbContext.CreateAsync(ReportData);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostExportToCSV()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostCreateTemplateType(ReportTypeModel reporttype)
        {
            if (ModelState.IsValid)
            {
                reporttype.ReportTypeCreationTime = DateTime.Now;

                await _reportTypeContext.CreateAsync(reporttype);

                return RedirectToPage("/Reports/ReportType");
            }
            return Page();
        }


    }
}
