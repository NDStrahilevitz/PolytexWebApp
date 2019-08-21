using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PolytexWebApp.DataLayer;
using PolytexWebApp.Models;

namespace PolytexWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(){
        }

        public IActionResult OnGetAsync(){
            return Page();
        }
    }
}

