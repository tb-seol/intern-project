using System;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace TimeCalculator.UnitTests.Converter
{
    public class IsWidthLessThanHeightMultiConverterTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct_correctly()
        {
            // Arrange

            // Act
            var sut = new Action(() =>
            {
                new IsWidthLessThanHeightMultiConverter();
            });

            // Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region Convert
        [Theory]
        [InlineData(null, null, null, null, typeof(bool), false)]
        [InlineData(new int[] { 100, 200 }, null, null, null, typeof(bool), true)]
        [InlineData(new int[] { 100, 200 }, typeof(int), false, null, typeof(bool), true)]
        [InlineData(new int[] { 3000, 50000 }, typeof(int), false, null, typeof(bool), true)]
        [InlineData(new int[] { -100, 50000 }, typeof(int), false, null, typeof(bool), true)]
        [InlineData(new int[] { 200, 50 }, typeof(double), "abc", null, typeof(bool), false)]
        [InlineData(new int[] { 200, -29999 }, typeof(float), -5, null, typeof(bool), false)]
        [InlineData(new int[] { 200, 200 }, typeof(float), -5, null, typeof(bool), false)]
        public void Convert_does_return_correctly(
            int[] values, Type targetType, object parameter, CultureInfo cultureInfo,
            Type expectedResultType, bool expectedResult)
        {
            // Arrange
            var sut = new IsWidthLessThanHeightMultiConverter();
            object[] converted = values?.Cast<object>().ToArray();

            // Act
            object result = sut.Convert(converted, targetType, parameter, cultureInfo);

            // Assert
            result.Should().BeOfType(expectedResultType);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(new object[] { }, null, null, null)]
        [InlineData(new object[] { 200 }, null, null, null)]
        [InlineData(new object[] { 200, 100, 300 }, null, null, null)]
        [InlineData(new object[] { 200, 100, 300, 400 }, null, null, null)]
        [InlineData(new object[] { 200, 100, 300, 400, 500 }, null, null, null)]
        public void Convert_does_throw_ArgumentException_if_incorrect_length_of_values(
            object[] values, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            // Arrange
            var sut = new IsWidthLessThanHeightMultiConverter();

            // Act
            var tryConvert = new Action(() =>
            {
                sut.Convert(values, targetType, parameter, cultureInfo);
            });

            // Assert
            tryConvert.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(1234, 123.4f)]
        [InlineData(123.4d, 1234)]
        [InlineData(123.4d, 123.4f)]
        public void Convert_does_throw_InvalidOperationException_if_typeof_values_are_not_integer(
            object width, object height)
        {
            // Arrange
            var sut = new IsWidthLessThanHeightMultiConverter();
            object[] values = new object[] { width, height };

            // Act
            var tryConvert = new Action(() =>
            {
                sut.Convert(values, null, null, null);
            });

            // Assert
            tryConvert.Should().Throw<InvalidOperationException>();
        }
        #endregion
    }
}
