namespace TimeCalculator
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IsWidthLessThanHeightMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return false;

            if (values.Length != 2)
                throw new ArgumentException("length of values must be 2");

            if (int.TryParse(values[0].ToString(), out int width) == false)
                throw new InvalidOperationException("The width could not be converted to an integer");

            if (int.TryParse(values[1].ToString(), out int height) == false)
                throw new InvalidOperationException("The height could not be converted to an integer");

            return width < height;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
