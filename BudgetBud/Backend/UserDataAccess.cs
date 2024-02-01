using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using MySql.Data.MySqlClient;

namespace BudgetBud.Backend
{
    public class UserDataAccess : DbConnection
    {

        public UserDataAccess() { }

        #region Username Taken

        public bool IsUserTaken(string username)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT COUNT(*)
                                          FROM `userstbl`
                                          WHERE `username` = '{username}';";

                    using (var command = new MySqlCommand(userQuery, connection)) 
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
                return false;
            }
        }

        #endregion

        #region Register
        // returns true if register successful, false if error
        public bool Register(string fullName, string username, string password)
        {
            int userId;
            bool output = false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // create user
                    string userQuery = $@"INSERT INTO `userstbl` (`full_name`, `username`, `password`) 
                                          VALUES ('{fullName}', '{username}', '{password}'); 
                                          SELECT LAST_INSERT_ID();";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        userId = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // initialize categories table
                    string categoriesQuery = $@"INSERT INTO `categoriestbl` (`category_name`, `budget_percent`, `budget_amount`, `userId`) 
                                                VALUES 
                                                ('Food', '20', '0', '{userId}'), 
                                                ('Entertainment', '20', '0', '{userId}'),
                                                ('Health and Wellness', '20', '0', '{userId}'),
                                                ('Transportation', '20', '0', '{userId}'),
                                                ('Housing', '20', '0', '{userId}');";

                    using (var command = new MySqlCommand(categoriesQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                output = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            return output;
        }

        #endregion

        #region Login
        // returns true if login successful, false if error / wrong credentials
        public bool GetLoginAuthentication(string username, string password)
        {
            bool output = false;

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string loginQuery = $@"SELECT `id`, `username`, `status`
                                           FROM `userstbl`
                                           WHERE `username` = '{username}'
                                           AND `password` = '{password}'";

                    using (var command = new MySqlCommand(loginQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            // Check if any rows are returned
                            if (reader.Read())
                            {
                                // Read values from the reader
                                int userId = reader.GetInt32("id");
                                string fetchedUsername = reader.GetString("username");
                                string fetchedStatus = reader.GetString("status");

                                // if valid user (successful login)
                                if (userId != 0)
                                {
                                    output = true;

                                    // Set current session values
                                    UserContext.IsLoggedIn = true;
                                    UserContext.SessionUserId = userId;
                                    UserContext.UserName = fetchedUsername;
                                    UserContext.Status = fetchedStatus;
                                }
                            }
                        }
                    }

                    connection.Close();
                    return output;
                }
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
