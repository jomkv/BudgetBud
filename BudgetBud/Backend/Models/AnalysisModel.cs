using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using MySqlX.XDevAPI;

namespace BudgetBud.Backend.Models
{
    public class AnalysisModel : DbConnection
    {
        public AnalysisModel() { }

        public DateTime fromDate { get; set; } = DateTime.MinValue;
        public DateTime toDate { get; set; } = DateTime.MaxValue;

        public List<KeyValuePair<int, string>> expenseCount { get; private set; } = new List<KeyValuePair<int, string>>();
        public List<KeyValuePair<double, string>> categoryTotalSpent { get; private set; } = new List<KeyValuePair<double, string>>();
        public decimal userTotalSpent { get; private set; } = decimal.Zero;
        public int totalExpenseLogged { get; private set; } = 0;
        public string favoriteCategory { get; private set; } = "No Expenses";
        public decimal avgDailySpent { get; private set; } = 0;

        private void FetchExpenseCount ()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT category, COUNT(*) as expenseCount
                                             FROM expensestbl
                                             WHERE userId = {UserContext.SessionUserId}
                                             AND date BETWEEN @fromDate AND @toDate
                                             GROUP BY category;";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fromDate", fromDate);
                        command.Parameters.AddWithValue("@toDate", toDate);

                        var reader = command.ExecuteReader();

                        this.expenseCount = new List<KeyValuePair<int, string>>();

                        while (reader.Read())
                        {
                            string category = reader["category"].ToString();
                            int expenseCount = Convert.ToInt32(reader["expenseCount"]);

                            // Append count to list
                            this.expenseCount.Add(new KeyValuePair<int, string>(expenseCount, category));
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

        private void FetchCategoryTotalSpent()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT category, SUM(`amount`) as totalSpent
                                             FROM expensestbl
                                             WHERE userId = {UserContext.SessionUserId}
                                             AND date BETWEEN @fromDate AND @toDate
                                             GROUP BY category;";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fromDate", fromDate);
                        command.Parameters.AddWithValue("@toDate", toDate);

                        var reader = command.ExecuteReader();

                        this.categoryTotalSpent = new List<KeyValuePair<double, string>>();

                        while (reader.Read())
                        {
                            string category = reader["category"].ToString();
                            double expenseCount = Convert.ToDouble(reader["totalSpent"]);

                            // Append count to list
                            this.categoryTotalSpent.Add(new KeyValuePair<double, string>(expenseCount, category));
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

        private void FetchTotalExpenseLogged()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT COUNT(*) FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}
                                             AND date BETWEEN @fromDate AND @toDate;";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fromDate", fromDate);
                        command.Parameters.AddWithValue("@toDate", toDate);

                        var result = command.ExecuteScalar();

                        if(result != DBNull.Value && result != null)
                        {
                            this.totalExpenseLogged = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception e )
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        private void FetchUserTotalSpent()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT SUM(`amount`) FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}
                                             AND date BETWEEN @fromDate AND @toDate;";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fromDate", fromDate);
                        command.Parameters.AddWithValue("@toDate", toDate);

                        var result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            userTotalSpent = Convert.ToDecimal(result);

                            // Calculate avg daily spent

                            int numberOfDays = (int)(toDate - fromDate).TotalDays;

                            if (fromDate == DateTime.MinValue && toDate == DateTime.MaxValue) // If "all time" is selected
                            {
                                numberOfDays = GetNumOfDaysAllTime();
                            }

                            if (numberOfDays == 0)
                            {
                                avgDailySpent = Math.Round(userTotalSpent, 2);
                                return;
                            }
                            

                            decimal averageDailySpent = userTotalSpent / numberOfDays;
                            avgDailySpent = Math.Round(averageDailySpent, 2);
                        }
                        else
                        {
                            userTotalSpent = 0;
                            avgDailySpent = 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }
        }

        private int GetNumOfDaysAllTime()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string dateQuery = $@"SELECT MIN(`date`) AS earliestDate, MAX(`date`) AS latestDate
                                          FROM `expensestbl`
                                          WHERE `userId` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(dateQuery, connection))
                    {
                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            DateTime earliestDate = Convert.ToDateTime(reader["earliestDate"]);
                            DateTime latestDate = Convert.ToDateTime(reader["latestDate"]);

                            int numberOfDays = (int)(latestDate - earliestDate).TotalDays;
                            return numberOfDays;
                        }

                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error: {e.Message}");
            }

            return 0;
        }

        public void GetFavoriteCategory()
        {
            if (expenseCount.Count == 0)
            {
                // Handle the case where there are no expenses
                this.favoriteCategory = "No Expenses";
                return;
            }

            // Find the category with the highest count
            KeyValuePair<int, string> favoriteCategory = expenseCount.OrderByDescending(x => x.Key).First();

            this.favoriteCategory = favoriteCategory.Value;
        }

        public void GetData()
        {
            FetchExpenseCount();
            FetchCategoryTotalSpent();
            FetchTotalExpenseLogged();
            FetchUserTotalSpent();
            GetFavoriteCategory();
        }
    }
}
