using System;

namespace DatabaseGateway.Model
{

    
    public class Log
    {
        public int Id { get; set; }

        public double Co2 { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public DateTime DateTime { get; set; }

        public int Id_Greenhouse { get; set; }

        public Log(int id, double co2, double temperature, double humidity, int idGreenhouse)
        {
            Id = id;
            Co2 = co2;
            Temperature = temperature;
            Humidity = humidity;
            DateTime = DateTime.Now;
            Id_Greenhouse = idGreenhouse;
        }
        
        public Log(int id, double co2, double temperature, double humidity, DateTime dateTime, int idGreenhouse)
        {
            Id = id;
            Co2 = co2;
            Temperature = temperature;
            Humidity = humidity;
            DateTime = dateTime;
            Id_Greenhouse = idGreenhouse;
        }

        public Log()
        {
            Id = -1;
            Co2 = -1;
            Temperature = -1;
            Humidity = -1;
            DateTime = DateTime.Now;
            Id_Greenhouse = -1;
        }
    }
}