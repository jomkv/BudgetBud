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
                    string categoriesQuery = $@"INSERT INTO `categoriestbl` (`category_name`, `budget_percent`, `userId`) 
                                                VALUES 
                                                ('Food', '20', '{userId}'), 
                                                ('Entertainment', '20', '{userId}'),
                                                ('Health and Wellness', '20', '{userId}'),
                                                ('Transportation', '20', '{userId}'),
                                                ('Housing', '20', '{userId}');";

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

                    string loginQuery = $@"SELECT `id` 
                                           FROM `userstbl`
                                           WHERE `username` = '{username}'
                                           AND `password` = '{password}'";
                    using (var command = new MySqlCommand(loginQuery, connection))
                    {
                        int userId = Convert.ToInt32(command.ExecuteScalar());

                        // if valid user (successful login)
                        if (userId != 0)
                        {
                            output = true;

                            // set current session id to userId
                            UserContext.IsLoggedIn = true;
                            UserContext.SessionUserId = userId;
                        }

                        connection.Close();

                        return output;
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
    }
}
