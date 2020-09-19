namespace Fohlio.RevitReportsIntegration.Entities
{
    public interface IFont
    {
        string FontName { get; }

        double Size { get; }

        bool IsBold { get; }

        bool IsItalic { get; }

        bool IsUnderLine { get; }

        double GetFontSizeInMm();

        string GetDesiredName();
    }
}