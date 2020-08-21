using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private const int MAX_LENGTH = 16;
        private const int MAX_LENGTH_WITHOUT_DECIMAL_POINT = MAX_LENGTH - 1;


        private bool bUsedDecimalPoint = false;
        private StringBuilder mInputCharacters = new StringBuilder(MAX_LENGTH * 2);


        public ETimeUnit UnitForConvert { get; set; }

        public Time(ETimeUnit defaultUnit)
        {
            UnitForConvert = defaultUnit;
        }

        public void Clear()
        {
            mInputCharacters.Clear();
            bUsedDecimalPoint = false;
        }

        public void RemoveLastCharacter()
        {
            if (mInputCharacters.Length == 0)
            {
                return;
            }

            if (mInputCharacters[mInputCharacters.Length - 1] == '.')
            {
                bUsedDecimalPoint = false;
            }

            mInputCharacters.Length--;
        }

        public void AppendCharacter(char character)
        {
            if (mInputCharacters.Length == MAX_LENGTH)
            {
                return;
            }

            if (!bUsedDecimalPoint &&
                mInputCharacters.Length == MAX_LENGTH_WITHOUT_DECIMAL_POINT)
            {
                return;
            }

            if ((character < '0' || character > '9') && character != '.')
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

            mInputCharacters.Append(character);
        }

        public List<double> GetAllTimes()
        {
            double microSeconds = getMicroSecondsFromInputCharacters();
            List<double> times = new List<double>();

            foreach (ETimeUnit timeUnit in Enum.GetValues(typeof(ETimeUnit)))
            {
                times.Add(convertMicroSeconds(microSeconds, timeUnit));
            }

            return times;
        }

        private double getMicroSecondsFromInputCharacters()
        {
            if (mInputCharacters.Length == 0)
            {
                return 0;
            }

            double inputTime;
            double microSeconds = double.MinValue;

            if (double.TryParse(mInputCharacters.ToString(), out inputTime))
            {
                switch (UnitForConvert)
                {
                    case ETimeUnit.MicroSecond:
                        microSeconds = inputTime;
                        break;
                    case ETimeUnit.MilliSecond:
                        microSeconds = inputTime * MILLI_SECOND_UNIT;
                        break;
                    case ETimeUnit.Second:
                        microSeconds = inputTime * SECOND_UNIT;
                        break;
                    case ETimeUnit.Minute:
                        microSeconds = inputTime * MINUTE_UNIT;
                        break;
                    case ETimeUnit.Hour:
                        microSeconds = inputTime * HOUR_UNIT;
                        break;
                    case ETimeUnit.Day:
                        microSeconds = inputTime * DAY_UNIT;
                        break;
                    case ETimeUnit.Week:
                        microSeconds = inputTime * WEEK_UNIT;
                        break;
                    case ETimeUnit.Year:
                        microSeconds = inputTime * YEAR_UNIT;
                        break;
                    default:
                        Debug.Assert(false, "unknown type");
                        break;
                }
            }
            else
            {
                Debug.Assert(false);
            }

            Debug.Assert(microSeconds != double.MinValue);

            return microSeconds;
        }

        private double convertMicroSeconds(double microSeconds, ETimeUnit timeUnit)
        {
            double convertedTime = double.MinValue;

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
                    break;
            }

            Debug.Assert(convertedTime != double.MinValue);

            return convertedTime;
        }
    }
}
