using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace TimeCalculator
{
    public class Time
    {
        private const ulong MILLI_SECOND_UNIT = 1000;
        private const ulong SECOND_UNIT = MILLI_SECOND_UNIT * 1000;
        private const ulong MINUTE_UNIT = SECOND_UNIT * 60;
        private const ulong HOUR_UNIT = MINUTE_UNIT * 60;
        private const ulong DAY_UNIT = HOUR_UNIT * 24;
        private const ulong WEEK_UNIT = DAY_UNIT * 7;
        private const ulong YEAR_UNIT = DAY_UNIT * 365;
        private const string ZERO = "0";

        private static List<char> VALIDATE_CHARACTERS = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };

        private StringBuilder mMicroSeconds = new StringBuilder(1024);
        private bool bUsedDecimalPoint = false;

        public void Clear()
        {
            mMicroSeconds.Clear();
            bUsedDecimalPoint = false;
        }

        public void Delete()
        {
            if (mMicroSeconds.Length > 0)
            {
                mMicroSeconds.Length--;
            }
        }

        public void AppendCharacter(char character)
        {
            if (!VALIDATE_CHARACTERS.Contains(character))
            {
                return;
            }

            if (character == '.')
            {
                if (bUsedDecimalPoint)
                {
                    return;
                }

                bUsedDecimalPoint = true;
            }

            mMicroSeconds.Append(character);
        }

        public string ConvertTo(ETimeUnit timeUnit)
        {
            if (mMicroSeconds.Length == 0)
            {
                return ZERO;
            }

            double convertedTime = double.MinValue;

            double microSeconds;

            if (double.TryParse(mMicroSeconds.ToString(), out microSeconds))
            {
                switch (timeUnit)
                {
                    case ETimeUnit.MicroSecond:
                        convertedTime = microSeconds;
                        break;
                    case ETimeUnit.MilliSecond:
                        convertedTime = microSeconds / MILLI_SECOND_UNIT;
                        break;
                    case ETimeUnit.Second:
                        convertedTime = microSeconds / SECOND_UNIT;
                        break;
                    case ETimeUnit.Minute:
                        convertedTime = microSeconds / MINUTE_UNIT;
                        break;
                    case ETimeUnit.Hour:
                        convertedTime = microSeconds / HOUR_UNIT;
                        break;
                    case ETimeUnit.Day:
                        convertedTime = microSeconds / DAY_UNIT;
                        break;
                    case ETimeUnit.Week:
                        convertedTime = microSeconds / WEEK_UNIT;
                        break;
                    case ETimeUnit.Year:
                        convertedTime = microSeconds / YEAR_UNIT;
                        break;
                    default:
                        Debug.Assert(false, "unknown type");
                        convertedTime = double.MinValue;
                        break;
                }
            }

            Debug.Assert(convertedTime != double.MinValue);

            return convertedTime.ToString("N");
        }
    }
}
