using System.Windows;
using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Konvertiert einen Booleanwert in eine Visibility-Eigenschaft
        /// </summary>
        /// <param name="value">Der Booleanwert anhand dem konvertiert werden soll</param>
        /// <param name="targetType">Wird nicht verwendet</param>
        /// <param name="parameter">Ist optional</param>
        /// <param name="culture">Wird nicht verwendet</param>
        /// <returns>
        /// Gibt Visibility.Visible zurück wenn value = true ist.
        /// Wenn value = false ist, dann:
        /// Visibility.Hidden wenn kein ConverterParameter angegeben wird,
        /// Visibility.Hidden wenn explizit ConverterParameter = HIDDEN angegeben wird,
        /// Visibility.Hidden wenn ein unbekannter Wert als ConverterParameter angegeben wird.
        /// Visibility.Collapsed wenn explizit ConverterParameter = COLLAPSED angegeben wird.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return Visibility.Visible;
            }
            else
            {
                if (parameter == null || !(((string)parameter).ToUpper()).Equals("COLLAPSED"))
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
