using DocDocGo.DAL;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DocDocGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public void OnGet()
        {
        
        }
        
        public void OnPost(IFormFile file) 
        {

        }
    }
}
