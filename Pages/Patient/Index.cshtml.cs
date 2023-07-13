using DocDocGo.DAL;
using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Pages.Patient
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRepository<PatientModel> _dbContext;
        public IEnumerable<PatientModel> Patients { get; set; }

        public IndexModel(IRepository<PatientModel> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
           Patients = await _dbContext.GetAsync();
        }
    }
}
