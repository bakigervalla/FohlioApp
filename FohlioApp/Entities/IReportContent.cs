using System.IO;

namespace Fohlio.RevitReportsIntegration.Entities
{
    public interface IReportContent
    {
        IReport Report { get; } 

        Stream Content { get; }

        int Page { get; set; }

        double Resolution { get; set; }

        int GetTotalPagesCount();
    }
}