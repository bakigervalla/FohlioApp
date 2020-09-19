namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class Project
    {
        internal Project(int id, string name, bool singleSchedule)
        {
            Id = id;

            Name = name;

            SingleSchedule = singleSchedule;
        }
        public int Id { get; }

        public string Name { get; }

        public bool SingleSchedule { get; }
    }
}