using DocDocGo.DAL;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportGeneratorModel : PageModel
    {
        private readonly IRepository<ReportsModel> _dbContext;
        
        public ReportGeneratorModel(IRepository<ReportsModel> dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
        
        }
        
        public void OnPost(IFormFile file) 
        {

        }
    }
}
