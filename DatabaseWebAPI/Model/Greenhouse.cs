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

    public double Area { get; set; }

    public double Co2Preferred { get; set; }

    public double TemperaturePreferred { get; set; }

    public double HumidityPreferred { get; set; }

    private string _name;
    private string _description;
    private string _location;
    
    public int ActuatorSet { get; set; }

    public List<Log> Logs;
    public List<Plant> Plants;

    public Greenhouse(int id, string name, string description, string location, double area,int actuatorSet, double co2Preferred,
        double temperaturePreferred, double humidityPreferred)
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
    public Greenhouse(string name, string description, string location, double area, double co2Preferred,
        double temperaturePreferred, double humidityPreferred)
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