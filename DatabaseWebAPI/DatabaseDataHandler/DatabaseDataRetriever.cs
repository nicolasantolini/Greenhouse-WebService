using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DatabaseGateway.DatabaseDataHandler;
using DatabaseGateway.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;

namespace DatabaseWebAPI.DatabaseDataHandler
{

    public class DatabaseDataRetriever : IDataManager
    {
        private SqlConnection _connection;

        private String server_name = "database-1.cf0m6bhmfrsf.eu-west-1.rds.amazonaws.com";
        private String database_name = "GreenHouse";
        private readonly String connectionString;

        public DatabaseDataRetriever()
        {
            connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = server_name,
                InitialCatalog = database_name,
                UserID = "admin",
                Password = "rootroot1234"
            }.ConnectionString;
            Console.WriteLine(connectionString);
        }

        public async Task<List<Greenhouse>> GetGreenhouses(string userEmail)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();
                //open connection

                List<Greenhouse> greenhouses = new List<Greenhouse>();

                SqlCommand command = new SqlCommand("SELECT * FROM Greenhouses WHERE Owner = @0", _connection);
                //create command
                command.Parameters.Add(new SqlParameter("0", userEmail));
                //add parameters
                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Greenhouse newGreenhouse = new Greenhouse((int) reader[0], (string) reader[1], (string) reader[2],
                        (string) reader[3], Decimal.ToDouble((decimal) reader[4])
                        , (bool) reader[8], Decimal.ToDouble((decimal) reader[6]), Decimal.ToDouble((decimal) reader[7]),Decimal.ToDouble((decimal) reader[5]),(string) reader[9]);
                    //switched reader 8 and 5 because greenhouse constructor has different order of arguments compared to db table
                    newGreenhouse.Logs = GetAllLogs(newGreenhouse.Id).Result;
                    newGreenhouse.Plants = GetPlants(newGreenhouse.Id).Result;
                    greenhouses.Add(newGreenhouse);
                }

                return greenhouses;
            }
        }

        public async Task<Greenhouse> GetGreenhouse(int greenhouseId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                Greenhouse newGreenhouse = new Greenhouse();

                SqlCommand command = new SqlCommand("SELECT * FROM Greenhouses WHERE Id_Greenhouse = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouseId));


                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    newGreenhouse = new Greenhouse((int) reader[0], (string) reader[1], (string) reader[2],
                        (string) reader[3], Decimal.ToDouble((decimal) reader[4])
                        , (bool) reader[8], Decimal.ToDouble((decimal) reader[6]),
                        Decimal.ToDouble((decimal) reader[7]), Decimal.ToDouble((decimal) reader[5]),(string) reader[9]);
                    newGreenhouse.Logs = GetAllLogs(newGreenhouse.Id).Result;
                    newGreenhouse.Plants = GetPlants(newGreenhouse.Id).Result;
                }

                await reader.CloseAsync();
                return newGreenhouse;
            }
        }

        public async Task UpdateGreenhouse(Greenhouse greenhouse)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                SqlCommand command =
                    new SqlCommand(
                        "UPDATE Greenhouses SET name = @0, description = @1, location = @2, area = @3, co2Preferred = @4, temperaturePreferred = @5, humidityPreferred = @6 WHERE Id_Greenhouse = @7",
                        _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouse.Name));
                command.Parameters.Add(new SqlParameter("1", greenhouse.Description));
                command.Parameters.Add(new SqlParameter("2", greenhouse.Location));
                
                command.Parameters.AddWithValue("3",(decimal)greenhouse.Area);
                command.Parameters.AddWithValue("4",(decimal)greenhouse.Co2Preferred);
                command.Parameters.AddWithValue("5",(decimal)greenhouse.TemperaturePreferred);
                command.Parameters.AddWithValue("6",(decimal)greenhouse.HumidityPreferred);
                
                command.Parameters.Add(new SqlParameter("7", greenhouse.Id));

                command.ExecuteNonQuery();
            }
        }

        public async Task CreateGreenhouse(Greenhouse greenhouse)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                string addGreenhouse =
                    "INSERT INTO Greenhouses (name, description, location, area, co2Preferred, temperaturePreferred, humidityPreferred ,actuator, owner) VALUES (@0, @1, @2, @3 ,@4, @5, @6, @7, @8)";

                await using (SqlCommand queryInsertGreenhouse = new SqlCommand(addGreenhouse))
                {
                    queryInsertGreenhouse.Connection = _connection;
                    queryInsertGreenhouse.Parameters.Add("@0", SqlDbType.VarChar, 50).Value = greenhouse.Name;
                    queryInsertGreenhouse.Parameters.Add("@1", SqlDbType.VarChar, 200).Value = greenhouse.Description;
                    queryInsertGreenhouse.Parameters.Add("@2", SqlDbType.VarChar, 50).Value = greenhouse.Location;
                    queryInsertGreenhouse.Parameters.Add("@3", SqlDbType.Decimal).Value = greenhouse.Area;
                    queryInsertGreenhouse.Parameters.Add("@4", SqlDbType.Decimal).Value = greenhouse.Co2Preferred;
                    queryInsertGreenhouse.Parameters.Add("@5", SqlDbType.Decimal).Value = greenhouse.TemperaturePreferred;
                    queryInsertGreenhouse.Parameters.Add("@6", SqlDbType.Decimal).Value = greenhouse.HumidityPreferred;
                    queryInsertGreenhouse.Parameters.Add("@7", SqlDbType.Bit).Value = false;
                    queryInsertGreenhouse.Parameters.Add("@8", SqlDbType.VarChar, 50).Value = "test@test.com";

                    queryInsertGreenhouse.ExecuteNonQuery();
                }
            }
        }

        public async Task RemoveGreenhouse(int greenhouseId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Greenhouses WHERE Id_Greenhouse = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouseId));

                command.ExecuteNonQuery();
            }
        }

        public async Task<List<Plant>> GetPlants(int greenhouseId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                List<Plant> plants = new List<Plant>();

                SqlCommand command = new SqlCommand("SELECT * FROM Plants WHERE Id_Greenhouse = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouseId));

                await using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Plant plant = new Plant(
                        (int) reader[0],
                        (string) reader[1],
                        (string) reader[2],
                        (string) reader[3],
                        (string) reader[4],
                        (int) reader[5]);

                    plants.Add(plant);
                }

                return plants;
            }
        }

        public async Task UpdatePlant(Plant plant)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                SqlCommand command =
                    new SqlCommand(
                        "UPDATE Plants SET name = @0, description = @1, type = @2 scientificName = @3, Id_Greenhouse = @4 WHERE Id_Plant = @5",
                        _connection);

                command.Parameters.Add(new SqlParameter("0", plant.Name));
                command.Parameters.Add(new SqlParameter("1", plant.Description));
                command.Parameters.Add(new SqlParameter("2", plant.Type));
                command.Parameters.Add(new SqlParameter("3", plant.ScientificName));
                command.Parameters.Add(new SqlParameter("4", plant.Id_Greenhouse));
                command.Parameters.Add(new SqlParameter("5", plant.Id));

                command.ExecuteNonQuery();
            }
        }

        public async Task RemovePlant(int plantId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Plants WHERE Id_plant = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", plantId));

                command.ExecuteNonQuery();
            }
        }

        public async Task CreatePlant(Plant plant)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                string addPlant =
                    "INSERT INTO Plants (name, description, type, scientificName, Id_Greenhouse) VALUES (@0, @1, @2, @3, @4)";

                await using (SqlCommand queryInsertPlant = new SqlCommand(addPlant))
                {
                    queryInsertPlant.Connection = _connection;
                    queryInsertPlant.Parameters.Add("@0", SqlDbType.VarChar, 50).Value = plant.Name;
                    queryInsertPlant.Parameters.Add("@1", SqlDbType.VarChar, 200).Value = plant.Description;
                    queryInsertPlant.Parameters.Add("@2", SqlDbType.VarChar, 50).Value = plant.Type;
                    queryInsertPlant.Parameters.Add("@3", SqlDbType.VarChar, 100).Value = plant.ScientificName;
                    queryInsertPlant.Parameters.Add("@4", SqlDbType.Decimal).Value = plant.Id_Greenhouse;

                    queryInsertPlant.ExecuteNonQuery();
                }
            }
        }

        public async Task<List<Log>> GetAllLogs(int greenhouseId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                List<Log> logs = new List<Log>();

                SqlCommand command = new SqlCommand("SELECT * FROM Logs WHERE Id_Greenhouse = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouseId));

                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Log log = new Log(
                        (int) reader[0], 
                        Decimal.ToDouble((decimal) reader[1]), 
                        Decimal.ToDouble((decimal) reader[2]), 
                        Decimal.ToDouble((decimal) reader[3]),
                        (DateTime)reader[4],
                        (int) reader[5]);

                    logs.Add(log);
                }

                return logs;
            }
        }

        public async Task<Log> GetLastLog(int greenhouseId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();
                Log log = new Log();
                SqlCommand command =
                    new SqlCommand(
                        "SELECT TOP 1 * FROM Logs WHERE Id_Greenhouse = @0 ORDER BY Id_Log DESC",
                        _connection);

                command.Parameters.Add(new SqlParameter("0", greenhouseId));
                await using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    log = new Log(
                        (int) reader[0], 
                        Decimal.ToDouble((decimal) reader[1]),
                        Decimal.ToDouble((decimal) reader[2]),
                        Decimal.ToDouble((decimal) reader[3]),
                        (DateTime)reader[4],
                        (int) reader[5]);
                }
                return log;
            }
        }

        public async Task<Log> GetLog(int logId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                Log toReturn = new Log();
                SqlCommand command = new SqlCommand("SELECT * FROM Logs WHERE Id_log = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", logId));

                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    toReturn = new Log((int) reader[0], 
                        Decimal.ToDouble((decimal) reader[1]), 
                        Decimal.ToDouble((decimal) reader[2]), 
                        Decimal.ToDouble((decimal) reader[3]),
                        (DateTime) reader[4],
                        (int) reader[5]);
                }

                return toReturn;
            }
        }

        public async Task<List<Routine>> GetAllRoutinesOfPlant(int plantId)
        {
            await using (_connection = new SqlConnection(connectionString))
            {
                _connection.Open();

                List<Routine> routines = new List<Routine>();

                SqlCommand command = new SqlCommand("SELECT * FROM Routines WHERE Id_Plant = @0", _connection);

                command.Parameters.Add(new SqlParameter("0", plantId));

                await using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    Routine routine = new Routine(
                        (int) reader[0],
                        (string) reader[1],
                        (string) reader[2],
                        (int) reader[3]);

                    routines.Add(routine);
                }

                return routines;
            }
        }

        public async Task<bool> SetActuatorTrue(int greenhouseId)
        {
            try
            {
                await using (_connection = new SqlConnection(connectionString))
                {
                    _connection.Open();

                    SqlCommand command =
                        new SqlCommand(
                            "UPDATE Greenhouses SET actuator = 1 WHERE Id_Greenhouse = @0",
                            _connection);

                    command.Parameters.Add(new SqlParameter("0", greenhouseId));

                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}