#region Usings

using System;
using FluentAssertions;
using Framework.Domain.Services.Time;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Services.Time
{
    public class FixedTimeProviderTests
    {
        private static readonly TimeSpan TimeDelta = TimeSpan.FromMilliseconds(2);

        private static FixedTimeProvider GetInstance(DateTime moment)
        {
            return new FixedTimeProvider(moment);
        }

        private static FixedTimeProvider GetInstance(DateTimeOffset moment)
        {
            return new FixedTimeProvider(moment);
        }

        [Fact]
        public void GetCurrentDateShouldReturnDateOnly()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.Date;
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetCurrentDate();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetCurrentLocalDateAndTimeShouldReturnLocalDateAndTime()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.DateTime;
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetLocalDateAndTime();

            // Assert
            actual.Should().BeCloseTo(expected, TimeDelta);
        }

        [Fact]
        public void GetCurrentUtcDateAndTimeShouldReturnUtcDateAndTime()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.UtcDateTime;
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetUtcDateAndTime();

            // Assert
            actual.Should().BeCloseTo(expected, TimeDelta);
        }

        [Fact]
        public void GetCurrentLocalDateAndTimeWithOffsetShouldReturnLocalDateAndTime()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.DateTime;
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetLocalDateAndTimeWithOffset();

            // Assert
            actual.Should().BeCloseTo(expected, TimeDelta);
        }

        [Fact]
        public void GetCurrentUtcDateAndTimeWithOffsetShouldReturnUtcDateAndTime()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.UtcDateTime;
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetUtcDateAndTimeWithOffset();

            // Assert
            actual.Should().BeCloseTo(expected, TimeDelta);
        }

        [Fact]
        public void GetUnixTimeInSecondsShouldReturnUnixTimeInSeconds()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.ToUnixTimeSeconds();
            var instance = new FixedTimeProvider(value);
            // Act
            var actual = instance.GetUnixTimeInSeconds();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetUnixTimeInMilliSecondsShouldReturnUnixTimeInMilliSeconds()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.ToUnixTimeMilliseconds();
            var instance = new FixedTimeProvider(value);
            // Act
            var actual = instance.GetUnixTimeInMilliseconds();

            // Assert
            actual.Should().Be(expected);
        }
        #region Constructor tests

        [Fact]
        public void ConstructorWithDateTimeShouldNotThrowException()
        {
            // Arrange
            var moment = DateTime.Now;

            // Act
            Action constructorUnderTest = () => GetInstance(moment);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        [Fact]
        public void ConstructorWithDateTimeOffsetShouldNotThrowException()
        {
            // Arrange
            var moment = DateTimeOffset.Now;

            // Act
            Action constructorUnderTest = () => GetInstance(moment);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion
    }
}