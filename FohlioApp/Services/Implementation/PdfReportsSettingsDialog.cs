using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;
using Fohlio.RevitReportsIntegration.Utils;
using Fohlio.RevitReportsIntegration.ViewModel;
using Fohlio.RevitReportsIntegration.Views;

namespace Fohlio.RevitReportsIntegration.Services.Implementation
{
    public class PdfReportsSettingsDialog : IPdfReportsSettingsDialog
    {
        public bool Show(IReadOnlyCollection<IReportContent> pdfReports, IntPtr mainWindowHandle)
        {
            var browser = PdfReportSettingsBrowserViewModel.Instance;

            browser.Initialize(pdfReports);

            var window = new PdfSettingsDialogWindow();

            WindowOwnerUtil.BindWithMainWindow(window, mainWindowHandle);

            return window.ShowDialog().GetValueOrDefault();
        }
    }
}