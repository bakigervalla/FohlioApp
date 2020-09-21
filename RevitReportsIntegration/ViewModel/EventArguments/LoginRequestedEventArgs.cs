using System;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class LoginRequestedEventArgs : EventArgs
    {
        public LoginRequestedEventArgs(string userName, string password)
        {
            UserName = userName;

            Password = password;
        }

        public string UserName { get; }

        public string Password { get; }
    }
}