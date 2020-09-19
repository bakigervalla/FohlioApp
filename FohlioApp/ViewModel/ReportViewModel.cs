using System.IO;
using System.Linq;
using Fohlio.RevitReportsIntegration.Entities;
using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class ReportViewModel : ViewModelBase, IReport
    {
        private bool isChecked;

        public ReportViewModel(Report report)
        {
            Id = report.Id;

            Kind = report.Kind;

            Url = report.Url;

            Name = Path.GetFileName(report.Url.Split('/').Last());
        }

        public int Id { get; }

        public ReportKind Kind { get; }

        public string Url { get; }

        public string Name { get; }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;

                OnPropertyChanged();
            }
        }
    }
}