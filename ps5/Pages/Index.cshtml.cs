using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ps5.Models;

namespace ps5.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            Response.Cookies.Append("cookies", JsonSerializer.Serialize(new List<Product> { }));
            return RedirectToPage("List");
        }
    }
}
