using DocDocGo.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        public UserModel credentialModel { get; set; }
        
        public void OnGet()
        {
        }

    }
}
