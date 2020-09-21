using System;
using System.Collections.Generic;
using Fohlio.RevitReportsIntegration.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class ImportReportsEventArgs : EventArgs
    {
        public ImportReportsEventArgs(IEnumerable<object> reports, object titleBlockId)
        {
           //  Reports = reports;

            TitleBlockId = titleBlockId;
        }

        public IEnumerable<object> Reports { get; }

        public object TitleBlockId { get; }
    }
}