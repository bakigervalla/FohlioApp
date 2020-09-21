using Fohlio.RevitReportsIntegration.Services.Entities;

namespace Fohlio.RevitReportsIntegration.ViewModel.EventArguments
{
    public class SelectProjectEventArgs
    {
        public SelectProjectEventArgs(Project project)
        {
            Project = project;
        }

        public Project Project { get; }
    }
}