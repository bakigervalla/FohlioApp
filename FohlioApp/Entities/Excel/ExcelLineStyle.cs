using System.Globalization;
using System.Linq;
using OfficeOpenXml.Style;
using System;

namespace Fohlio.RevitReportsIntegration.Entities.Excel
{
    public class ExcelLineStyle
    {
        private readonly ExcelBorderStyle borderStyle;

        private ExcelLineStyle(string name, int weight, ExcelBorderStyle borderStyle)
        {
            this.borderStyle = borderStyle;
            Name = name;
            Weight = weight;
        }

        public static ExcelLineStyle Create(ExcelBorderStyle borderStyle)
        {
            if (borderStyle == ExcelBorderStyle.None)
                throw new InvalidOperationException("Border style cannot be None");

            var name = GetLineStyleName(borderStyle);

            var weight = GetWeight(borderStyle);

            return new ExcelLineStyle(name, weight, borderStyle);
        }

        public string Name { get; }

        public int Weight { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((ExcelLineStyle) obj);
        }

        public override int GetHashCode() => (int) borderStyle;

        protected bool Equals(ExcelLineStyle other) => borderStyle == other.borderStyle;

        private static string GetLineStyleName(ExcelBorderStyle borderStyle)
        {
            var name = string.Concat(borderStyle.ToString().Select((x, i) => ConvertCharacter(x, i == 0)));

            return $"Fohlio Report border {name}";
        }

        private static int GetWeight(ExcelBorderStyle borderStyle)
        {
            switch (borderStyle)
            {
                case ExcelBorderStyle.Medium:
                case ExcelBorderStyle.MediumDashDot:
                case ExcelBorderStyle.MediumDashDotDot:
                case ExcelBorderStyle.MediumDashed:
                case ExcelBorderStyle.Double:
                    return 2;

                case ExcelBorderStyle.Thick:
                    return 4;

                default:
                    return 1;
            }
        }
        
        private static string ConvertCharacter(char x, bool isFirst)
        {
            return !isFirst && char.IsUpper(x)
                ? $" {x}"
                : x.ToString(CultureInfo.InvariantCulture);
        }
    }
}