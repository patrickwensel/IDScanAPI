using System;
using System.Globalization;
using Xamarin.Forms;

namespace IDScan.Converters
{
    public class BoolInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool) || value == null)
                return false;

            bool val = (bool)value;
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
