using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm.Converters
{
    public class InversedBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;

            var flag = (bool) value;

            return !flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}