using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PowerPlantCzarnobyl.Infrastructure
{
   

    public class MembersRepository : IMembersRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["PowerPlantConnectionString"].ConnectionString;

        public bool AddUser(Member user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Users] ([Login], [Password], [Role]) " +
                        "VALUES (@Login, @Password, @Role)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@Login", SqlDbType.NVarChar, 255).Value = user.Login;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = user.Password;
                    command.Parameters.Add("@Role", SqlDbType.NVarChar, 255).Value = user.Role;

                    if (command.ExecuteNonQuery() == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return false;
        }

        public Member GetUser(string username)
        {
            Member user = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $"SELECT * FROM [Users] WHERE [UserName] = @UserName";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 255).Value = username;

                    SqlDataReader dataReader = command.ExecuteReader();

                    dataReader.Read();

                    user = new Member
                    {
                        Login = dataReader["UserName"].ToString(),
                        Password = dataReader["Password"].ToString(),
                        Role = dataReader["Role"].ToString(),
                    };

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                user = null;
            }

            return user;
        }
    }
}
