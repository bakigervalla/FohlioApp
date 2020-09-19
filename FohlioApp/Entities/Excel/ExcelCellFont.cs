using System.Globalization;
using Fohlio.RevitReportsIntegration.Utils.Extensions;
using OfficeOpenXml.Style;

namespace Fohlio.RevitReportsIntegration.Entities.Excel
{
    public class ExcelCellFont : IFont
    {
        public ExcelCellFont(ExcelFont font)
        {
            FontName = font.Name;

            Size = font.Size;

            IsBold = font.Bold;

            IsItalic = font.Italic;

            IsUnderLine = font.UnderLine;
        }

        public string FontName { get; }

        public double Size { get; }

        public bool IsBold { get; }

        public bool IsItalic { get; }

        public bool IsUnderLine { get; }

        public double GetFontSizeInMm()
        {
            const double factor = 25.4/96.0;
            const double delta = 2.5;

            return (Size - delta) * factor;
        }

        public string GetDesiredName()
        {
            var name = $"Fohlio reports {GetFontSizeInMm().ToString("0.0", CultureInfo.InvariantCulture)} mm {FontName}";

            if (IsPlain())
                return name;

            return $"{name} {(IsBold ? "b" : string.Empty)}{(IsItalic ? "i" : string.Empty)}{(IsUnderLine ? "u" : string.Empty)}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((ExcelCellFont) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FontName.GetHashCode();
                hashCode = (hashCode*397) ^ IsBold.GetHashCode();
                hashCode = (hashCode*397) ^ IsItalic.GetHashCode();
                hashCode = (hashCode*397) ^ IsUnderLine.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(ExcelCellFont other)
        {
            return string.Equals(FontName, other.FontName) 
                && IsBold == other.IsBold 
                && IsItalic == other.IsItalic 
                && IsUnderLine == other.IsUnderLine
                && Size.IsAlmostEqualTo(other.Size);
        }

        private bool IsPlain() => !(IsBold || IsItalic || IsUnderLine);
    }
}