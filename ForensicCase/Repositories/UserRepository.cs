using ForensicCase.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ForensicCase.Repositories
{
    public class UserRepository
    {
        public readonly string ConnectionString;
        public UserRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        public DataTable GetUserList(string search = null)
        {
            DataTable dt = new DataTable();
            var query = "select * from application_user where search = " + search;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var da = new MySqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public ApplicationUser GetSingleUser(string username, string password)
        {
            ApplicationUser user = new ApplicationUser();
            var query = "select * from application_user where username = " + username;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var da = new MySqlCommand(query, conn);
                da.Fill(dt);
            }
            return user;
        }
    }
}
