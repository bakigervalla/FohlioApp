using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.EventArguments;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class ProjectViewModel
    {
        private readonly Project project;

        public ProjectViewModel(Project project)
        {
            this.project = project;

            Name = project.Name;

            SelectProjectCommand = new RelayCommand(RequestSelectProject);
        }

        public string Name { get; }

        public ICommand SelectProjectCommand { get; }

        public event EventHandler<SelectProjectEventArgs> SelectProject;

        private void RequestSelectProject() => SelectProject?.Invoke(this, new SelectProjectEventArgs(project));
    }
}