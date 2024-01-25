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
    public class AnalysisModel : DbConnection
    {
        public AnalysisModel() { }

        public DateTime fromDate { get; set; } = DateTime.MinValue;
        public DateTime toDate { get; set; } = DateTime.MaxValue;

        public List<KeyValuePair<int, string>> expenseCount { get; private set; } = new List<KeyValuePair<int, string>>();
        public List<KeyValuePair<double, string>> categoryTotalSpent { get; private set; } = new List<KeyValuePair<double, string>>();
        public int totalExpenseLogged { get; private set; } = 0;

        public void FetchExpenseCount ()
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

        public void FetchCategoryTotalSpent()
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
                                             WHERE `userId` = {UserContext.SessionUserId};";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
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

        public void GetData()
        {
            FetchExpenseCount();
            FetchCategoryTotalSpent();
            FetchTotalExpenseLogged();
        }
    }
}
