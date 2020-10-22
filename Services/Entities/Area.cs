using System.Collections.Generic;

namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class Area
    {
        internal Area(int id, string name, int position, int layer_id, int project_id, bool is_used)
        {
            Id = id;

            Name = name;

            Position = position;

            LayerId = layer_id;

            ProjectId = project_id;

            IsUsed = is_used;
        }
        public int Id { get; }

        public string Name { get; }

        public int Position { get; }

        public int LayerId { get; }

        public int ProjectId { get; }

        public bool IsUsed { get; set; }

        public string Description => $"{Position} {Name}";
    }
}