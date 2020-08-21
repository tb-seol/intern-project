namespace TimeCalculator
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IsLessThanMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return false;

            if (int.TryParse(values[0].ToString(), out int firstValue) == false)
                throw new InvalidOperationException("The first value could not be converted to an integer");

            if (int.TryParse(values[1].ToString(), out int secondValue) == false)
                throw new InvalidOperationException("The second value could not be converted to an integer");

            return firstValue < secondValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
