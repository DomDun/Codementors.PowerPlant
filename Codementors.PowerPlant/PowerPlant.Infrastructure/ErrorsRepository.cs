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

                    string commandSql = "INSERT INTO [Errors] ([PlantName], [MachineName], [MachineValue], [Unit], [Date]) " +
                        "VALUES (@PlantName, @MachineName, @MachineValue, @Unit, @Date)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@PlantName", SqlDbType.NVarChar, 255).Value = error.PlantName;
                    command.Parameters.Add("@MachineName", SqlDbType.NVarChar, 255).Value = error.MachineName;
                    command.Parameters.Add("@MachineValue", SqlDbType.Float, 8).Value = error.MachineValue;
                    command.Parameters.Add("@Unit", SqlDbType.NVarChar, 255).Value = error.Unit;
                    command.Parameters.Add("@Date", SqlDbType.DateTime2).Value = error.errorTime;
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
