using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProjectManager.WPF.Converters
{
    public class ObjectToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool.TryParse((string)parameter, out bool invertedConversion);

            return (value == null) == !invertedConversion ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
