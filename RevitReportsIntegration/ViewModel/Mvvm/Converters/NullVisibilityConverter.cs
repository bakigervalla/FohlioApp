using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Fohlio.RevitReportsIntegration.ViewModel.Mvvm.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString() != string.Empty)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
