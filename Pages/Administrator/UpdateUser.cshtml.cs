using DocDocGo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Administrator
{
    public class UpdateUserModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IEmailSender _emailSender;

        public UpdateUserModel(UserManager<UserModel> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public UserModel ExistingUserModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ExistingUserModel = await _userManager.FindByIdAsync(id.ToString());

            if (ExistingUserModel == null)
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
            var existingUser = await _userManager.FindByIdAsync(ExistingUserModel.Id.ToString());

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = ExistingUserModel.FirstName;
            existingUser.LastName = ExistingUserModel.LastName;
            existingUser.PhoneNumber = ExistingUserModel.PhoneNumber;
            existingUser.Email = ExistingUserModel.Email;


            await _userManager.UpdateAsync(existingUser);

            return RedirectToPage("/Patient/Index");

        }

    }
}
