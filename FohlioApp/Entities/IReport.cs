using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Entities
{
    public interface IReport
    {
        int Id { get; }

        ReportKind Kind { get; }

        string Url { get; }

        string Name { get; }
    }
}