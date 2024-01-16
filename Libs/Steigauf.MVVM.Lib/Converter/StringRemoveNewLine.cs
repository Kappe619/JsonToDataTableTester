using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class StringRemoveNewLine : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value)) 
                return string.Empty;
            String sValue = (string)value;
            if(sValue.Contains("\r\n"))
                value = sValue.Replace("\r\n", " ");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            return value;
        }
    }
}
