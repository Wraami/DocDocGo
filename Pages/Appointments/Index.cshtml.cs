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

        [BindProperty]
        public AppointmentModel NewAppointment { get; set; }

        [BindProperty]
        public AppointmentModel SelectedAppointment { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetEvents()
        {
            IEnumerable<AppointmentModel> appointments = await _dbContext.GetAsync();

            var eventList = new List<object>();
            foreach (var appointment in appointments)
            {

                var eventData = new
                {
                    id = appointment.AppointmentId,
                    title = appointment.Topic,
                    start = appointment.StartTime,
                    end = appointment.EndTime
                };

                eventList.Add(eventData);
            }
            return new JsonResult(eventList);
        }

        public async Task<IActionResult> OnPostAddAppointment()
        {
           
            if (!ModelState.IsValid)
            {              
                return Page();
            }

            if (NewAppointment.StartTime >= NewAppointment.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
                return Page();
            }

            // Add the new appointment to the database.
            await _dbContext.CreateAsync(NewAppointment);

            return RedirectToPage("/Appointments/Index");
        }


        public async Task<IActionResult> OnPostUpdateAppointment()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return Page();
            }
            if (SelectedAppointment.StartTime >= SelectedAppointment.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
                return Page();
            }
            //appropriate code here for updating when implemented.

            // Redirect to the same page or another page if needed
            return RedirectToPage("/Appointments/Index");
        }
    }
}
