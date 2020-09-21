using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class PdfReportSettingsViewModel : ViewModelBase
    {
        private int page = 1;
        private double resolution = 150;

        public PdfReportSettingsViewModel(IReportContent reportContent)
        {
            ReportContent = reportContent;
        }

        public IReportContent ReportContent { get; }

        public string Name => "Baki"; // ReportContent.Report.Name;

        public int Page
        {
            get { return page; }
            set
            {
                page = value;

                OnPropertyChanged();
            }
        }

        public int TotalPages => ReportContent.GetTotalPagesCount();

        public double Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;

                OnPropertyChanged();
            }
        }

        public IEnumerable<double> AvailableResolutions => new[] {72.0, 150, 300, 600};

        public bool IsValid() => Page > 0 && Page <= TotalPages;

        public void Apply()
        {
            ReportContent.Page = Page;

            ReportContent.Resolution = Resolution;
        }
    }
}