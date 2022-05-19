using DatabaseGateway.Model;

namespace DatabaseGateway.DatabaseDataHandler;

public interface IDataManager
{
    Task<List<Greenhouse>> GetGreenhouses(string userEmail);
    Task<Greenhouse> GetGreenhouse(int greenhouseId);
    Task UpdateGreenhouse(Greenhouse greenhouse);
    Task CreateGreenhouse(Greenhouse greenhouse);
    Task RemoveGreenhouse(int greenhouseId);
    Task<List<Plant>> GetPlants(int greenhouseId);
    Task UpdatePlant(Plant plant);
    Task RemovePlant(int plantId);
    Task CreatePlant(Plant plant);
    Task<List<Log>> GetAllLogs(int greenhouseId);
    Task<Log> GetLastLog(int greenhouseId);
    Task<Log> GetLog(int logId);

    Task<List<Routine>> GetAllRoutines(int plantId);
    
    Task<bool> SetActuatorTrue(int greenhouseId);

}