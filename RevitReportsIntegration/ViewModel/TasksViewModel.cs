using System;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.Services.Entities;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {

        private static readonly Lazy<TasksViewModel> InstanceObj = new Lazy<TasksViewModel>(() => new TasksViewModel());
        public event EventHandler<Project> GoToModule;

        public static TasksViewModel Instance = InstanceObj.Value;

        private Project Project { get; set; }

        public ICommand SwitchCommand { get; }
        public ICommand NextCommand { get; }
            
        private Module _moduleType;
        public Module ModuleType { get => _moduleType; set { _moduleType = value; OnPropertyChanged(nameof(ModuleType)); } }

        private TasksViewModel()
        {
            SwitchCommand = new RelayCommand<int>( (p) => ModuleType = (Module)p);
            NextCommand = new RelayCommand(Navigate);
        }

        private void Navigate()
        {
            if (ModuleType != Module.None)
                GoToModule?.Invoke(ModuleType, Project);
        }

        public void Initialize(Project project)
        {
            Project = project;
        }
    }


}