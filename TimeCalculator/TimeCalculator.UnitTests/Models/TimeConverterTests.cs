using System;
using FluentAssertions;
using TimeCalculator.Models;
using Xunit;

namespace TimeCalculator.UnitTests.Models
{
    public class TimeConverterTests
    {
        #region Convert
        [Theory]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.MicroSecond, "123000000")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.MilliSecond, "123000")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Second, "123")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Minute, "2.05")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Hour, "0.034167")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Day, "0.001424")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Week, "0.000203")]
        [InlineData("123", ETimeUnit.Second, ETimeUnit.Year, "0.000004")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.MicroSecond, "691200000000")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.MilliSecond, "691200000")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Second, "691200")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Minute, "11520")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Hour, "192")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Day, "8")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Week, "1.142857")]
        [InlineData("8", ETimeUnit.Day, ETimeUnit.Year, "0.0219178")]
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
