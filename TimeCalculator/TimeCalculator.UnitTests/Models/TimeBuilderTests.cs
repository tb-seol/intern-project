using System;
using FluentAssertions;
using TimeCalculator.Models;
using Xunit;

namespace TimeCalculator.UnitTests.Models
{
    public class TimeBuilderTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct_correctly()
        {
            // Arrange

            // Act
            var sut = new Action(() =>
            {
                new TimeBuilder();
            });

            // Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region SetTime
        [Theory]
        [InlineData(null, "0")]
        [InlineData("", "0")]
        [InlineData("0", "0")]
        [InlineData("1", "1")]
        [InlineData("100", "100")]
        [InlineData("1234.56", "1234.56")]
        [InlineData("0.01", "0.01")]
        [InlineData("-5", "0")]
        [InlineData("abcd", "0")]
        [InlineData("1.2.3", "0")]
        public void SetTime_does_set_time_correctly(
            string inputValue, string expectedValue)
        {
            // Arrange
            var sut = new TimeBuilder();

            // Act
            sut.SetTime(inputValue);

            // Assert
            string result = sut.ToBuild();
            result.Should().Be(expectedValue);
        }
        #endregion

        #region ToBuild
        [Theory]
        [InlineData("", "0")]
        [InlineData("0", "0")]
        [InlineData("1234", "1234")]
        [InlineData("123.456", "123.456")]
        [InlineData("0.123", "0.123")]
        [InlineData("-123", "0")]
        [InlineData("asdf", "0")]
        [InlineData("1.2.3", "0")]
        [InlineData(null, "0")]
        public void ToBuild_does_return_correctly(
            string inputValue, string expectedValue)
        {
            // Arrange
            var sut = new TimeBuilder();
            sut.SetTime(inputValue);

            // Act
            string result = sut.ToBuild();

            // Assert
            result.Should().Be(expectedValue);
        }
        #endregion

        #region AppendCharacter
        [Theory]
        [InlineData("", '0', "0")]
        [InlineData("", '1', "1")]
        [InlineData("", '.', "0.")]
        [InlineData("1", '.', "1.")]
        [InlineData("9", '0', "90")]
        [InlineData("123", '4', "1234")]
        [InlineData("99999999.999", '9', "99999999.9999")]
        [InlineData("123.456", '.', "123.456")]
        [InlineData("123.", '.', "123.")]
        [InlineData("123.", ',', "123.")]
        [InlineData("0.00000000", '0', "0.000000000")]
        [InlineData("0.23456789012345", '6', "0.23456789012345")]
        [InlineData("0.2345678901234", '5', "0.23456789012345")]
        [InlineData("123456789012345", '0', "123456789012345")]
        [InlineData("123456789012345", '.', "123456789012345.")]
        [InlineData("1234567890.1234", '5', "1234567890.12345")]
        [InlineData("1234567890.12345", '6', "1234567890.12345")]
        // AppendCharacter는 축적된 input의 마지막 요소로 character를 이어 붙인다.
        public void AppendCharacter_does_append_char_at_last_of_input(
            string initialInput, char appendingChar, string expectedInput)
        {
            // Arrange
            var sut = new TimeBuilder();
            sut.SetTime(initialInput);

            // Act
            sut.AppendCharacter(appendingChar);

            // Assert
            string result = sut.ToBuild();
            result.Should().Be(expectedInput);
        }
        #endregion

        #region Clear
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("1.2")]
        [InlineData("-1")]
        [InlineData("asdf")]
        [InlineData("1.2.3..")]
        public void Clear_does_set_time_to_zero(
            string inputValue)
        {
            // Arrange
            var sut = new TimeBuilder();
            sut.SetTime(inputValue);

            // Act
            sut.Clear();

            // Assert
            string result = sut.ToBuild();
            result.Should().Be("0");
        }
        #endregion

        #region RemoveLastCharacter
        [Theory]
        [InlineData(null, "0")]
        [InlineData("0", "0")]
        [InlineData("1", "0")]
        [InlineData(".", "0")]
        [InlineData("10", "1")]
        [InlineData("23", "2")]
        [InlineData("3.", "3")]
        [InlineData("5.7", "5.")]
        [InlineData("0.01", "0.0")]
        [InlineData("748564843", "74856484")]
        [InlineData("1.0000005", "1.000000")]
        public void RemoveLastCharacter_does_remove_last_char(
            string inputValue, string expectedValue)
        {
            // Arrange
            var sut = new TimeBuilder();
            sut.SetTime(inputValue);

            // Act
            sut.RemoveLastCharacter();

            // Assert
            string result = sut.ToBuild();
            result.Should().Be(expectedValue);
        }
        #endregion
    }
}
