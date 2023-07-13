using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<AppointmentModel> _dbContext;

        public IEnumerable<AppointmentModel> Appointments { get; set; }

        public IndexModel(IRepository<AppointmentModel> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }
        
        public async Task<JsonResult> OnGetAppointmentEvents() //Used for fetching appointments, seperate event is more scalable.
        {
            var events = await _dbContext.GetAsync();
            return new JsonResult(events);
        }
    }
}
