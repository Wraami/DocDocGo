using DocDocGo.Models;
using DocDocGo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public RegisterModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public UserModel CredentialModel { get; set; }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel
                {
                    Email = CredentialModel.Email,
                    DateOfBirth = CredentialModel.DateOfBirth,
                    PhoneNumber = CredentialModel.PhoneNumber,
                    FirstName = CredentialModel.FirstName,
                    MiddleName = CredentialModel?.MiddleName,
                    LastName = CredentialModel.LastName,
                    UserName = CredentialModel.Email,
                    Password = CredentialModel.Password //Need to refine so no cleartext password is stored, put in seperate viewmodel.
                };

                var result = await _userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Home/Dashboard"); // Redirect to main dashboard
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
