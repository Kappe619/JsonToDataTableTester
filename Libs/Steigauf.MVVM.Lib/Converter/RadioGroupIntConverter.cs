using System;
using System.Globalization;
using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class RadioGroupIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value) == ((int)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemChecked = (bool)value;
            if (itemChecked) return (int)parameter;
            return 0;
        }
    }
}
