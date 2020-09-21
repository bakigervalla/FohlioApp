using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class PdfImportSettingsPromptEventArgs : EventArgs
    {
        public PdfImportSettingsPromptEventArgs(IReadOnlyCollection<IReportContent> reports)
        {
            Reports = reports;
        }

        public IReadOnlyCollection<IReportContent> Reports { get; }

        public bool Cancel { get; set; }
    }
}