using BudgetBud.Backend.Db;
using BudgetBud.Backend;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BudgetBud.Backend.Models.BudgetModel;
using System.Diagnostics;

namespace BudgetBud.Backend.Models
{
    public class CategoriesModel : DbConnection
    {
        public CategoriesModel() { }

        public List<KeyValuePair<int, string>> categories {  get; private set; }
        
        public void GetData()
        {
            FetchCategories();
        }

        public void FetchCategories()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoriesQuery = $@"SELECT `categoryId`, `category_name`
                                                FROM `categoriestbl`
                                                WHERE
                                                `userId` = '{UserContext.SessionUserId}';";

                    using (var command = new MySqlCommand(categoriesQuery, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        // list of categories
                        categories = new List<KeyValuePair<int, string>>();

                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["categoryId"]);
                            string name = Convert.ToString(reader["category_name"]);

                            categories.Add(new KeyValuePair<int, string>(id, name));
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
            
        }
    }
}
