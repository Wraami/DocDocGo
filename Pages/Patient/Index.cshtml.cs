using DocDocGo.DAL;
using DocDocGo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Pages.Patient
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _dbContext;

        public IEnumerable<PatientModel> Patients { get; set; }
        public IndexModel(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
         Patients = await _dbContext.Patients.ToListAsync();
        }
    }
}
