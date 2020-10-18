using System.Collections.Generic;

namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class Column
    {
        internal Column(int id, string key, string name, string type, int position)
        {
            Id = id;

            Key = key;

            Name = name;

            Type = type;

            Position = position;
        }
        public int Id { get; }

        public string Key { get; }

        public string Name { get; }

        public string Type { get; }

        public dynamic Position { get; }

        public string Description => $"{Key} {Name}";
    }
}