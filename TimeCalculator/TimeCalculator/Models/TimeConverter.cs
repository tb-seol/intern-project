namespace TimeCalculator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class TimeConverter
    {
        private const ulong MICRO_SECOND_VALUE = 1;
        private const ulong MILLI_SECOND_VALUE = MICRO_SECOND_VALUE * 1000;
        private const ulong SECOND_VALUE = MILLI_SECOND_VALUE * 1000;
        private const ulong MINUTE_VALUE = SECOND_VALUE * 60;
        private const ulong HOUR_VALUE = MINUTE_VALUE * 60;
        private const ulong DAY_VALUE = HOUR_VALUE * 24;
        private const ulong WEEK_VALUE = DAY_VALUE * 7;
        private const ulong YEAR_VALUE = DAY_VALUE * 365;

        private static readonly List<ulong> _unitValues = new List<ulong> {
            MICRO_SECOND_VALUE,
            MILLI_SECOND_VALUE,
            SECOND_VALUE,
            MINUTE_VALUE,
            HOUR_VALUE,
            DAY_VALUE,
            WEEK_VALUE,
            YEAR_VALUE
        };

        static TimeConverter()
        {
            Debug.Assert(_unitValues.Count == Enum.GetNames(typeof(ETimeUnit)).Length,
                "enum count does not match");
        }

        public static string Convert(string srcTime, ETimeUnit srcTimeUnit, ETimeUnit destTimeUnit)
        {
            double srcTimeValue = double.Parse(srcTime);

            double microSeconds = GetMicroSecondsFrom(srcTimeValue, srcTimeUnit);
            double convertedTime = ConvertMicroSeconds(microSeconds, destTimeUnit);

            return convertedTime.ToString();
        }

        private static double GetMicroSecondsFrom(double time, ETimeUnit timeUnit)
        {
            return time * _unitValues[(int)timeUnit];
        }

        private static double ConvertMicroSeconds(double microSeconds, ETimeUnit timeUnit)
        {
            return microSeconds / _unitValues[(int)timeUnit];
        }
    }
}
