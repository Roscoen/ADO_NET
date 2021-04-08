using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ps5.Models;

namespace ps5.Pages
{
    public class DetailsModel : PageModel
    {

        private readonly ILogger<DetailsModel> _logger;

        public IConfiguration _configuration { get; }

        public DetailsModel(IConfiguration configuration, ILogger<DetailsModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [BindProperty(SupportsGet = true)]
        public int id { set; get; }

        public string name { set; get; }

        public decimal price { set; get; }

        public List<Product> productList;

        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product WHERE id=@ProductId";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ProductId", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                name = reader["name"].ToString();
                price = Decimal.Parse(reader["price"].ToString());
            }

            reader.Close();
            connection.Close();

        }
    }
}
