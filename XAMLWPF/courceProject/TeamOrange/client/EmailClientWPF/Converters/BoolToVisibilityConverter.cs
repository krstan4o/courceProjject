﻿using System;
using System.Windows;
using System.Windows.Data;

namespace EmailClientWPF.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
            {
                return null;
            }
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            var isVisible = bool.Parse(value.ToString());

            return (isVisible) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
