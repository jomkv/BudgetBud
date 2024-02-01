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

        #region Properties

        public List<KeyValuePair<int, string>> categories {  get; private set; } = new List<KeyValuePair<int, string>>();
        public int categoryCount { get; private set; } = 0;

        #endregion

        #region Get Data from Database

        #region Categories Info

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

        #endregion

        #region Category Count

        public void FetchCategoryCount()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoryQuery = $@"SELECT COUNT(*) 
                                              FROM `categoriestbl`
                                              WHERE `userId` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(categoryQuery, connection))
                    {
                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null) 
                        {
                            this.categoryCount = Convert.ToInt32(result);
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

        public void GetData()
        {
            FetchCategories();
            FetchCategoryCount();
        }

        #endregion

        #region Actions

        public void DeleteCategory(int id)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Delete expenses that uses this category
                            string expensesQuery = $@"DELETE FROM `expensestbl`
                                                      WHERE
                                                      `categoryId` = '{id}';";

                            using (var command = new MySqlCommand(expensesQuery, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            // Delete the category itself
                            string categoryQuery = $@"DELETE FROM `categoriestbl`
                                                      WHERE
                                                      `categoryId` = '{id}';";

                            using (var command = new MySqlCommand(categoryQuery, connection))
                            {
                                command.ExecuteNonQuery();
                            }

                            // If all goes well, save / commit changes to database
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            // If error occurs, do not save / commit changes to database
                            transaction.Rollback();
                            Debug.WriteLine($"Transaction error: {e.Message}");
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

        public void EditCategory(int id, string newName)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoryQuery = $@"UPDATE `categoriestbl`
                                              SET `category_name` = '{newName}'
                                              WHERE `categoryId` = '{id}';";

                    using (var command = new MySqlCommand(categoryQuery, connection))
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

        public void CreateCategory(string name)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string categoryQuery = $@"INSERT INTO `categoriestbl` (`category_name`, `budget_percent`, `budget_amount`, `userId`) 
                                              VALUES ('{name}', 0, 0, '{UserContext.SessionUserId}');";

                    using (var command = new MySqlCommand(categoryQuery, connection))
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

        #endregion
    }
}
