using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Appointments
{
    [Authorize]
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
     
                var eventList = appointments.Select(appointment => new
                {
                    id = appointment.AppointmentId,
                    title = appointment.Topic,
                    start = appointment.StartTime,
                    end = appointment.EndTime,
                    status = appointment.Status,
                    notes = appointment.Notes
                }).ToList();

            return new JsonResult(eventList);
        }

        public async Task<IActionResult> OnGetAppointment(int id)
        {
            var appointment = await _dbContext.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }
           
            if (string.IsNullOrEmpty(appointment.Notes))
            {
                appointment.Notes = string.Empty;
            }

            return new JsonResult(appointment);
        }



        public async Task<IActionResult> OnPostAddAppointment()
        {
            if (!ModelState.IsValid)
            {
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

            if (AppointmentData.StartTime >= AppointmentData.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
            }

            await _dbContext.CreateAsync(AppointmentData);

            return RedirectToPage("/Appointments/Index");
        }


        public async Task<IActionResult> OnPostUpdateAppointment()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var appointmentToUpdate = await _dbContext.GetByIdAsync(SelectedAppointmentId);

            if (appointmentToUpdate == null)
            {
                return NotFound();
            }

            appointmentToUpdate.Topic = SelectedAppointment.Topic;
            appointmentToUpdate.PatientId = SelectedAppointment.PatientId;
            appointmentToUpdate.StartTime = SelectedAppointment.StartTime;
            appointmentToUpdate.EndTime = SelectedAppointment.EndTime;
            appointmentToUpdate.Notes = SelectedAppointment.Notes;
            appointmentToUpdate.Status = SelectedAppointment.Status;

            if (appointmentToUpdate.StartTime >= appointmentToUpdate.EndTime)
            {
                ModelState.AddModelError("ValidationError", "Appointment starting time must be before end time.");
                return Page();
            }

            await _dbContext.UpdateAsync(appointmentToUpdate);

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
