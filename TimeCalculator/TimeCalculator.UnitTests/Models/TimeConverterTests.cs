using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using TimeCalculator.Models;
using Xunit;

namespace TimeCalculator.UnitTests.Models
{
    public class TimeConverterTests
    {
        #region Convert
        public class Testset_Convert_does_return_correctly : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.MicroSecond, "123456789" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.MilliSecond, "123456.789" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Second, "123.456789" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Minute, "2.05761315" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Hour, "0.03429355" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Day, "0.0014289" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Week, "0.00020413" },
                new object[] { "123456789", ETimeUnit.MicroSecond, ETimeUnit.Year, "0.00000391" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.MicroSecond, "987654000" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.MilliSecond, "987654" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Second, "987.654" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Minute, "16.4609" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Hour, "0.274348" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Day, "0.011431" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Week, "0.001633" },
                new object[] { "987654", ETimeUnit.MilliSecond, ETimeUnit.Year, "0.000031" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.MicroSecond, "123000000" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.MilliSecond, "123000" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Second, "123" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Minute, "2.05" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Hour, "0.034167" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Day, "0.001424" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Week, "0.000203" },
                new object[] { "123", ETimeUnit.Second, ETimeUnit.Year, "0.000004" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.MicroSecond, "2142000000" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.MilliSecond, "2142000" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Second, "2142" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Minute, "35.7" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Hour, "0.595" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Day, "0.024792" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Week, "0.003542" },
                new object[] { "35.7", ETimeUnit.Minute, ETimeUnit.Year, "0.000068" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.MicroSecond, "34560000000" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.MilliSecond, "34560000" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Second, "34560" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Minute, "576" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Hour, "9.6" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Day, "0.4" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Week, "0.057143" },
                new object[] { "9.6", ETimeUnit.Hour, ETimeUnit.Year, "0.001095" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.MicroSecond, "691200000000" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.MilliSecond, "691200000" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Second, "691200" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Minute, "11520" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Hour, "192" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Day, "8" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Week, "1.142857" },
                new object[] { "8", ETimeUnit.Day, ETimeUnit.Year, "0.021903" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.MicroSecond, "449366400000" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.MilliSecond, "449366400" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Second, "449366.4" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Minute, "7489.44" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Hour, "124.824" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Day, "5.201" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Week, "0.743" },
                new object[] { "0.743", ETimeUnit.Week, ETimeUnit.Year, "0.01424" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.MicroSecond, "31557600000" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.MilliSecond, "31557600" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Second, "31557.6" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Minute, "525.96" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Hour, "8.766" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Day, "0.36525" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Week, "0.052179" },
                new object[] { "0.001", ETimeUnit.Year, ETimeUnit.Year, "0.001"}
            };

            public IEnumerator<object[]> GetEnumerator() => this._data.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(Testset_Convert_does_return_correctly))]
        public void Convert_does_return_correctly(
            string source, ETimeUnit oldUnit, ETimeUnit newUnit,
            string expectedValue)
        {
            // Arrange

            // Act
            string newTime = TimeConverter.Convert(source, oldUnit, newUnit);

            // Assert
            string result = Math.Round(double.Parse(newTime), GetFractionalDigits(expectedValue))
                                .ToString("0.##############");
            result.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData(".")]
        [InlineData("..")]
        [InlineData("-")]
        [InlineData("+")]
        [InlineData(".1.")]
        [InlineData("1.2.3")]
        [InlineData("asdf")]
        public void Convert_does_throw_FormatException_if_typeof_value_is_not_double(
            string inputValue)
        {
            // Arrange
            ETimeUnit oldUnit = ETimeUnit.Day;
            ETimeUnit newUnit = ETimeUnit.Hour;

            // Act
            var converting = new Action(() =>
            {
                TimeConverter.Convert(inputValue, oldUnit, newUnit);
            });

            // Assert
            converting.Should().Throw<FormatException>();
        }
        #endregion

        private int GetFractionalDigits(string num)
        {
            int decimalPointIndex = num.IndexOf('.');

            if (decimalPointIndex >= 0)
                return num.Length - decimalPointIndex - 1;
            else
                return 0;
        }
    }
}
