using System;
using System.Windows.Data;

namespace Steigauf.MVVM.Converter
{
    public class BoolStringConverter : IValueConverter
        {
            public object Convert(object  value,
                                  Type targetType,
                                  object parameter,
                                  System.Globalization.CultureInfo culture)
            {
                // I added this because I kept getting DependecyProperty.UnsetValue  
                // Passed in as the program initializes 
                var bNot = parameter as string != null && (String)parameter == "NOT";
                if (value  as string != null)
                {
                    if ( !bNot &&  (String)value  == "1" || bNot &&  (String)value  != "1")
                        return true;
                    return false;
                }
                return false;
            }

            public object ConvertBack(object value,
                                        Type targetType,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                var bNot = parameter as string != null && (String)parameter == "NOT";

                if (!bNot && (bool)value || bNot && (bool)value == false )
                    return  "1";
                return "0";
            }
        } 

     
}
