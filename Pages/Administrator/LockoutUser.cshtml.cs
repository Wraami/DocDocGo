using DocDocGo.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DocDocGo.Services;
using Microsoft.AspNetCore.Authorization;

namespace DocDocGo.Pages.Administrator
{
    [Authorize(Roles = "Administrator")]
    public class LockoutUserModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IEmailSender _emailSender;

        public LockoutUserModel(UserManager<UserModel> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public UserModel ExistingUserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ExistingUserModel = await _userManager.FindByIdAsync(id.ToString());

            if (ExistingUserModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());

            if (existingUser == null)
            {
                return NotFound();
            }

            // Manually lockout the user
            await _userManager.SetLockoutEndDateAsync(existingUser, DateTimeOffset.MaxValue);

            //you could implement the sending of an email to the user, to tell them that their account has been locked out from the system here.

            return RedirectToPage("/Administrator/Settings");
        }

    }
}
