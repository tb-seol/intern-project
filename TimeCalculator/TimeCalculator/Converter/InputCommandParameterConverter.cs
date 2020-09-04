namespace TimeCalculator.Converter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Input;

    public class InputCommandParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                Key key => GetInputCommandParameterFromKeyBinding(key),
                string content => GetInputCommandParameterFromButtonContent(content),
                _ => throw new InvalidOperationException()
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static char GetInputCommandParameterFromKeyBinding(Key input)
        {
            return input switch
            {
                Key.NumPad0 => '0',
                Key.NumPad1 => '1',
                Key.NumPad2 => '2',
                Key.NumPad3 => '3',
                Key.NumPad4 => '4',
                Key.NumPad5 => '5',
                Key.NumPad6 => '6',
                Key.NumPad7 => '7',
                Key.NumPad8 => '8',
                Key.NumPad9 => '9',
                Key.Decimal => '.',
                _ => throw new NotSupportedException(input.ToString()),
            };
        }

        private static char GetInputCommandParameterFromButtonContent(string input)
        {
            switch (input)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ".":
                    return char.Parse(input.ToString());
                default:
                    throw new NotSupportedException(input.ToString());
            }
        }
    }
}
