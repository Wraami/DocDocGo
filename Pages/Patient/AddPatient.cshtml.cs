using Microsoft.AspNetCore.Mvc;
using DocDocGo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocDocGo.Repositories.Interfaces;

namespace DocDocGo.Pages.Patient
{
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
                    IsPrivatePatient = NewPatientModel.IsPrivatePatient
                };

                var result = await _dbContext.CreateAsync(newPatient);

            return Page();
        }
    }
}
