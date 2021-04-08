using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ps5.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;

        public IConfiguration _configuration { get; }

        [BindProperty(SupportsGet = true)]
        public int id { set; get; }

        public DeleteModel(IConfiguration configuration, ILogger<DeleteModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            string sql = "DELETE FROM Product WHERE id=@ProductId;";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);
            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            return RedirectToPage("Index");
        }
    }
}
