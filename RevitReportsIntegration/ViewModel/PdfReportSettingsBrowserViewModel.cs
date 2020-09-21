using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class PdfReportSettingsBrowserViewModel : DialogViewModelBase
    {
        private static readonly Lazy<PdfReportSettingsBrowserViewModel> InstanceObj = new Lazy<PdfReportSettingsBrowserViewModel>(() => new PdfReportSettingsBrowserViewModel());
        private readonly ObservableCollection<PdfReportSettingsViewModel> pdfReportSettings = new ObservableCollection<PdfReportSettingsViewModel>();

        private PdfReportSettingsBrowserViewModel()
        {
            
        }

        public static PdfReportSettingsBrowserViewModel Instance => InstanceObj.Value;

        public IEnumerable<PdfReportSettingsViewModel> PdfReportSettings => pdfReportSettings;

        public void Initialize(IEnumerable<IReportContent> reports)
        {
            pdfReportSettings.Clear();

            foreach (var report in reports)
                pdfReportSettings.Add(new PdfReportSettingsViewModel(report));
        }

        protected override bool CanOkCommandExecute() => PdfReportSettings.All(x => x.IsValid());

        protected override void OkCommandAction()
        {
            foreach (var report in pdfReportSettings)
                report.Apply();

            base.OkCommandAction();
        }
    }
}