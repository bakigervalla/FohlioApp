using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Fohlio.RevitReportsIntegration.ViewModel.Mvvm;

namespace Fohlio.RevitReportsIntegration.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {

        private static readonly Lazy<TasksViewModel> InstanceObj = new Lazy<TasksViewModel>(() => new TasksViewModel());
        public event EventHandler<EventArgs> LunchTasks;
        
        public ICommand SwitchCommand { get; }

        private TasksViewModel()
        {
            SwitchCommand = new RelayCommand<string>((p) => LunchTasks?.Invoke(p, EventArgs.Empty));
        }

        public static TasksViewModel Instance = InstanceObj.Value;

    }
}