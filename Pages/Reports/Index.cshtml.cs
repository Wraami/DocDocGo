using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Pages.Reports
{
    [Authorize]
    public class ReportsModel : PageModel
    {
        private IRepository<ReportModel> _dbcontext;
        public IEnumerable<ReportModel> Reports { get; set; }

        public ReportsModel(IRepository<ReportModel> dbcontext)
        {
                _dbcontext = dbcontext;
        }

        public async Task OnGet()
        {
            Reports = await _dbcontext.GetAsync();

        }
    }
}
