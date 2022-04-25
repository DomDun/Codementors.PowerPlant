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
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["PowerPlantDBConnectionString"].ConnectionString;

        public bool AddMember(Member member)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Members] ([Login], [Password], [Role]) " +
                        "VALUES (@Login, @Password, @Role)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@Login", SqlDbType.NVarChar, 255).Value = member.Login;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = member.Password;
                    command.Parameters.Add("@Role", SqlDbType.NVarChar, 255).Value = member.Role;

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

        public Member GetMember(string login)
        {
            Member member = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $"SELECT * FROM [Members] WHERE [Login] = @Login";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Login", SqlDbType.NVarChar, 255).Value = login;

                    SqlDataReader dataReader = command.ExecuteReader();

                    dataReader.Read();

                    member = new Member
                    {
                        Login = dataReader["Login"].ToString(),
                        Password = dataReader["Password"].ToString(),
                        Role = dataReader["Role"].ToString(),
                    };

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                member = null;
            }

            return member;
        }

        public bool DeleteMember(string login)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $"DELETE FROM [Members] WHERE [Login] = '{login}'";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    int rowsAffected = command.ExecuteNonQuery();

                    success = rowsAffected == 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }
    }
}
