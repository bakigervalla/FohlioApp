using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class AccountBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<AccountBrowserViewModel> InstanceObj = new Lazy<AccountBrowserViewModel>(() => new AccountBrowserViewModel());
        private string name;
        private bool isAuthentificated;

        private AccountBrowserViewModel()
        {
            LogoutCommand = new RelayCommand(Logout);
        }

        public static AccountBrowserViewModel Instance => InstanceObj.Value;

        public string Name
        {
            get { return name; }
            private set
            {
                name = value;

                OnPropertyChanged();
            }
        }

        public bool IsAuthentificated
        {
            get { return isAuthentificated; }
            private set
            {
                isAuthentificated = value;

                OnPropertyChanged();
            }
        }

        public ICommand LogoutCommand { get; }

        public event EventHandler LogoutRequested;

        public void Authentificate(string userName)
        {
            Name = userName;

            IsAuthentificated = true;
        }

        private void Logout()
        {
            IsAuthentificated = false;

            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}