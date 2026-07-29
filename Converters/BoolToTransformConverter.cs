using Avalonia.Data.Converters;
using Avalonia.Media.Transformation;
using System;
using System.Globalization;

namespace Panzerfaust.Converters
{
    public class BoolToTransformConverter : IValueConverter
    {
        public string TrueValue { get; set; } = "none";
        public string FalseValue { get; set; } = "none";

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => TransformOperations.Parse(value is true ? TrueValue : FalseValue);

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
