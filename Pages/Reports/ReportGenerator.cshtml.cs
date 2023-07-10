using DocDocGo.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportGeneratorModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public ReportGeneratorModel(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
        
        }
        
        public void OnPost(IFormFile file) 
        {

        }
    }
}
