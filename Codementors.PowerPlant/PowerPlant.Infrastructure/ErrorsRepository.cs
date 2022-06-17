using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

                    string commandSql = "INSERT INTO [Errors] ([PlantName], [MachineName], [Parameter], [ErrorTime], [LoggedUser], [MinValue], [MaxValue]) " +
                        "VALUES (@PlantName, @MachineName, @Parameter, @ErrorTime, @LoggedUser, @MinValue, @MaxValue)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@PlantName", SqlDbType.NVarChar, 255).Value = error.PlantName;
                    command.Parameters.Add("@MachineName", SqlDbType.NVarChar, 255).Value = error.MachineName;
                    command.Parameters.Add("@Parameter", SqlDbType.NVarChar, 255).Value = error.Parameter;
                    command.Parameters.Add("@ErrorTime", SqlDbType.DateTime2).Value = error.ErrorTime;
                    command.Parameters.Add("@LoggedUser", SqlDbType.NVarChar, 255).Value = error.LoggedUser;
                    command.Parameters.Add("@MinValue", SqlDbType.Int, 8).Value = error.MinValue;
                    command.Parameters.Add("@MaxValue", SqlDbType.Int, 8).Value = error.MaxValue;
                    
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Error> GetAllErrorsAsync(DateTime startData, DateTime endData)
        {
            List<Error> errors = new List<Error>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = @"
SELECT * FROM [Errors]  
      WHERE ErrorTime > @startData
      AND ErrorTime < @endData";
                    SqlCommand command = new SqlCommand(commandText, connection);

                    command.Parameters.Add("@startData", SqlDbType.DateTime2).Value = startData;
                    command.Parameters.Add("@endData", SqlDbType.DateTime2).Value = endData;

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Error error;

                        try
                        {
                            error = new Error
                            {
                                PlantName = dataReader["PlantName"].ToString(),
                                MachineName = dataReader["MachineName"].ToString(),
                                Parameter = dataReader["Parameter"].ToString(),
                                ErrorTime = DateTime.Parse(dataReader["ErrorTime"].ToString()),
                                LoggedUser = dataReader["LoggedUser"].ToString(),
                                MinValue = double.Parse(dataReader["MinValue"].ToString()),
                                MaxValue = double.Parse(dataReader["MaxValue"].ToString()),
                            };
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        errors.Add(error);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                errors = null;
            }

            return errors;
        }
    }
}
