namespace TimeCalculator
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;

    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "cannot be null");

            if (value is Enum == false)
                throw new InvalidOperationException("The value could not be converted to an enum");

            return GetDescriptionFromEnum((Enum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string GetDescriptionFromEnum(Enum enumObj)
        {
            FieldInfo fi = enumObj.GetType().GetField(enumObj.ToString());
            object[] attribArray = fi.GetCustomAttributes(false);

            return attribArray.OfType<DescriptionAttribute>()
                                   .FirstOrDefault()
                                   ?.Description;
        }
    }
}
