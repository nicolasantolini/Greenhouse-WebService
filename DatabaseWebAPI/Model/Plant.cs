using System;

namespace DatabaseGateway.Model
{
[Serializable]
    public class Plant
    {
        public int Id { get; set; }
        private string _name, _description, _type, _scientificName;
        private int _idGreenhouse;

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

        public string Type
        {
            get => _type;
            set => _type = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string ScientificName
        {
            get => _scientificName;
            set => _scientificName = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Id_Greenhouse
        {
            get;
            set;
        }

        public Plant(int id, string name, string description, string type, string scientificName, int idGreenhouse)
        {
            Id = id;
            _name = name;
            _description = description;
            _type = type;
            _scientificName = scientificName;
            Id_Greenhouse = idGreenhouse;
        }

        public Plant(string name, string description, string type, string scientificName, int idGreenhouse)
        {
            Id = -1;
            _name = name;
            _description = description;
            _type = type;
            _scientificName = scientificName;
            Id_Greenhouse = idGreenhouse;
        }

        public Plant()
        {
            Id = -1;
            _name = "";
            _description = "";
            _type = "";
            _scientificName = "";
            Id_Greenhouse = -1;
        }
    }
}