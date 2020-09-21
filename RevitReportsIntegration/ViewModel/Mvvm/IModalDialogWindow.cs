namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm
{
    public interface IModalDialogWindow
    {
        object DataContext { get; set; }

        bool? DialogResult { get; set; }
        
        bool? ShowDialog();

        void Close();
    }
}