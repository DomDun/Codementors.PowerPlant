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

        public async Task<bool> AddInspectionAsync(Inspection inspection)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandSql = "INSERT INTO [Inspections] ([CreateDate], [Name], [State], [Engineer]) " +
                        "VALUES (@CreateDate, @Name, @State, @Engineer)";
                    SqlCommand command = new SqlCommand(commandSql, connection);
                    command.Parameters.Add("@CreateDate", SqlDbType.DateTime2).Value = DateTime.Now;
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
                    command.Parameters.Add("@Engineer", SqlDbType.NVarChar, 255).Value = inspection.Engineer == null
                        ? (object)DBNull.Value
                        : inspection.Engineer;

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

        public List<Inspection> GetAllInspections()
        {
            List<Inspection> inspections = new List<Inspection>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = @"SELECT * FROM [Inspections]";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Inspection inspection = new Inspection();
                        inspection.Id = int.Parse(dataReader["Id"].ToString());
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
                        inspection.Engineer = dataReader["Engineer"] == DBNull.Value
                                    ? null
                                    : dataReader["Engineer"].ToString();

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

        public Inspection GetInspection(int id)
        {
            Inspection inspection = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $"SELECT * FROM [Inspections] WHERE [Id] = {id}";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    SqlDataReader dataReader = command.ExecuteReader();

                    dataReader.Read();

                    inspection = new Inspection
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        CreateDate = DateTime.Parse(dataReader["CreateDate"].ToString()),
                        UpdateDate = dataReader["UpdateDate"] == DBNull.Value
                                    ? null
                                    : (DateTime?)DateTime.Parse(dataReader["UpdateDate"].ToString()),
                        EndDate = dataReader["EndDate"] == DBNull.Value
                                    ? null
                                    : (DateTime?)DateTime.Parse(dataReader["EndDate"].ToString()),
                        MachineName = dataReader["Name"].ToString(),
                        Comments = dataReader["Comments"] == DBNull.Value
                                    ? null
                                    : dataReader["Comments"].ToString(),
                        State = Enum.TryParse(dataReader["State"].ToString(), out State cos) ? cos : inspection.State,
                        Engineer = dataReader["Engineer"] == DBNull.Value
                                    ? null
                                    : dataReader["Engineer"].ToString(),
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                inspection = null;
            }

            return inspection;
        }

        public bool UpdateInspection(int id, Inspection inspection)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandText = $@"
UPDATE [Inspections] SET 
[UpdateDate] = @UpdateDate,
[Comments] = @Comments,
[State] = @State,
[Engineer] = @Engineer 
WHERE ID = {id}";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@UpdateDate", SqlDbType.DateTime2).Value = inspection.UpdateDate;
                    command.Parameters.Add("@Comments", SqlDbType.NVarChar, 255).Value = inspection.Comments;
                    command.Parameters.Add("@State", SqlDbType.NVarChar, 255).Value = inspection.State;
                    command.Parameters.Add("@Engineer", SqlDbType.NVarChar, 255).Value = inspection.Engineer;

                    int rowsAffected =  command.ExecuteNonQuery();

                    success = rowsAffected == 1;
                    if (!success)
                    {
                        Console.WriteLine("Error when adding inspection");
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "Error when updating author with Id {Id}", inspection.Id);
                success = false;
            }

            return success;
        }
    }
}
