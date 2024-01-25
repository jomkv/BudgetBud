using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BudgetBud.Backend.Db
{
    public abstract class DbConnection
    {
        private readonly string connectionString;

        public DbConnection()
        {
            connectionString = "server=localhost;database=budgetbud;uid=admin;pwd=admin;";
        }

        protected MySqlConnection GetConnection()
        {
            // returns connection
            return new MySqlConnection(connectionString);
        }
    }
}
