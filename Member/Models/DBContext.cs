using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Member.Models
{
    public class DBContext
    {
        string conStr = ConfigurationManager.ConnectionStrings["Member"].ConnectionString;

        public DataSet ExcuteSearch(string queryString)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(queryString, con);
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);

                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ds;
        }

        public DataSet ExcuteSearch(string queryString, string name)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(queryString, con);
                    command.Parameters.Add("@Name", SqlDbType.NChar, 10);
                    command.Parameters["@Name"].Value = name;
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);

                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ds;
        }

        public DataSet ExcuteSearch(string queryString, int id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                try
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand(queryString, con);
                    command.Parameters.Add("@Id", SqlDbType.Int, 10);
                    command.Parameters["@Id"].Value = id;
                    adapter.SelectCommand = command;
                    adapter.Fill(ds);

                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ds;
        }

        public void ExcuteCreate(string queryString, string name, string email)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.Add("@Name", SqlDbType.NChar, 10, "Name");
                command.Parameters.Add("@Email", SqlDbType.NChar, 20, "Email");
                command.Parameters["@Name"].Value = name;
                command.Parameters["@Email"].Value = email;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ExcuteEdit(string queryString, int id, string name, string email)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                command.Parameters.Add("@Id", SqlDbType.Int, 10, "Id");
                command.Parameters.Add("@Name", SqlDbType.NChar, 10, "Name");
                command.Parameters.Add("@Email", SqlDbType.NChar, 20, "Email");
                command.Parameters["@Id"].Value = id;
                command.Parameters["@Name"].Value = name;
                command.Parameters["@Email"].Value = email;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void ExcuteDelete(string queryString, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand command = new SqlCommand(queryString, con);
                SqlParameter parameter = command.Parameters.Add("@Id", SqlDbType.Int, 10, "Id");
                command.Parameters["@Id"].Value = id;
                //parameter.SourceVersion = DataRowVersion.Original;
                command.Connection.Open();
                command.ExecuteNonQuery();

            }
        }
    }
}