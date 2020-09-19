using System;

namespace Fohlio.RevitReportsIntegration.Services.Entities
{
    public class Report
    {
        internal Report(int id, ReportKind kind, string url, DateTime createdAt)
        {
            Id = id;

            Kind = kind;

            Url = url;

            CreatedAt = createdAt;
        }

        public int Id { get; }

        public ReportKind Kind { get; } 

        public string Url { get; }

        public DateTime CreatedAt { get; }
    }
}