using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.Services
{
    public interface IPdfReportsSettingsDialog
    {
        bool Show(IReadOnlyCollection<IReportContent> pdfReports, IntPtr mainWindowHandle);
    }
}