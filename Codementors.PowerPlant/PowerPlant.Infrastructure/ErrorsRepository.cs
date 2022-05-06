using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PowerPlantCzarnobyl.Infrastructure
{
    public class ErrorsRepository : IErrorsRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["PowerPlantDBConnectionString"].ConnectionString;

        public void AddError(Error error)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Errors] ([PlantName], [MachineName], [Parameter], [ErrorTime], [LoggedUser], [MaxValue], [MinValue]) " +
                        "VALUES (@PlantName, @MachineName, @Parameter, @ErrorTime, @LoggedUser, @MaxValue, @MinValue)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@PlantName", SqlDbType.NVarChar, 255).Value = error.PlantName;
                    command.Parameters.Add("@MachineName", SqlDbType.NVarChar, 255).Value = error.MachineName;
                    command.Parameters.Add("@Parameter", SqlDbType.NVarChar, 255).Value = error.Parameter;
                    command.Parameters.Add("@ErrorTime", SqlDbType.DateTime2).Value = error.ErrorTime;
                    command.Parameters.Add("@LoggedUser", SqlDbType.NVarChar, 255).Value = error.LoggedUser;
                    command.Parameters.Add("@MaxValue", SqlDbType.Int, 8).Value = error.MaxValue;
                    command.Parameters.Add("@MinValue", SqlDbType.Int, 8).Value = error.MinValue;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
