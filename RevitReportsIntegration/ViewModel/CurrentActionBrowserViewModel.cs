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
        private string descriptionText;

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

        public string DescriptionText
        {
            get { return descriptionText; }
            private set
            {
                descriptionText = value;

                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public void SetState(BrowserState state)
        {
            var action = GetActionTitile(state);
            ActionTitle = action.Item1;
            DescriptionText = action.Item2;
        }

        private void Login()
        {
            // Autodesk.Revit.UI.TaskDialog.Show("dev", "This feature is under development");
        }

        private static (string, string) GetActionTitile(BrowserState state)
        {
            switch (state)
            {
                case BrowserState.Login:
                    return (Resources.LoginActionPrompt, null);
                
                case BrowserState.ProjectsList:
                    return (Resources.ProjectListActionPrompt, string.Empty);

                case BrowserState.ReportsList:
                    return (Resources.ReportsListActionPrompt, string.Empty);

                case BrowserState.Tasks:
                    return (Resources.task_ChooseTask, string.Empty);

                case BrowserState.RevitProjects:
                    return (Resources.ProjectListActionPrompt, string.Empty);

                case BrowserState.Categories:
                    return (Resources.cats_Step1, Resources.cats_Step1Desc);

                case BrowserState.Families:
                    return (Resources.cats_Step2, Resources.cats_Step2Desc);

                case BrowserState.Parameters:
                    return (Resources.cats_Step3, Resources.cats_Step3Desc);

                default:
                    return (string.Empty, string.Empty);
            }
        }
    }
}