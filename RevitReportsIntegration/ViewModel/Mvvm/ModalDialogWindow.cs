using System;
using System.Windows;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public class ModalDialogWindow : Window, IModalDialogWindow
    {
        public ModalDialogWindow()
        {
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dialogViewModel = e.NewValue as IDialogViewModel;

            if (dialogViewModel != null)
                dialogViewModel.RequestClose += OnRequestClose;
        }

        private void OnRequestClose(object sender, DialogCloseEventArgs e)
        {
            try
            {
                DialogResult = e.Result;
            }
            catch (InvalidOperationException)
            {
                Close();
            }
        }
    }
}