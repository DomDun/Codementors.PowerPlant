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
    public class InspectionRepository : IInspectionRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["PowerPlantDBConnectionString"].ConnectionString;

        public bool AddInspection(Inspection inspection)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Inspections] ([CreateDate], [UpdateDate], [EndDate], [Name], [Comments]) " +
                        "VALUES (@CreateDate, @UpdateDate, @EndDate, @Name, @Comments)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime2).Value = inspection.CreateDate;
                    command.Parameters.Add("@UpdateDate", SqlDbType.DateTime2).Value = inspection.UpdateDate;
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime2).Value = inspection.EndDate;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = inspection.Name;
                    command.Parameters.Add("@Comments", SqlDbType.NVarChar, 255).Value = inspection.Comments;

                    command.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Inspection>> GetInspectionsAsync(DateTime startData, DateTime endData)
        {
            List<Inspection> inspections = new List<Inspection>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"
SELECT * FROM [Inspections]  
      WHERE CreateDate > @startData
      AND CreateDate < @endData";
                    SqlCommand command = new SqlCommand(commandText, connection);

                    command.Parameters.Add("@startData", SqlDbType.DateTime2).Value = startData;
                    command.Parameters.Add("@endData", SqlDbType.DateTime2).Value = endData;

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        Inspection inspection;

                        try
                        {
                            inspection = new Inspection
                            {
                                CreateDate = DateTime.Parse(dataReader["CreateDate"].ToString()),
                                UpdateDate = DateTime.Parse(dataReader["UpdateDate"].ToString()),
                                EndDate = DateTime.Parse(dataReader["EndDate"].ToString()),
                                Name = dataReader["Name"].ToString(),
                                Comments = dataReader["Comments"].ToString(),
                            };
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        inspections.Add(inspection);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                inspections = null;
            }

            return inspections;
        }
    }
}
