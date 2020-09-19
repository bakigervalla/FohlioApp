using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class ProjectsBrowserViewModel : ViewModelBase
    {
        private static readonly Lazy<ProjectsBrowserViewModel> InstanceObj = new Lazy<ProjectsBrowserViewModel>(() => new ProjectsBrowserViewModel());
        private readonly ObservableCollection<ProjectViewModel> projects = new ObservableCollection<ProjectViewModel>();

        private ProjectsBrowserViewModel()
        {
            RefreshProjectListCommand = new RelayCommand(() => RefreshProjectListRequested?.Invoke(this, EventArgs.Empty));
        }

        public static ProjectsBrowserViewModel Instance => InstanceObj.Value;

        public IEnumerable<ProjectViewModel> Projects => projects;

        public event EventHandler RefreshProjectListRequested;

        public event EventHandler<SelectProjectEventArgs> SelectProject;

        public ICommand RefreshProjectListCommand { get; }

        public void Initialize(IEnumerable<Project> userProjects)
        {
            Clear();

            foreach (var userProject in userProjects)
            {
                var viewModel = new ProjectViewModel(userProject);

                viewModel.SelectProject += OnSelectProject;

                projects.Add(viewModel);
            }
        }

        private void OnStateChanged(object sender, MainBrowserStateChangedEventArgs e)
        {
            if (e.NewState == BrowserState.ProjectsList)
            {
                Clear();

                RefreshProjectListRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnSelectProject(object sender, SelectProjectEventArgs e) => SelectProject?.Invoke(this, e);

        private void Clear()
        {
            foreach (var project in projects)
                project.SelectProject -= OnSelectProject;

            projects.Clear();
        }
    }
}