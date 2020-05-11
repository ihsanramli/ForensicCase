using ForensicCase.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ForensicCase.Repositories
{
    public class ForensicRepository
    {
        public readonly string ConnectionString;
        private IConfiguration Configuration;
        public ForensicRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("Default");
        }
        public DataTable GetFlowerList(string search = null, string order = null)
        {
            DataTable dt = new DataTable();
            string str_search = "";

            if(!String.IsNullOrEmpty(search))
            {
                str_search += "flower_name = '" + search + "'";
            }
            else
            {
                str_search += "1=1";
            }

            var query = "select * from application_flower where " + str_search;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                var da = new MySqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public ApplicationUser GetSingleUser(string username)
        {
            ApplicationUser user = new ApplicationUser();
            var query = "select * from application_user where username = '" + username + "' limit 1";
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand(query, conn);

                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.address = rdr["address"].ToString();
                    user.full_name = rdr["full_name"].ToString();
                    user.email_address = rdr["email_address"].ToString();
                    user.age = (int)rdr["age"];
                    user.matric_no = rdr["matric_no"].ToString();
                    user.username = rdr["username"].ToString();
                    user.phone_number = rdr["phone_number"].ToString();
                    user.id = (int)rdr["id"];
                    user.identification_no = (long)rdr["identification_no"];
                }
            }
            return user;
        }

        public void ResetDb()
        {
            var query = "PSP_RESET";
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd = new MySqlCommand(query, conn);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
