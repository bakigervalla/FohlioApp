using System.Collections.Generic;

namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class Parameter
    {
        internal Parameter(int id, string name, string code, string key, dynamic children)
        {
            Id = id;

            Name = name;

            Code = code;

            Key = key;

            Children = children;
        }
        public int Id { get; }

        public string Name { get; }

        public string Code { get; }

        public string Key { get; }

        public dynamic Children { get; }

        public string Description => $"{Code} {Name}";
    }
}