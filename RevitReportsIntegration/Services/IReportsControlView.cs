namespace Fohlio.RevitReportsIntegration.Services
{
    public interface IReportsControlView
    {
        void Initialize(object application);

        void Show(object application);

        void Hide(object application);

        void RefreshTitleBlocks(object document);
    }
}