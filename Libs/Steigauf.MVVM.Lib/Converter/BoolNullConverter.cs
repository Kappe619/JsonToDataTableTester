using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class BoolNullConverter : IValueConverter
    {
        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              System.Globalization.CultureInfo culture)
        {
            // I added this because I kept getting DependecyProperty.UnsetValue  
            // Passed in as the program initializes 


            if (value == null)
            {
                return false;
            }
            if ((bool)value)
                return true;
            return false;
        }

        public object ConvertBack(object value,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            return (bool)value;
        }
    }


}
