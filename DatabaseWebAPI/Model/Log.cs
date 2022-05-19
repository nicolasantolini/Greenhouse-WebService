namespace DatabaseGateway.Model;

public class Log
{
    public int Id { get; set; }

    public float Co2 { get; set; }

    public float Temperature { get; set; }

    public float Humidity { get; set; }

    public DateTime DateTime { get; set; }

    public Log(int id,float co2, float temperature, float humidity)
    {
        Id = id;
        Co2 = co2;
        Temperature = temperature;
        Humidity = humidity;
        DateTime=DateTime.Now;
    }

    public Log(float co2, float temperature, float humidity)
    {
        Co2 = co2;
        Temperature = temperature;
        Humidity = humidity;
        DateTime=DateTime.Now;
    }
    
    
}