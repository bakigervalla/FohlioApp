using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class LaunchReportsImportRequestedEventArgs
    {
        public LaunchReportsImportRequestedEventArgs(IEnumerable<IReportContent> reports, object titleBlockId)
        {
            Reports = reports;

            TitleBlockId = titleBlockId;
        }

        public IEnumerable<IReportContent> Reports { get; }

        public object TitleBlockId { get; }
    }
}