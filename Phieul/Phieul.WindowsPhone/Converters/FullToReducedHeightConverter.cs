﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Phieul.Converters {
    public class FullToReducedHeightConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (double)value * .9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return null;
        }
    }
}
