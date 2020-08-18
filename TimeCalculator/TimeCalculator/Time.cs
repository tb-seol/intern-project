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

        private StringBuilder mMicroSecondsStringBuilder = new StringBuilder(1024);
        private bool bUsedDecimalPoint = false;

        public void Clear()
        {
            mMicroSecondsStringBuilder.Clear();
            bUsedDecimalPoint = false;
        }

        public void Delete()
        {
            if (mMicroSecondsStringBuilder.Length > 0)
            {
                mMicroSecondsStringBuilder.Length--;
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

            mMicroSecondsStringBuilder.Append(character);
        }

        public string ConvertTo(ETimeUnit timeUnit)
        {
            if (mMicroSecondsStringBuilder.Length == 0)
            {
                return ZERO;
            }

            double time = double.MinValue;

            string strMicroSeconds = mMicroSecondsStringBuilder.ToString();

            double microSeconds;

            if (double.TryParse(strMicroSeconds, out microSeconds))
            {
                switch (timeUnit)
                {
                    case ETimeUnit.MicroSecond:
                        time = microSeconds;
                        break;
                    case ETimeUnit.MilliSecond:
                        time = microSeconds / MILLI_SECOND_UNIT;
                        break;
                    case ETimeUnit.Second:
                        time = microSeconds / SECOND_UNIT;
                        break;
                    case ETimeUnit.Minute:
                        time = microSeconds / MINUTE_UNIT;
                        break;
                    case ETimeUnit.Hour:
                        time = microSeconds / HOUR_UNIT;
                        break;
                    case ETimeUnit.Day:
                        time = microSeconds / DAY_UNIT;
                        break;
                    case ETimeUnit.Week:
                        time = microSeconds / WEEK_UNIT;
                        break;
                    case ETimeUnit.Year:
                        time = microSeconds / YEAR_UNIT;
                        break;
                    default:
                        Debug.Assert(false, "unknown type");
                        time = double.MinValue;
                        break;
                }
            }

            Debug.Assert(time != double.MinValue);

            return time.ToString("N");
        }
    }
}
