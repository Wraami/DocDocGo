using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocDocGo.Pages.Administrator
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
