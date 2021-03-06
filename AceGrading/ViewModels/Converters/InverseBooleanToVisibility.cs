﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AceGrading
{
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }

    public sealed class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToVisibilityConverter() :
            base(Visibility.Hidden, Visibility.Visible)
        { }
    }

    public sealed class BooleanToCollapseConverter : BooleanConverter<Visibility>
    {
        public BooleanToCollapseConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }

    public sealed class InverseBooleanToCollapseConverter : BooleanConverter<Visibility>
    {
        public InverseBooleanToCollapseConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        { }
    }

    public sealed class InverseBoolean : BooleanConverter<bool>
    {
        public InverseBoolean() : base(false, true) { }
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
