using System;
using System.Windows.Controls;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class LoginBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<LoginBrowserViewModel> InstanceObj = new Lazy<LoginBrowserViewModel>(() => new LoginBrowserViewModel());
        private string userName;

        private LoginBrowserViewModel()
        {
#if DEBUG
            LoginCommand = new RelayCommand<object>(LoginDebug);
#else
            LoginCommand = new RelayCommand<object>(Login);

#endif
        }

        public static LoginBrowserViewModel Instance = InstanceObj.Value;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;

                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public event EventHandler<LoginRequestedEventArgs> LoginRequested;

        private void Login(object pwdControl)
        {
            if (string.IsNullOrWhiteSpace(UserName))
                return;

            var passwordBox = (PasswordBox)pwdControl;

            var password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(password))
                return;

            passwordBox.Password = string.Empty;

            LoginRequested?.Invoke(this, new LoginRequestedEventArgs(UserName, password));
        }

        private void LoginDebug(object pwdControl)
        {
            LoginRequested?.Invoke(this, new LoginRequestedEventArgs("bakigervalla@gmail.com.beta", "orion857"));
        }
    }
}