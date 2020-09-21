using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
                return DependencyProperty.UnsetValue;

            return parameter.Equals(value)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}