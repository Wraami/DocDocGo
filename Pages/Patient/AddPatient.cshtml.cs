using Microsoft.AspNetCore.Mvc;
using DocDocGo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DocDocGo.Pages.Patient
{
    [Authorize]
    public class AddPatientModel : PageModel
    {
        private readonly IRepository<PatientModel> _dbContext;
        public IEnumerable<PatientModel> Patients { get; set; }

        public AddPatientModel(IRepository<PatientModel> dbContext)
        {
            _dbContext = dbContext;
        }
        
        [BindProperty]
        public PatientModel NewPatientModel { get; set; }

        public async Task OnGetAsync()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newPatient = new PatientModel
            {
                FirstName = NewPatientModel.FirstName,
                LastName = NewPatientModel.LastName,
                ContactNumber = NewPatientModel.ContactNumber,
                EmailAddress = NewPatientModel.EmailAddress,
                Gender = NewPatientModel.Gender,
                DateOfBirth = NewPatientModel.DateOfBirth,
                IsPrivatePatient = NewPatientModel.IsPrivatePatient
                };

                var result = await _dbContext.CreateAsync(newPatient);

            return RedirectToPage("/Patient/Index");
        }
    }
}
