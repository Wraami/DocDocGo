using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Administrator
{
    [Authorize(Roles = "Administrator")]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost() 
        { 
        }
    }
}
