using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ps5.Pages
{

    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public IConfiguration _configuration { get; }

        public EditModel(IConfiguration configuration, ILogger<EditModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty]
        [HiddenInput]
        public int id { set; get; }

        [BindProperty]
        [Display(Name = "Nazwa przemiotu"), Required(ErrorMessage = "Pole 'Nazwa przedmiotu' jest obowiazkowe"), RegularExpression(@"^[a-zA-Z\s]{1,150}$", ErrorMessage = "Podac tylko litery")]
        public string name { get; set; }

        [BindProperty]
        [Display(Name = "Cena"), Range(0, int.MaxValue, ErrorMessage = "'Cena' musi być wieksza od 0")]
        public decimal price { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            string sql = "UPDATE Product SET name=@ProductName, price=@ProductPrice WHERE id=@ProductId;";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.Parameters.AddWithValue("@ProductName", name);
            cmd.Parameters.AddWithValue("@ProductPrice", price);
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            return RedirectToPage("Index");
        }
    }
}
