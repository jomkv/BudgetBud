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
    #region Custom Category Class
    public class Category
    {
        public string Name { get; private set; }
        public decimal BudgetPercent { get; private set; }

        public Category(string name, decimal percent)
        {
            this.BudgetPercent = percent;
            this.Name = name;
        }
    }
    #endregion

    public class HomeModel : DbConnection
    {
        public HomeModel() { }

        private string today = DateTime.Now.ToString("yyyy-MM-dd");

        #region Properties

        public decimal budget { get; private set; } = 0;
        public decimal spent { get; private set; } = 0;
        public decimal available { get; private set; } = 0;
        public int expenseCountToday { get; private set; } = 0;
        public decimal totalSpentToday { get; private set; } = 0;
        public List<KeyValuePair<string, int>> expenseMeters { get; private set; } = new List<KeyValuePair<string, int>>();
        // Category Name, Allocated Budget (in percentage form)
        public List<Category> categoryBudgets { get; private set; } = new List<Category>();

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
                            this.available = Convert.ToDecimal(result);
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

        #region Total Spent and Available

        public void FetchTotalSpent()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT SUM(`amount`)
                                             FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}
                                             AND MONTH(`date`) = MONTH(CURRENT_DATE)
                                             AND YEAR(`date`) = YEAR(CURRENT_DATE)";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if(result != DBNull.Value && result != null)
                        {
                            this.spent = Convert.ToDecimal(result);
                            this.available -= this.spent;
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

        #endregion

        #region Expense Meters Per Category

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

                        if(result != DBNull.Value && result != null)
                        {
                            totalSpent = Convert.ToDecimal(result);
                        }
                    }

                    #endregion

                    #region Get Category's Allocated Budget

                    string categoryQuery = $@"SELECT (`budget_percent` / 100) * {budget}
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
                        percent = (int) Math.Round(totalSpent / categoryBudget * 100);
                    }

                    #endregion

                    expenseMeters.Add(new KeyValuePair<string, int>(categoryName, percent));
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        public void FetchCategories ()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoryQuery = $@"SELECT `categoryId`, `category_name`
                                              FROM `categoriestbl`
                                              WHERE `userId` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(categoryQuery, connection))
                    {
                        var reader = command.ExecuteReader();

                        while(reader.Read())
                        {
                            string name = reader["category_name"].ToString();
                            int id = Convert.ToInt32(reader["categoryId"]);

                            FetchExpenseMeter(id, name);
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

        #endregion

        #region Total Expenses Count Today
        public void FetchExpenseCountToday()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    Debug.WriteLine(today);

                    string expenseQuery = $@"SELECT COUNT(*) FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}
                                             AND `date` = '{this.today}';";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            this.expenseCountToday = Convert.ToInt32(result);
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
        #endregion

        #region Total Spent Today
        public void FetchTotalSpentToday()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT SUM(`amount`) FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}
                                             AND `date` = '{this.today}';";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if(result != DBNull.Value && result != null)
                        {
                            this.totalSpentToday = Convert.ToDecimal(result);
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

        #endregion

        public void FetchCategoryBudgets ()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoryQuery = $@"SELECT `category_name`, `budget_percent` 
                                              FROM `categoriestbl`
                                              WHERE `userId` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(categoryQuery, connection))
                    {
                        var reader = command.ExecuteReader();
                        decimal totalBudget = 0;

                        while(reader.Read())
                        {
                            string name = reader.GetString("category_name");
                            decimal budget = reader.GetDecimal("budget_percent");
                            categoryBudgets.Add(new Category(name, budget));

                            totalBudget += budget;
                        }

                        if(totalBudget < 100)
                        {
                            decimal unallocated = 100 - totalBudget;
                            Debug.WriteLine(unallocated);
                            // Add unallocated budget to list
                            categoryBudgets.Add(new Category("Unallocated", unallocated));
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

        public void GetData()
        {
            FetchExpenseCountToday();
            FetchTotalSpentToday();
            FetchMonthlyBudget();
            FetchTotalSpent();
            FetchCategories();
            FetchCategoryBudgets();
        }

        #endregion
    }
}
