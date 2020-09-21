using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.Properties;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class CurrentActionBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<CurrentActionBrowserViewModel> InstanceObj = new Lazy<CurrentActionBrowserViewModel>(() => new CurrentActionBrowserViewModel());
        private string actionTitle;

        private CurrentActionBrowserViewModel()
        {
            SetState(BrowserState.Login);

            LoginCommand = new RelayCommand(Login);
        }

        public static CurrentActionBrowserViewModel Instance => InstanceObj.Value;

        public string ActionTitle
        {
            get { return actionTitle; }
            private set
            {
                actionTitle = value;

                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public void SetState(BrowserState state) => ActionTitle = GetActionTitile(state);

        private void Login()
        {
            // Autodesk.Revit.UI.TaskDialog.Show("dev", "This feature is under development");
        }

        private static string GetActionTitile(BrowserState state)
        {
            switch (state)
            {
                case BrowserState.Login:
                    return Resources.LoginActionPrompt;

                case BrowserState.ProjectsList:
                    return Resources.ProjectListActionPrompt;

                case BrowserState.ReportsList:
                    return Resources.ReportsListActionPrompt;

                default:
                    return string.Empty;
            }
        }
    }
}