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
            return GetEnumDescription2((Enum)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); //<< -- 실무였다면 이런거 다 구현해야해요.
        }

        private static string GetEnumDescription(Enum enumObj)
        {
            FieldInfo fi = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fi.GetCustomAttributes(false);

            if (attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            else
            {
                var attrib = attribArray[0] as DescriptionAttribute;
                return attrib.Description;
            }
        }

        private static string GetEnumDescription2(Enum enumObj)
        {
            FieldInfo fi = enumObj.GetType().GetField(enumObj.ToString());

            object[] attribArray = fi.GetCustomAttributes(false);
            return attribArray.OfType<DescriptionAttribute>()
                                   .FirstOrDefault()
                                   ?.Description;
        }
    }
}
