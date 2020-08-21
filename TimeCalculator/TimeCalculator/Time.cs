namespace TimeCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

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

        private readonly StringBuilder _inputCharacters = new StringBuilder(MAX_LENGTH * 2);
        private bool _isUsedDecimalPoint;

        public ETimeUnit UnitForConvert { get; set; }

        public Time(ETimeUnit defaultUnit)
        {
            this.UnitForConvert = defaultUnit;
        }

        public void Clear()
        {
            this._inputCharacters.Clear();
            this._isUsedDecimalPoint = false;
        }

        public void RemoveLastCharacter()
        {
            if (this._inputCharacters.Length == 0)
                return;

            if (this._inputCharacters[^1] == '.')
                this._isUsedDecimalPoint = false;

            this._inputCharacters.Length--;
        }

        public void AppendCharacter(char character)
        {
            if (CanAppend() == false && ValidateCharToAppend(character) == false)
                return;

            if (character == '.')
            {
                if (this._isUsedDecimalPoint)
                    return;

                this._isUsedDecimalPoint = true;
            }

            this._inputCharacters.Append(character);
        }

        public List<double> GetAllTimes()
        {
            double microSeconds = GetMicroSecondsFromInputCharacters();
            var times = new List<double>();

            foreach (ETimeUnit timeUnit in Enum.GetValues(typeof(ETimeUnit)))
            {
                times.Add(ConvertMicroSeconds(microSeconds, timeUnit));
            }

            return times;
        }

        private double GetMicroSecondsFromInputCharacters()
        {
            if (this._inputCharacters.Length == 0)
                return 0;

            if (double.TryParse(this._inputCharacters.ToString(), out double inputTime))
            {
                return this.UnitForConvert switch
                {
                    ETimeUnit.MicroSecond => inputTime,
                    ETimeUnit.MilliSecond => inputTime * MILLI_SECOND_UNIT,
                    ETimeUnit.Second => inputTime * SECOND_UNIT,
                    ETimeUnit.Minute => inputTime * MINUTE_UNIT,
                    ETimeUnit.Hour => inputTime * HOUR_UNIT,
                    ETimeUnit.Day => inputTime * DAY_UNIT,
                    ETimeUnit.Week => inputTime * WEEK_UNIT,
                    ETimeUnit.Year => inputTime * YEAR_UNIT,
                    _ => throw new NotSupportedException(this.UnitForConvert.ToString()),
                };
            }

            Debug.Assert(false, "failed to parse input characters");
            return double.MinValue;
        }

        private static double ConvertMicroSeconds(double microSeconds, ETimeUnit timeUnit)
        {
            double convertedTime = timeUnit switch
            {
                ETimeUnit.MicroSecond => microSeconds,
                ETimeUnit.MilliSecond => microSeconds / MILLI_SECOND_UNIT,
                ETimeUnit.Second => microSeconds / SECOND_UNIT,
                ETimeUnit.Minute => microSeconds / MINUTE_UNIT,
                ETimeUnit.Hour => microSeconds / HOUR_UNIT,
                ETimeUnit.Day => microSeconds / DAY_UNIT,
                ETimeUnit.Week => microSeconds / WEEK_UNIT,
                ETimeUnit.Year => microSeconds / YEAR_UNIT,
                _ => throw new NotSupportedException(timeUnit.ToString()),
            };

            return convertedTime;
        }

        private bool CanAppend()
        {
            if (this._inputCharacters.Length == MAX_LENGTH)
                return false;

            if (this._isUsedDecimalPoint == false &&
                this._inputCharacters.Length == MAX_LENGTH_WITHOUT_DECIMAL_POINT)
                return false;

            return true;
        }

        private static bool ValidateCharToAppend(char input)
        {
            if ((input < '0' || input > '9') && input != '.')
                return true;
            else
                return false;
        }
    }
}
