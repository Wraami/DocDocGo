using DocDocGo.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportsModel : PageModel
    {
        private ApplicationDBContext _dbcontext;

        public ReportsModel(ApplicationDBContext dbcontext)
        {
                _dbcontext = dbcontext;
        }

        public void OnGet()
        {
        }
    }
}
