namespace DatabaseGateway.Model;

public class Plant
{
    private int Id { get; set; }
    private string _name, _description, _type, _scientificName;

    private string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    private string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    private string Type
    {
        get => _type;
        set => _type = value ?? throw new ArgumentNullException(nameof(value));
    }

    private string ScientificName
    {
        get => _scientificName;
        set => _scientificName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Plant(int id, string name, string description, string type, string scientificName)
    {
        Id = id;
        _name = name;
        _description = description;
        _type = type;
        _scientificName = scientificName;
    }
    
    public Plant(string name, string description, string type, string scientificName)
    {
        _name = name;
        _description = description;
        _type = type;
        _scientificName = scientificName;
    }
}