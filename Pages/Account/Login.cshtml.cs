using DocDocGo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace DocDocGo.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;

        public LoginModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty, Required]
        public UserModel CredentialModel { get; set; }

        public void OnGet()
        {
        
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(CredentialModel.Email, CredentialModel.PasswordHash, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Home/Dashboard"); // Redirect to the main dashboard.
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }

    }
}
