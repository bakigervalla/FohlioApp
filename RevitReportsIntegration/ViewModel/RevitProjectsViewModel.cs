using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class RevitProjectsViewModel : ViewModelBase
    {
        private static readonly Lazy<RevitProjectsViewModel> InstanceObj = new Lazy<RevitProjectsViewModel>(() => new RevitProjectsViewModel());
        private readonly ObservableCollection<Project> projects = new ObservableCollection<Project>();

        public event EventHandler<Project> LunchMapp;

        public static RevitProjectsViewModel Instance = InstanceObj.Value;

        public IEnumerable<Project> Projects => projects;

        public event EventHandler RefreshProjectListRequested;

        public Project SelectedProject { get; set; }

        public ICommand NextCommand { get; }
        public ICommand SelectProjectCommand { get; }

        private RevitProjectsViewModel()
        {
            NextCommand = new RelayCommand(GoToDevisionsPage);
            SelectProjectCommand = new RelayCommand<Project>((p) => GoToDivisionPage(p));
        }

        private void GoToDivisionPage(Project p)
        {
            LunchMapp?.Invoke(this, p);
        }

        private void GoToDevisionsPage()
        {
            LunchMapp?.Invoke(this, SelectedProject);
        }

        public void Initialize(MainBrowserViewModel mainBrowser)
        {
            mainBrowser.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(object sender, MainBrowserStateChangedEventArgs e)
        {
            if (e.NewState == BrowserState.RevitProjects)
            {
                Clear();

                RefreshProjectListRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Clear()
        {
            projects.Clear();
        }

        public void Initialize(IEnumerable<Project> userProjects)
        {
            foreach (var userProject in userProjects)
                projects.Add(userProject);
        }


    }
}
