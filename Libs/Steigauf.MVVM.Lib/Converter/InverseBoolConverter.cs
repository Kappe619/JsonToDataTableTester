using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            // I added this because I kept getting DependecyProperty.UnsetValue  
            // Passed in as the program initializes 


            if (value.GetType() != typeof(bool))
            {
                throw new ArgumentException("Value is not a boolean.");
            }

            return !(bool)value;

        }

        public object ConvertBack(object value,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            if (value.GetType() != typeof(bool))
            {
                throw new ArgumentException("Value is not a boolean.");
            }

            return !(bool)value;
        }
    }


}
