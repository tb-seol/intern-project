using System;
using FluentAssertions;
using Saige.MVVM;
using TimeCalculator.ViewModels;
using Xunit;

namespace TimeCalculator.UnitTests.ViewModels
{
    public class TimeCalculatorViewModelTests
    {
        #region Constructor
        [Fact]
        public void Constructor_does_construct_correctly()
        {
            // Arrange

            // Act
            var sut = new Action(() =>
            {
                new TimeCalculatorViewModel();
            });

            // Assert
            sut.Should().NotThrow();
        }
        #endregion

        #region Time
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("1.01")]
        [InlineData("1234")]
        [InlineData("9.99999999")]
        [InlineData("99999999.9")]
        [InlineData("0.00000000001")]
        [InlineData("123456789012345")]
        [InlineData("123456789012345.")]
        [InlineData("1.23456789012345")]
        public void Time_getter_does_return_time_correctly(
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            foreach (char ch in expectedTime)
                sut.AppendCharacter(ch);

            // Act
            string result = sut.Time;

            // Assert
            result.Should().Be(expectedTime);
        }
        #endregion

        #region SelectedTimeUnit
        [Theory]
        [InlineData(ETimeUnit.MicroSecond)]
        [InlineData(ETimeUnit.MilliSecond)]
        [InlineData(ETimeUnit.Second)]
        [InlineData(ETimeUnit.Minute)]
        [InlineData(ETimeUnit.Hour)]
        [InlineData(ETimeUnit.Day)]
        [InlineData(ETimeUnit.Week)]
        [InlineData(ETimeUnit.Year)]
        public void SelectedTimeUnit_getter_does_return_unit_correctly(
            ETimeUnit expectedUnit)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            sut.SelectedTimeUnit = expectedUnit;

            // Act
            ETimeUnit result = sut.SelectedTimeUnit;

            // Assert
            result.Should().Be(expectedUnit);
        }

        [Theory]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Day)]
        [InlineData(ETimeUnit.MilliSecond, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Second, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Minute, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Hour, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Day, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Week, ETimeUnit.Day)]
        [InlineData(ETimeUnit.Year, ETimeUnit.Day)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.MicroSecond)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.MilliSecond)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Second)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Minute)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Hour)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Week)]
        [InlineData(ETimeUnit.MicroSecond, ETimeUnit.Year)]
        public void SelectedTimeUnit_setter_does_set_unit_correctly(
            ETimeUnit initUnit,
            ETimeUnit expectedTimeUnit)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            sut.SelectedTimeUnit = initUnit;

            // Act
            sut.SelectedTimeUnit = expectedTimeUnit;

            // Assert
            ETimeUnit result = sut.SelectedTimeUnit;
            result.Should().Be(expectedTimeUnit);
        }
        #endregion

        #region InputCommand
        [Fact]
        public void InputCommand_getter_does_not_return_null()
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            RelayCommand<char> result = sut.InputCommand;

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData('0', "0")]
        [InlineData('1', "1")]
        [InlineData('2', "2")]
        [InlineData('3', "3")]
        [InlineData('4', "4")]
        [InlineData('5', "5")]
        [InlineData('6', "6")]
        [InlineData('7', "7")]
        [InlineData('8', "8")]
        [InlineData('9', "9")]
        [InlineData('.', "0.")]
        public void Execute_of_InputCommand_does_return_correctly(
            char commandParameter,
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            sut.InputCommand.Execute(commandParameter);

            // Assert
            sut.Time.Should().Be(expectedTime);
        }

        [Theory]
        [InlineData(',')]
        [InlineData('-')]
        [InlineData('+')]
        [InlineData('`')]
        [InlineData('|')]
        [InlineData('?')]
        [InlineData('a')]
        [InlineData('A')]
        [InlineData('ㅇ')]
        [InlineData('★')]
        [InlineData('/')]
        [InlineData('\\')]
        public void Excute_of_InputCommand_does_return_zero_as_string(
            char commandParameter)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            sut.InputCommand.Execute(commandParameter);

            // Assert
            sut.Time.Should().Be("0");
        }
        #endregion

        #region ClearCommand
        [Fact]
        public void ClearCommand_getter_does_not_return_null()
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            RelayCommand result = sut.ClearCommand;

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("5")]
        [InlineData(".")]
        [InlineData("123")]
        [InlineData("123.456")]
        [InlineData("123456.")]
        [InlineData("0.")]
        [InlineData("0.00")]
        [InlineData("0.00001")]
        [InlineData("asdf")]
        [InlineData("-+-+-+-")]
        [InlineData("ㅁㄴㅇㄹㅋㅌㅊㅍ")]
        [InlineData("@@@@////????!!!!")]
        public void Execute_of_ClearCommand_does_clear_correctly(
            string initTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            foreach (char initChar in initTime)
                sut.AppendCharacter(initChar);

            // Act
            sut.ClearCommand.Execute(null);

            // Assert
            sut.Time.Should().Be("0");
        }
        #endregion

        #region DeleteCommand
        [Fact]
        public void DeleteCommand_getter_does_not_return_null()
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            RelayCommand result = sut.DeleteCommand;

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData("", "0")]
        [InlineData("0", "0")]
        [InlineData("5", "0")]
        [InlineData("5.", "5")]
        [InlineData("1.2", "1.")]
        [InlineData("34.", "34")]
        [InlineData("0.00", "0.0")]
        [InlineData("123456.789", "123456.78")]
        public void Execute_of_DeleteCommand_does_delete_correctly(
            string initTime,
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            foreach (char initChar in initTime)
                sut.AppendCharacter(initChar);

            // Act
            sut.DeleteCommand.Execute(null);

            // Assert
            sut.Time.Should().Be(expectedTime);
        }
        #endregion

        #region AppendCharacter
        [Theory]
        [InlineData(new char[] { '0' }, "0")]
        [InlineData(new char[] { '1' }, "1")]
        [InlineData(new char[] { '2' }, "2")]
        [InlineData(new char[] { '3' }, "3")]
        [InlineData(new char[] { '4' }, "4")]
        [InlineData(new char[] { '5' }, "5")]
        [InlineData(new char[] { '6' }, "6")]
        [InlineData(new char[] { '7' }, "7")]
        [InlineData(new char[] { '8' }, "8")]
        [InlineData(new char[] { '9' }, "9")]
        [InlineData(new char[] { '.' }, "0.")]
        [InlineData(new char[] { '0', '0' }, "0")]
        [InlineData(new char[] { '0', '5' }, "5")]
        [InlineData(new char[] { '0', '.' }, "0.")]
        [InlineData(new char[] { '1', '0' }, "10")]
        [InlineData(new char[] { '1', '5' }, "15")]
        [InlineData(new char[] { '1', '.' }, "1.")]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', '.', '3' }, "1.3")]
        [InlineData(new char[] { '1', '2', '.' }, "12.")]
        [InlineData(new char[] { '0', '.', '3' }, "0.3")]
        public void AppendCharacter_does_append_char_that_correct(
            char[] appendingChars,
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            foreach (char appendingChar in appendingChars)
                sut.AppendCharacter(appendingChar);

            // Assert
            sut.Time.Should().Be(expectedTime);
        }

        [Theory]
        [InlineData(new char[] { 'a', 'b', 'c' }, "0")]
        [InlineData(new char[] { 'ㄱ', 'ㄴ', 'ㄷ' }, "0")]
        [InlineData(new char[] { '-' }, "0")]
        [InlineData(new char[] { '.', '1', ',' }, "0.1")]
        [InlineData(new char[] { '5', 'q', '+' }, "5")]
        [InlineData(new char[] { '0', 'o', '1', 'O' }, "1")]
        public void AppendCharacter_does_not_append_char_that_incorrect(
            char[] appendingChars,
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();

            // Act
            foreach (char appendingChar in appendingChars)
                sut.AppendCharacter(appendingChar);

            // Assert
            sut.Time.Should().Be(expectedTime);
        }
        #endregion

        #region Clear
        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("5")]
        [InlineData(".")]
        [InlineData("123")]
        [InlineData("123.456")]
        [InlineData("0.123456")]
        [InlineData("0.00005")]
        [InlineData("123456789012345.")]
        public void Clear_does_set_Time_to_zero(
            string initTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            foreach (char initChar in initTime)
                sut.AppendCharacter(initChar);

            // Act
            sut.Clear();

            // Assert
            sut.Time.Should().Be("0");
        }
        #endregion

        #region RemoveLastCharacter
        [Theory]
        [InlineData("", "0")]
        [InlineData("0", "0")]
        [InlineData("5", "0")]
        [InlineData("0.", "0")]
        [InlineData("5.", "5")]
        [InlineData("123", "12")]
        [InlineData("123.", "123")]
        [InlineData("12.3", "12.")]
        public void RemoveLastCharacter_does_remove_last_char_of_Time(
            string initTime,
            string expectedTime)
        {
            // Arrange
            var sut = new TimeCalculatorViewModel();
            foreach (char initChar in initTime)
                sut.AppendCharacter(initChar);

            // Act
            sut.RemoveLastCharacter();

            // Assert
            sut.Time.Should().Be(expectedTime);
        }
        #endregion
    }
}
