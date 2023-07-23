using DocDocGo.Models;
using DocDocGo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DocDocGo.Pages.Patient
{
    public class UpdatePatientModel : PageModel
    {
        private readonly IRepository<PatientModel> _dbContext;

        public UpdatePatientModel(IRepository<PatientModel> dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public PatientModel ExistingPatientModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ExistingPatientModel = await _dbContext.GetByIdAsync(id);

            if (ExistingPatientModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            var existingPatient = await _dbContext.GetByIdAsync(ExistingPatientModel.PatientId);
            if (existingPatient == null)
            {
                return NotFound();
            }

            existingPatient.FirstName = ExistingPatientModel.FirstName;
            existingPatient.LastName = ExistingPatientModel.LastName;
            existingPatient.ContactNumber = ExistingPatientModel.ContactNumber;
            existingPatient.Gender = ExistingPatientModel.Gender;
            existingPatient.EmailAddress = ExistingPatientModel.EmailAddress;
            existingPatient.DateOfBirth = ExistingPatientModel.DateOfBirth;
            existingPatient.IsPrivatePatient = ExistingPatientModel.IsPrivatePatient;


            await _dbContext.UpdateAsync(existingPatient);

            return RedirectToPage("/Patient/Index");

        }
    }
}
