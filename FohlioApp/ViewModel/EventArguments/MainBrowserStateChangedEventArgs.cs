using System;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class MainBrowserStateChangedEventArgs : EventArgs
    {
        public MainBrowserStateChangedEventArgs(BrowserState newState)
        {
            NewState = newState;
        }

        public BrowserState NewState { get; }
    }
}