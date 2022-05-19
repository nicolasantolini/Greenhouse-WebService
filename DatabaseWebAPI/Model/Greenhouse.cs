namespace DatabaseGateway.Model;

public class Greenhouse
{

    public int Id
    {
        get => Id;
        set => Id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Location
    {
        get => _location;
        set => _location = value ?? throw new ArgumentNullException(nameof(value));
    }

    public float Area { get; set; }

    public float Co2Preferred { get; set; }

    public float TemperaturePreferred { get; set; }

    public float HumidityPreferred { get; set; }

    private string _name;
    private string _description;
    private string _location;
    
    public ushort ActuatorSet { get; set; }

    public List<Log> Logs;
    public List<Plant> Plants;

    public Greenhouse(int id, string name, string description, string location, float area,ushort actuatorSet, float co2Preferred,
        float temperaturePreferred, float humidityPreferred)
    {
        Id = id;
        _name = name;
        _description = description;
        _location = location;
        Co2Preferred = co2Preferred;
        TemperaturePreferred = temperaturePreferred;
        HumidityPreferred = humidityPreferred;
        Area = area;
        ActuatorSet = actuatorSet;
        Logs = new List<Log>();
        Plants = new List<Plant>();
    }
    public Greenhouse(string name, string description, string location, float area, float co2Preferred,
        float temperaturePreferred, float humidityPreferred)
    {
        _name = name;
        _description = description;
        _location = location;
        Co2Preferred = co2Preferred;
        TemperaturePreferred = temperaturePreferred;
        HumidityPreferred = humidityPreferred;
        Area = area;
        ActuatorSet = 0;
        Logs = new List<Log>();
        Plants = new List<Plant>();
    }

    public Greenhouse()
    {
        _name = "";
        _description = "";
        _location = "";
        Co2Preferred = 0;
        TemperaturePreferred = 0;
        HumidityPreferred = 0;
        Area = 0;
        ActuatorSet = 0;
        Logs = new List<Log>();
        Plants = new List<Plant>();
    }

}