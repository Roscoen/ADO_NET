using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using ps5.Models;

namespace ps5.Pages
{
    public class ListModel : PageModel
    {
        public List<Product> productList = new List<Product>();

        private readonly ILogger<ListModel> _logger;

        public IConfiguration _configuration { get; }

        public ListModel(IConfiguration configuration, ILogger<ListModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }



        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection connection = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product";
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = Int32.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                decimal price = Decimal.Parse(reader["price"].ToString());

                productList.Add(new Product {id = id, name=name, price = price });
            }

            reader.Close();
            connection.Close();

        }
        

    }
}
