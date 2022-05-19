using System;

namespace DatabaseGateway.Model
{

    public class Routine
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Task { get; set; }
        public int Id_Plant { get; set; }

        public Routine(int id, string day, string task, int id_plant)
        {
            Id = id;
            Day = day;
            Task = task;
            Id_Plant = id_plant;
        }
        
        public Routine()
        {
            Id = -1;
            Day = "noDay";
            Task = "noTask";
            Id_Plant = -1;
        }
    }
}