using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Phieul.Converters {
    public class CheckedToTankConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if((bool)value) {
                return "FULL\nTANK";
            } else {
                return "PARTIAL\nTANK";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return null;
        }
    }
}
