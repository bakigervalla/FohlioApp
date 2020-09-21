using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using Fohlio.RevitReportsIntegration.Utils;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm.Converters
{
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bmp = value as Bitmap;
            if (bmp == null)
            {
                var defaultBmp = parameter as Bitmap;
                if (defaultBmp != null)
                    return BitmapSourceConverter.ConvertFromImage(defaultBmp);
            }

            return bmp == null ? null : BitmapSourceConverter.ConvertFromImage(bmp);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}