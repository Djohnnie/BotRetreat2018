﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Wpf.Framework.Converters
{
    public class HealthToBrushConverter : IValueConverter
    {
        public Brush LowBrush { get; set; }

        public Brush MediumBrush { get; set; }

        public Brush HighBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var health = value as HealthDto;
            return health == null ? LowBrush : ((Single)health.Current / health.Maximum > .666f ? HighBrush : ((Single)health.Current / health.Maximum < .333f ? LowBrush : MediumBrush));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from Brush to Health is not supported!");
        }
    }
}