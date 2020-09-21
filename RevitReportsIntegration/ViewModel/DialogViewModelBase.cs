using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public abstract class DialogViewModelBase : ViewModelBase, IDialogViewModel
    {
        protected DialogViewModelBase()
        {
            OkCommand = new RelayCommand(OkCommandAction, CanOkCommandExecute);

            CancelCommand = new RelayCommand(CancelCommandAction, CanExecuteCancelCommand);
        }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        public event EventHandler<DialogCloseEventArgs> RequestClose;

        protected virtual bool CanOkCommandExecute() => true;

        protected virtual void OkCommandAction() => Close(true);

        protected virtual bool CanExecuteCancelCommand() => true;

        protected virtual void CancelCommandAction() => Close(false);

        protected void Close(bool? result) => RequestClose?.Invoke(this, new DialogCloseEventArgs(result));
    }
}