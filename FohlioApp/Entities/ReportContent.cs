using System.IO;
using System.Text.RegularExpressions;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.Entities
{
    public class ReportContent : IReportContent
    {
        private int? totalPagesCount;

        public ReportContent(IReport report, Stream content)
        {
            Report = report;

            Content = content;
        }

        public IReport Report { get; }

        public Stream Content { get; }

        public int Page { get; set; } = 1;

        public double Resolution { get; set; } = 150;

        public int GetTotalPagesCount()
        {
            if (Report.Kind != ReportKind.Pdf)
                return 1;

            if (totalPagesCount.HasValue)
                return totalPagesCount.Value;

            using (var stream = new MemoryStream())
            {
                Content.CopyTo(stream);

                Content.Position = 0;

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    var regex = new Regex(@"/Type\s*/Page[^s]");

                    var matches = regex.Matches(reader.ReadToEnd());

                    Content.Position = 0;

                    totalPagesCount = matches.Count;

                    return matches.Count;
                }
            }
        }
    }
}