using System;
using System.Windows.Input;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public class RelayCommand : ICommand
    {
        private readonly WeakAction execute;
        private readonly WeakFunc<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute == null)
                    return;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute == null)
                    return;
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this.execute = new WeakAction(execute);
            if (canExecute == null)
                return;
            this.canExecute = new WeakFunc<bool>(canExecute);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            if (!canExecute.IsStatic && !canExecute.IsAlive)
                return false;
            return canExecute.Execute();
        }

        public virtual void Execute(object parameter)
        {
            if (!CanExecute(parameter) || execute == null || !execute.IsStatic && !execute.IsAlive)
                return;
            execute.Execute();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly WeakAction<T> execute;
        private readonly WeakFunc<T, bool> canExecute;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            this.execute = new WeakAction<T>(execute);
            if (canExecute == null)
                return;
            this.canExecute = new WeakFunc<T, bool>(canExecute);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute == null)
                    return;

                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (canExecute == null)
                    return;

                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
                return true;
            if (!canExecute.IsStatic && !canExecute.IsAlive)
                return false;
            if (parameter == null && typeof(T).IsValueType)
                return canExecute.Execute(default(T));
            return canExecute.Execute((T)parameter);
        }

        public virtual void Execute(object parameter)
        {
            var parameter1 = parameter;

            if (parameter != null && parameter.GetType() != typeof(T) && parameter is IConvertible)
                parameter1 = Convert.ChangeType(parameter, typeof(T), null);

            if (!CanExecute(parameter1) || execute == null || !execute.IsStatic && !execute.IsAlive)
                return;

            if (parameter1 == null)
            {
                if (typeof(T).IsValueType)
                    execute.Execute(default(T));
            }
            else
                execute.Execute((T)parameter1);
        }
    }
}