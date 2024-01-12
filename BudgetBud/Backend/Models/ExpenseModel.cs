using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using MySql.Data.MySqlClient;

namespace BudgetBud.Backend.Models
{
    public class ExpenseModel : DbConnection
    {
        public ExpenseModel () { }

        #region Properties

        public List<KeyValuePair<int, string>> categories { get; private set; }
        public string month = DateTime.Now.ToString("MMMM");

        #endregion

        #region Fetching data from Database

        public void FetchCategories()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoriesQuery = $@"SELECT `categoryId`, `category_name`
                                                FROM `categoriestbl`
                                                WHERE `userId` = '{UserContext.SessionUserId}';";

                    using (var command = new MySqlCommand(categoriesQuery, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        // list of expense categories
                        categories = new List<KeyValuePair<int, string>>();

                        while (reader.Read())
                        {
                            int categoryId = Convert.ToInt32(reader["categoryId"]);
                            string categoryName = reader["category_name"].ToString();
                            // append to said list
                            categories.Add(new KeyValuePair<int, string>(categoryId, categoryName));
                        }

                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void GetData()
        {
            FetchCategories();
        }

        #endregion

        #region Actions

        // returns true if insertion successful, false if error
        public bool AddExpense(string category, int categoryId, string title, string description, decimal amount, string date)
        {
            bool output = false;
            try
            {

                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"INSERT INTO `expensestbl`
                                             (`category`, `categoryId`, `title`, `description`, `amount`, `date`, `userId`)
                                             VALUES
                                             ('{category}' ,'{categoryId}','{title}','{description}','{amount}','{date}','{UserContext.SessionUserId}');";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                output = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return output;
        }

        #endregion
    }
}
