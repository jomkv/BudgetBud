using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using BudgetBud.Pages.Menus;
using MySql.Data.MySqlClient;

namespace BudgetBud.Backend.Models
{
    #region Expense Meter Custom Class
    public class ExpenseMeter
    {
        public string CategoryName { get; set; }
        public int MeterPercent { get; set; }
        public decimal Spent { get; set; }
        public decimal Remaining { get; set; }

        public ExpenseMeter(string name, int percent, decimal spent, decimal remaining)
        {
            this.Spent = spent;
            this.CategoryName = name;
            this.MeterPercent = percent;
            this.Remaining = remaining;
        }
    }
    #endregion
    public class ExpenseModel : DbConnection
    {
        public ExpenseModel () { }

        #region Properties
        private decimal budget { get; set; } = 0;
        public List<KeyValuePair<int, string>> categories { get; private set; }
        public List<KeyValuePair<string, int>> expenseMeters { get; private set; } = new List<KeyValuePair<string, int>>();
        public List<ExpenseMeter> expenseMeters2 { get; private set; } = new List<ExpenseMeter>();

        public string month = DateTime.Now.ToString("MMMM");

        #endregion

        #region Fetching data from Database

        #region Monthly Budget
        public void FetchMonthlyBudget()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT `monthly_budget`
                                          FROM `userstbl`
                                          WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            this.budget = Convert.ToDecimal(result);
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
        #endregion

        #region Categories
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
                        expenseMeters2 = new List<ExpenseMeter>();

                        while (reader.Read())
                        {
                            int categoryId = Convert.ToInt32(reader["categoryId"]);
                            string categoryName = reader["category_name"].ToString();
                            // append to said list
                            categories.Add(new KeyValuePair<int, string>(categoryId, categoryName));
                            FetchExpenseMeter(categoryId, categoryName);
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

        #endregion

        #region Expense Meters per Category

        public void FetchExpenseMeter(int categoryId, string categoryName)
        {
            #region Properties

            decimal totalSpent = 0;
            decimal categoryBudget = 0;
            int percent = 0;

            #endregion

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    #region Get Category's Total Spent 

                    string expenseQuery = $@"SELECT SUM(`amount`)
                                             FROM `expensestbl`
                                             WHERE MONTH(`date`) = MONTH(CURRENT_DATE)
                                             AND YEAR(`date`) = YEAR(CURRENT_DATE)
                                             AND `userId` = {UserContext.SessionUserId}
                                             AND `categoryId` = {categoryId};";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            totalSpent = Convert.ToDecimal(result);
                        }
                    }

                    #endregion

                    #region Get Category's Allocated Budget

                    string categoryQuery = $@"SELECT `budget_amount`
                                              FROM `categoriestbl`
                                              WHERE `categoryId` = {categoryId}";

                    using (var command = new MySqlCommand(categoryQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            categoryBudget = Convert.ToDecimal(result);
                        }
                    }

                    #endregion

                    #region Edge Case Conditions for Calculating Percent

                    if (categoryBudget == 0 && totalSpent > 0) // no budget but has spent
                    {
                        percent = 101;
                    }
                    else if (totalSpent == 0 && categoryBudget > 0) // has budget but no spent
                    {
                        percent = 0;
                    }
                    else if (totalSpent == 0 && categoryBudget == 0) // no budget and no spent
                    {
                        percent = 100;
                    }
                    else // valid
                    {
                        percent = (int)Math.Round(totalSpent / categoryBudget * 100);
                    }

                    #endregion

                    expenseMeters2.Add(new ExpenseMeter(categoryName, percent, totalSpent, categoryBudget));
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        #endregion

        public void GetData()
        {
            FetchMonthlyBudget();
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
