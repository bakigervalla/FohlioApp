using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm.Converters
{
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
                return false;

            return parameter.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;

            return (bool)value
                ? parameter
                : DependencyProperty.UnsetValue;
        }
    }
}