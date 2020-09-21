using System;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class DialogCloseEventArgs : EventArgs
    {
        public DialogCloseEventArgs(bool? result)
        {
            Result = result;
        }

        public bool? Result { get; }
    }
}