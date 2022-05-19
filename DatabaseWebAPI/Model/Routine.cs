namespace DatabaseGateway.Model;

public class Routine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Day{ get; set; }  
    public string Frequency{ get; set; }
    public int PlantId { get; set; }

    public Routine(int id, string name, string day, string frequency, int plantId)
    {
        Id = id;
        Name = name;
        Day = day;
        Frequency = frequency;
        PlantId = plantId;
    }
}