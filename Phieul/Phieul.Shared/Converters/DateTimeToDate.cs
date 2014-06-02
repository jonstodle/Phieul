using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Phieul.Converters
{
    public class DateTimeToDate : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((DateTime)value).ToString("d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return null;
        }
    }
}
