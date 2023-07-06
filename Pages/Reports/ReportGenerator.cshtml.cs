using DocDocGo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Reports
{
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
    }
}
