using DocDocGo.DAL;
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
        
        public ReportGeneratorModel(IRepository<ReportModel> dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public ReportModel ReportModel { get; set; }

        public void OnGet()
        {
        
        }
        
        public async Task OnPost(IFormFile file) 
        {
            var reports = _dbContext.GetByIdAsync(ReportModel.ReportId);

        }
    }
}
