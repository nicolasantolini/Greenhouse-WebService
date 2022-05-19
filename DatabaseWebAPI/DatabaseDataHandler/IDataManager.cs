using DatabaseGateway.Model;

namespace DatabaseGateway.DatabaseDataHandler;

public interface IDataManager
{
    List<Greenhouse> GetGreenhouses(string userEmail);
    Greenhouse GetGreenhouse(int greenhouseId);
    void UpdateGreenhouse(Greenhouse greenhouse);
    void CreateGreenhouse(Greenhouse greenhouse);
    void RemoveGreenhouse(int greenhouseId);
    List<Plant> GetPlants(int greenhouseId);
    void UpdatePlant(Plant plant);
    void RemovePlant(int plantId);
    void CreatePlant(Plant plant);
    List<Log> GetAllLogs(int greenhouseId);
    Log GetLastLog(int greenhouseId);
    Log GetLog(int logId);
    bool SetActuatorTrue(int greenhouseId);

}