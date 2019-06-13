using System;
using FluentAssertions;
using Framework.Domain.Services.Time;
using Xunit;

namespace Framework.Domain.UnitTests.Services.Time
{
    public class SystemTimeProviderTests
    {
        private static readonly TimeSpan TimeDelta = TimeSpan.FromMilliseconds(2);
        private const long UnixTimeDelta = 2;

        private static SystemTimeProvider GetInstance()
        {
            return new SystemTimeProvider();
        }

        [Fact]
        public void GetCurrentDateShouldReturnDateOnly()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.Date;
            var instance = GetInstance();

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
            var instance = GetInstance();

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
            var instance = GetInstance();

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
            var instance = GetInstance();

            // Act
            var actual = instance.GetLocalDateAndTimeWithOffset();

            // Assert
            actual.Should().BeCloseTo(expected, TimeSpan.FromMilliseconds(2));
        }

        [Fact]
        public void GetCurrentUtcDateAndTimeWithOffsetShouldReturnUtcDateAndTime()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.UtcDateTime;
            var instance = GetInstance();

            // Act
            var actual = instance.GetUtcDateAndTimeWithOffset();

            // Assert
            actual.Should().BeCloseTo(expected, TimeSpan.FromMilliseconds(2));
        }

        [Fact]
        public void GetUnixTimeInSecondsShouldReturnUnixTimeInSeconds()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.ToUnixTimeSeconds();
            var instance = new SystemTimeProvider();

            // Act
            var actual = instance.GetUnixTimeInSeconds();

            // Assert
            actual.Should().BeCloseTo(expected, UnixTimeDelta);
        }

        [Fact]
        public void GetUnixTimeInMillisecondsShouldReturnUnixTimeInMilliseconds()
        {
            // Arrange
            var value = DateTimeOffset.Now;
            var expected = value.ToUnixTimeMilliseconds();
            var instance = new SystemTimeProvider();

            // Act
            var actual = instance.GetUnixTimeInMilliseconds();

            // Assert
            actual.Should().BeCloseTo(expected, UnixTimeDelta);
        }

        #region Constructor tests

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance();

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion
    }
}