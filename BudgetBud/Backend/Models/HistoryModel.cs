using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetBud.Backend.Db;
using System.Data;

namespace BudgetBud.Backend.Models
{
    public class HistoryModel : DbConnection
    {
        public HistoryModel() { }

        public DataTable expenseTable { get; private set; }

        #region Fetch Data from Database

        public void FetchTable()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    string expenseQuery = $@"SELECT `expenseId` AS 'ID', `category` AS 'Category', `title` AS 'Title', `description` AS 'Description', `amount` AS 'Amount', `date` AS 'Date'
                                             FROM `expensestbl`
                                             WHERE `userId` = {UserContext.SessionUserId}";

                    using (var command = new MySqlCommand(expenseQuery, connection))
                    {
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

        public void GetData()
        {
            FetchTable();
        }

        #endregion
    }
}
