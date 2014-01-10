using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryHelper.Utils
{
    /// <summary>
    /// Property converter from string to string.
    /// 
    /// </summary>
    public class StringToStripStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((string)value).Replace(" ", ".");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // One-Way Conversion!
            return null;
        }
    }
}
