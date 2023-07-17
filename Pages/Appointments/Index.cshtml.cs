using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace DocDocGo.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentRepository<AppointmentModel> _dbContext;
        private readonly IRepository<PatientModel> _patientcontext;

        public IEnumerable<AppointmentModel> Appointments { get; set; }
        public IEnumerable<PatientModel> Patients { get; set; }

        public IndexModel(IAppointmentRepository<AppointmentModel> dbContext, IRepository<PatientModel> patientRepository)
        {
            _dbContext = dbContext;
            _patientcontext = patientRepository;
        }

        [BindProperty]
        public AppointmentModel NewAppointment { get; set; }

        [BindProperty]
        public AppointmentModel SelectedAppointment { get; set; }

        [BindProperty]
        public int SelectedAppointmentId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Patients = await _patientcontext.GetAsync();
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

        public async Task<IActionResult> OnGetAppointment(int id)
        {
            var appointment = await _dbContext.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return new JsonResult(appointment);
        }



        public async Task<IActionResult> OnPostAddAppointment()
        {
            if (!ModelState.IsValid)
            {
                // Get all model state errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                // Log or display the errors
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // Replace with your desired error handling logic
                }
                return Page();
            }
          
                var AppointmentData = new AppointmentModel
                {
                    StartTime = NewAppointment.StartTime,
                    PatientId = NewAppointment.PatientId,
                    EndTime = NewAppointment.EndTime,
                    Notes = NewAppointment.Notes,
                    Status = NewAppointment.Status,
                    Topic = NewAppointment.Topic
                };

            // Rest of your code here

            if (AppointmentData.StartTime >= AppointmentData.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
            }

            // Add the new appointment to the database.
            await _dbContext.CreateAsync(AppointmentData);

            return RedirectToPage("/Appointments/Index");
        }


        public async Task<IActionResult> OnPostUpdateAppointment()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (SelectedAppointment.StartTime >= SelectedAppointment.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
                return Page();
            }
            await _dbContext.UpdateAsync(SelectedAppointment);

            return RedirectToPage("/Appointments/Index");
        }

        public async Task<IActionResult> OnPostDeleteAppointment()
        {
            var appointmentToDelete = await _dbContext.GetByIdAsync(SelectedAppointmentId);

            if (appointmentToDelete != null)
            {
                await _dbContext.DeleteAsync(appointmentToDelete);
            }

            return RedirectToPage("/Appointments/Index");
        }
    }
}
