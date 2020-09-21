namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public interface IExecuteWithObject
    {
        object Target { get; }

        void ExecuteWithObject(object parameter);

        void MarkForDeletion();
    }
}