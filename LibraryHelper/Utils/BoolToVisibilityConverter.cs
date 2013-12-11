namespace LibraryHelper.Utils
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Property converter from Boolean to Visibility
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // ONE-WAY Conversion!
            return null;
        }
    }
}
