using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace TimeCalculator.UnitTests.Converter
{
    public class EnumDescriptionConverterTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct_correctly()
        {
            // Arrange

            // Act
            var sut = new Action(() =>
            {
                new EnumDescriptionConverter();
            });

            // Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region Convert
        [Theory]
        [InlineData(ETimeUnit.MicroSecond, null, null, null, typeof(string), "마이크로초")]
        [InlineData(ETimeUnit.MilliSecond, null, null, null, typeof(string), "밀리초")]
        [InlineData(ETimeUnit.Second, null, null, null, typeof(string), "초")]
        [InlineData(ETimeUnit.Minute, null, null, null, typeof(string), "분")]
        [InlineData(ETimeUnit.Hour, null, null, null, typeof(string), "시간")]
        [InlineData(ETimeUnit.Day, null, null, null, typeof(string), "일")]
        [InlineData(ETimeUnit.Week, null, null, null, typeof(string), "주")]
        [InlineData(ETimeUnit.Year, null, null, null, typeof(string), "년")]
        [InlineData(ETimeUnit.MicroSecond, typeof(bool), 4182794, null, typeof(string), "마이크로초")]
        [InlineData(ETimeUnit.MilliSecond, typeof(int), 'k', null, typeof(string), "밀리초")]
        [InlineData(ETimeUnit.Second, typeof(float), "asjfoweaoui", null, typeof(string), "초")]
        [InlineData(ETimeUnit.Minute, typeof(uint), 3.1235f, null, typeof(string), "분")]
        public void Convert_does_return_correctly(
            Enum enumValue, Type targetType, object parameter, CultureInfo culture,
            Type expectedResultType, string expectedResult)
        {
            // Arrange
            var sut = new EnumDescriptionConverter();

            // Act
            object result = sut.Convert(enumValue, targetType, parameter, culture);

            // Assert
            result.Should().BeOfType(expectedResultType);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(null)]
        public void Convert_does_throw_ArgumentNullException_if_value_is_null(
            Enum enumValue)
        {
            // Arrange
            var sut = new EnumDescriptionConverter();

            // Act
            var tryConvert = new Action(() =>
            {
                sut.Convert(enumValue, null, null, null);
            });

            // Assert
            tryConvert.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        [InlineData(-5.7d)]
        [InlineData('a')]
        [InlineData("abcd")]
        public void Convert_does_throw_InvalidOperationException_if_typeof_value_is_not_enum(
            object value)
        {
            // Arrange
            var sut = new EnumDescriptionConverter();

            // Act
            var tryConvert = new Action(() =>
            {
                sut.Convert(value, null, null, null);
            });

            // Assert
            tryConvert.Should().Throw<InvalidOperationException>();
        }
        #endregion
    }
}
