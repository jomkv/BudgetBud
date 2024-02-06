using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using System.Data;
using BudgetBud.Pages.Menus;
using System.ComponentModel;

namespace BudgetBud.Backend.Models
{
    public class HistoryModel : DbConnection
    {
        public HistoryModel() { }

        public DataTable expenseTable { get; private set; }
        public List<KeyValuePair<int, string>> categories { get; private set; }
        public string ExpenseTitle { get; set; }
        public int? CategoryId { get; set; }

        #region Fetch Data from Database

        public void FetchTable()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    var parameters = new List<MySqlParameter>();
                    parameters.Add(new MySqlParameter("@userId", UserContext.SessionUserId));

                    string expenseQuery = @"SELECT `expenseId` AS 'ID', `category` AS 'Category', `title` AS 'Title', `description` AS 'Description', `amount` AS 'Amount', `date` AS 'Date'
                                     FROM `expensestbl`
                                     WHERE `userId` = @userId";

                    if (!string.IsNullOrEmpty(ExpenseTitle))
                    {
                        expenseQuery += " AND `title` LIKE @title";
                        parameters.Add(new MySqlParameter("@title", $"%{ExpenseTitle}%"));
                    }

                    if (CategoryId.HasValue)
                    {
                        expenseQuery += " AND `categoryId` = @categoryId";
                        parameters.Add(new MySqlParameter("@categoryId", CategoryId));
                    }
                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.Parameters.AddRange(parameters.ToArray());

                        DataTable dataTable = new DataTable();

                        using (var adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        this.expenseTable = dataTable;
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
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
                            Debug.WriteLine($"HERE {categoryName}");
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
            FetchTable();
            FetchCategories();
        }

        #endregion

        #region Actions

        public void DeleteExpense (int expenseId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"DELETE FROM `expensestbl`
                                             WHERE `expenseid` = {expenseId};";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        public bool EditExpense (int expenseId, int categoryId, string category, string title, string desc, decimal amount, string date)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"UPDATE `expensestbl`
                                             SET `categoryId` = '{categoryId}',
                                                 `category` = '{category}',
                                                 `title` = '{title}',
                                                 `description` = '{desc}',
                                                 `amount` = '{amount}',
                                                 `date` = '{date}'
                                             WHERE `expenseId` = {expenseId};";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
                return false;
            }
        }

        #endregion
    }
}
