using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetBud.Backend.Db;
using System.Drawing;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
                                                ('Food', '0', '0', '{userId}'), 
                                                ('Entertainment', '0', '0', '{userId}'),
                                                ('Health and Wellness', '0', '0', '{userId}'),
                                                ('Transportation', '0', '0', '{userId}'),
                                                ('Housing', '0', '0', '{userId}');";

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

                    string loginQuery = $@"SELECT `id`, `username`, `status`, `full_name`, `profile_image`
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

                                // if valid user (successful login)
                                if (userId != 0)
                                {
                                    string fetchedUsername = reader.GetString("username");
                                    string fetchedStatus = reader.GetString("status");
                                    string fetchedFullname = reader.GetString("full_name");

                                    // Image res
                                    var res = reader["profile_image"];

                                    // Set profile pic if not null
                                    if (res != DBNull.Value && res != null)
                                    {
                                        byte[] profilePicRaw = (byte[])res;

                                        MemoryStream ms = new MemoryStream(profilePicRaw);
                                        Image image = Image.FromStream(ms);

                                        UserContext.ProfilePic = image;
                                    }

                                    // Set current session values
                                    UserContext.IsLoggedIn = true;
                                    UserContext.SessionUserId = userId;
                                    UserContext.UserName = fetchedUsername;
                                    UserContext.Status = fetchedStatus;
                                    UserContext.FullName = fetchedFullname;

                                    output = true;
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

        public void FetchProfilePic(int userId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT `profile_image`
                                           FROM `userstbl`
                                           WHERE `id` = {userId};";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var res = command.ExecuteScalar();

                        if (res != DBNull.Value && res != null)
                        {
                            byte[] profilePicRaw = (byte[])res;

                            MemoryStream ms = new MemoryStream(profilePicRaw);
                            Image image = Image.FromStream(ms);

                            UserContext.ProfilePic = image;
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

        public void FetchDetails(int userId)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT `full_name`, `username`
                                           FROM `userstbl`
                                           WHERE `id` = {userId};";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string fullname = reader.GetString("full_name");
                            string username = reader.GetString("username");

                            UserContext.UserName = username;
                            UserContext.FullName = fullname;
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
    }
}
