using DocDocGo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel CredentialModel { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {

        }
    }
}
