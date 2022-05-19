namespace DatabaseGateway.Model;

public class Log
{
    public int Id { get; set; }

    public double Co2 { get; set; }

    public double Temperature { get; set; }

    public double Humidity { get; set; }

    public DateTime DateTime { get; set; }

    public Log(int id,double co2, double temperature, double humidity,DateTime dateTime)
    {
        Id = id;
        Co2 = co2;
        Temperature = temperature;
        Humidity = humidity;
        DateTime=dateTime;
    }

    public Log(double co2, double temperature, double humidity)
    {
        Co2 = co2;
        Temperature = temperature;
        Humidity = humidity;
        DateTime=DateTime.Now;
    }
    
    
}