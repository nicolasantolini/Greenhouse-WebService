using System.Data.SqlClient;
using DatabaseGateway.Model;

namespace DatabaseGateway.DatabaseDataHandler;

public class DatabaseDataRetriever : IDataManager
{
    private SqlConnection _connection;

    public DatabaseDataRetriever()
    {
        using (_connection = new SqlConnection("Server=[server_name];Database=[database_name];Trusted_Connection=true"))
        {
            _connection.Open();
        }
    }
    
    public List<Greenhouse> GetGreenhouses(string userEmail)
    {
        List<Greenhouse> greenhouses = new List<Greenhouse>();
        // Create the command  
        SqlCommand command = new SqlCommand("SELECT * FROM Greenhouses WHERE Owner = @0", _connection);  
        // Add the parameters.  
        command.Parameters.Add(new SqlParameter("0", userEmail));

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Greenhouse newGreenhouse = new Greenhouse( (int) reader[0], (string) reader[1], (string) reader[2], (string) reader[3], (float) reader[4]
                ,(ushort) reader[5],(float) reader[6], (float) reader[7],(float) reader[8]);
            newGreenhouse.Logs=GetAllLogs(newGreenhouse.Id);
            newGreenhouse.Plants = GetPlants(newGreenhouse.Id);
            greenhouses.Add(newGreenhouse);
        }

        return greenhouses;
    }

    public Greenhouse GetGreenhouse(int greenhouseId)
    {
        Greenhouse newGreenhouse = new Greenhouse();
        // Create the command  
        SqlCommand command = new SqlCommand("SELECT * FROM Greenhouses WHERE Id = @0", _connection);  
        // Add the parameters.  
        command.Parameters.Add(new SqlParameter("0", greenhouseId));

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            newGreenhouse = new Greenhouse( (int) reader[0], (string) reader[1], (string) reader[2], (string) reader[3], (float) reader[4]
                ,(ushort) reader[5],(float) reader[6], (float) reader[7],(float) reader[8]);
            newGreenhouse.Logs=GetAllLogs(newGreenhouse.Id);
            newGreenhouse.Plants = GetPlants(newGreenhouse.Id);
        }

        return newGreenhouse;
    }

    public void UpdateGreenhouse(Greenhouse greenhouse)
    {
        throw new NotImplementedException();
    }

    public void CreateGreenhouse(Greenhouse greenhouse)
    {
        // Create the command, to insert the data into the Table!  
        // this is a simple INSERT INTO command!  
  
        SqlCommand insertCommand = new SqlCommand("INSERT INTO TableName (FirstColumn, SecondColumn, ThirdColumn, ForthColumn) VALUES (@0, @1, @2, @3)", _connection);  
        
        insertCommand.Parameters.Add(new SqlParameter("0", 10));  
        insertCommand.Parameters.Add(new SqlParameter("1", "Test Column"));  
        insertCommand.Parameters.Add(new SqlParameter("2", DateTime.Now));  
        insertCommand.Parameters.Add(new SqlParameter("3", false)); 
        
        Console.WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery());
    }

    public void RemoveGreenhouse(int greenhouseId)
    {
        throw new NotImplementedException();
    }

    public List<Plant> GetPlants(int greenhouseId)
    {
        throw new NotImplementedException();
    }

    public void UpdatePlant(Plant plant)
    {
        throw new NotImplementedException();
    }

    public void RemovePlant(int plantId)
    {
        throw new NotImplementedException();
    }

    public void CreatePlant(Plant plant)
    {
        throw new NotImplementedException();
    }

    public List<Log> GetAllLogs(int greenhouseId)
    {
        throw new NotImplementedException();
    }

    public Log GetLastLog(int greenhouseId)
    {
        throw new NotImplementedException();
    }

    public Log GetLog(int logId)
    {
        throw new NotImplementedException();
    }

    public bool SetActuatorTrue(int greenhouseId)
    {
        throw new NotImplementedException();
    }
}