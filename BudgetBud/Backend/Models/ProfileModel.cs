using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using MySqlX.XDevAPI;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace BudgetBud.Backend.Models
{
    public class ProfileModel : DbConnection
    {
        public ProfileModel() { }

        public byte[] profilePic { get; private set; } = null;

        private void FetchImage()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT `profile_image` FROM `userstbl`
                                          WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // Check if the column is not null
                            if (!reader.IsDBNull(0))
                            {
                                // Read the BLOB data into a byte array
                                this.profilePic = (byte[])reader["profile_image"];
                            }
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
            FetchImage();
        }

        public byte[] CropImageToCircle(byte[] imageData)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                using (Bitmap sourceBitmap = new Bitmap(memoryStream))
                {
                    int diameter = Math.Min(sourceBitmap.Width, sourceBitmap.Height);
                    Bitmap resultBitmap = new Bitmap(diameter, diameter);

                    using (Graphics g = Graphics.FromImage(resultBitmap))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(0, 0, diameter, diameter);
                            g.SetClip(path);
                            g.DrawImage(sourceBitmap, 0, 0, diameter, diameter);
                        }
                    }

                    // Convert the resultBitmap back to a byte array
                    using (MemoryStream resultStream = new MemoryStream())
                    {
                        resultBitmap.Save(resultStream, System.Drawing.Imaging.ImageFormat.Png);
                        return resultStream.ToArray();
                    }
                }
            }
        }

        #region Actions

        public bool SetPicture(string imagePath)
        {
            try
            {
                byte[] imageData;

                // Read the image file, store to imageData as bytes
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                }

                imageData = CropImageToCircle(imageData);

                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"UPDATE userstbl 
                                          SET `profile_image` = @ImageData
                                          WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();

                    return true;
                }
                    
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            return false;
        }

        public bool IsUsernameTaken(string username)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT COUNT(*) FROM `userstbl`
                                          WHERE `username` = '{username}';";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var res = command.ExecuteScalar();

                        if(res != DBNull.Value && res != null)
                        {
                            int count = Convert.ToInt32(res);

                            if(count == 0)
                            {
                                return false;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            return true;
        }

        public void SetDetails(string fullName, string username)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"UPDATE userstbl 
                                          SET 
                                          `full_name` = '{fullName}',
                                          `username` = '{username}'  
                                          WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(userQuery, connection))
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

        public bool IsCurrentPassCorrect(string currPass)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"SELECT `password` FROM `userstbl` 
                                      WHERE `id` = {UserContext.SessionUserId}";

                    using (var command = new MySqlCommand(userQuery, connection))
                    {
                        var res = command.ExecuteScalar();

                        if (res != DBNull.Value && res != null)
                        {
                            string actualPass = res.ToString();

                            // Compare the two passwords
                            return actualPass.Equals(currPass);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            return false; // Default to false if there's an error
        }

        public void SetPassword(string newPass)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string userQuery = $@"UPDATE userstbl 
                                          SET `password` = '{newPass}'
                                          WHERE `id` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(userQuery, connection))
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
