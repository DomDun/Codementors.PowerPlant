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

        public async Task<bool> AddInspection(Inspection inspection)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandSql = "INSERT INTO [Inspections] ([CreateDate], [Name], [State]) " +
                        "VALUES (@CreateDate, @Name, @State)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime2).Value = inspection.CreateDate;
                    command.Parameters.Add("@UpdateDate", SqlDbType.DateTime2).Value = inspection.UpdateDate == null
                        ?(object)DBNull.Value
                        : inspection.UpdateDate;
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime2).Value = inspection.EndDate == null
                        ? (object)DBNull.Value
                        : inspection.EndDate;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = inspection.MachineName;
                    command.Parameters.Add("@Comments", SqlDbType.NVarChar, 255).Value = inspection.Comments == null
                        ? (object)DBNull.Value
                        : inspection.Comments;
                    command.Parameters.Add("@State", SqlDbType.NVarChar, 255).Value = inspection.State;

                    await command.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Inspection>> GetAllInspectionsAsync()
        {
            List<Inspection> inspections = new List<Inspection>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = @"SELECT * FROM [Inspections]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        Inspection inspection = new Inspection();
                        inspection.CreateDate = DateTime.Parse(dataReader["CreateDate"].ToString());
                        inspection.UpdateDate = dataReader["UpdateDate"] == DBNull.Value
                                    ? null
                                    : (DateTime?)DateTime.Parse(dataReader["UpdateDate"].ToString());
                        inspection.EndDate = dataReader["EndDate"] == DBNull.Value
                                    ? null
                                    : (DateTime?)DateTime.Parse(dataReader["EndDate"].ToString());
                        inspection.MachineName = dataReader["Name"].ToString();
                        inspection.Comments = dataReader["Comments"] == DBNull.Value
                                    ? null
                                    : dataReader["Comments"].ToString();
                        inspection.State = Enum.TryParse(dataReader["State"].ToString(), out State cos) ? cos : inspection.State; 

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
