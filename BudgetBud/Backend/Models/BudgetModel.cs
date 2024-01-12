using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;

namespace BudgetBud.Backend.Models
{
    public class BudgetModel : DbConnection
    {
        #region Category Custom Class

        public class Category<T1, T2, T3>
        {
            public T1 Id { get; private set; }
            public T2 Name { get; private set; }
            public T3 BudgetPercent { get; private set; }

            public Category(T1 id, T2 name, T3 percent)
            {
                this.Id = id;
                this.Name = name;
                this.BudgetPercent = percent;
            }
        }

        #endregion


        #region Properties

        public string Month { get; private set; }
        public decimal? MonthlyBudget { get; private set; }
        public List<Category<int, string, int>> categories { get; private set; }
        public BudgetModel() { }

        #endregion

        #region Fetching Data from DB
        public void FetchCategories()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoriesQuery = $@"SELECT `categoryId`, `category_name`, `budget_percent`
                                                FROM `categoriestbl`
                                                WHERE `userId` = '{UserContext.SessionUserId}';";

                    using (var command = new MySqlCommand(categoriesQuery, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();

                        // list of expense categories
                        categories = new List<Category<int, string, int>>();

                        while (reader.Read())
                        {
                            int categoryId = Convert.ToInt32(reader["categoryId"]);
                            int percent = Convert.ToInt32(reader["budget_percent"]);
                            string categoryName = reader["category_name"].ToString();
                            // append to said list
                            categories.Add(new Category<int, string, int>(categoryId, categoryName, percent));
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

        public void FetchMonthlyBudget()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string budgetQuery = $@"SELECT `monthly_budget`
                                            FROM `userstbl`
                                            WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(budgetQuery, connection))
                    {
                        object result = command.ExecuteScalar();

                        // if query result is empty / null
                        if (result == DBNull.Value || result == null)
                        {
                            this.MonthlyBudget = null;
                            return;
                        }

                        this.MonthlyBudget = Convert.ToDecimal(result);

                        Debug.WriteLine(this.MonthlyBudget);
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
            FetchMonthlyBudget();
            this.Month = DateTime.Now.ToString("MMMM");
        }

        #endregion

        #region Actions
        public bool SaveBudget(List<KeyValuePair<int, int>> categoryBudgets, decimal monthlyBudget)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string budgetQuery = $@"UPDATE `userstbl`
                                            SET `monthly_budget` = '{monthlyBudget}'
                                            WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(budgetQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Key = Id, Value = Budget
                    foreach (KeyValuePair<int, int> categoryBudget in categoryBudgets)
                    {
                        Debug.WriteLine($"ID: {categoryBudget.Key} VALUE: {categoryBudget.Value}");

                        string categoryBudgetQuery = $@"UPDATE `categoriestbl`
                                                        SET `budget_percent` = {categoryBudget.Value}
                                                        WHERE `categoryId` = {categoryBudget.Key};";

                        using (var categoryCommand = new MySqlCommand(categoryBudgetQuery, connection))
                        {
                            categoryCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        #endregion
    }
}
