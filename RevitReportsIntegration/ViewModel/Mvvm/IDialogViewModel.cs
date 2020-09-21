using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public interface IDialogViewModel
    {
        ICommand OkCommand { get; }

        ICommand CancelCommand { get; }

        event EventHandler<DialogCloseEventArgs> RequestClose;
    }
}