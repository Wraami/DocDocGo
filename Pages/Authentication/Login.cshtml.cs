using DocDocGo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Authentication
{
    public class LoginModel : PageModel
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
