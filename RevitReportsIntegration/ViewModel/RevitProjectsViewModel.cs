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
        private readonly ObservableCollection<ProjectViewModel> projects = new ObservableCollection<ProjectViewModel>();

        public event EventHandler<ProjectViewModel> LunchMapp;

        public static RevitProjectsViewModel Instance = InstanceObj.Value;

        public IEnumerable<ProjectViewModel> Projects => projects;

        public event EventHandler RefreshProjectListRequested;

        public ProjectViewModel SelectedProject { get; set; }

        public ICommand NextCommand { get; }
        public ICommand SelectProjectCommand { get; }

        private RevitProjectsViewModel()
        {
            NextCommand = new RelayCommand(GoToMappPage);
            SelectProjectCommand = new RelayCommand<ProjectViewModel>((p) => SelectProjectAndNavigateToMappPage(p));
        }

        private void SelectProjectAndNavigateToMappPage(ProjectViewModel p)
        {
            LunchMapp?.Invoke(this, p);
        }

        private void GoToMappPage()
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
            {
                var viewModel = new ProjectViewModel(userProject);

                projects.Add(viewModel);
            }
        }


    }
}
