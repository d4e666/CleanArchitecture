#region Usings

using FluentAssertions;
using Framework.Domain.Services.Time;
using Moq;
using System;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Services.Time
{
    public class TimeProviderTests
    {

        private class TimeProviderAccessor : TimeProvider
        {
            /// <inheritdoc />
            public override DateTime GetCurrentDate()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override DateTime GetLocalDateAndTime()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override DateTime GetUtcDateAndTime()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override DateTimeOffset GetLocalDateAndTimeWithOffset()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override DateTimeOffset GetUtcDateAndTimeWithOffset()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override long GetUnixTimeInSeconds()
            {
                throw new NotImplementedException();
            }

            /// <inheritdoc />
            public override long GetUnixTimeInMilliseconds()
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void SetCurrentShouldNotAllowNull()
        {
            // Arrange

            // Act
            Action methodUnderTest = () => TimeProvider.SetCurrent(null);

            // Assert
            methodUnderTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetCurrentShouldSetCurrentProperty()
        {
            // Arrange
            var providerMock = new Mock<ITimeProvider>();

            // Act
            TimeProvider.SetCurrent(providerMock?.Object);

            // Assert
            TimeProvider.Current.Should().NotBeNull();
            TimeProvider.Current.Should().BeEquivalentTo(providerMock.Object);
        }

        [Fact]
        public void GetUnixEpochShouldReturnEpochValue()
        {
            // Arrange
            var expected = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

            // Act
            var actual = new TimeProviderAccessor().UnixEpoch;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinDateValueShouldReturnFixedDate()
        {
            // Arrange
            var expected = new DateTime(1900, 1, 1, 0, 0, 0);

            // Act
            var actual = new TimeProviderAccessor().MinDateTime;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void MaxDateValueShouldReturnFixedDate()
        {
            // Arrange
            var expected = new DateTime(9999, 12, 31, 0, 0, 0);

            // Act
            var actual = new TimeProviderAccessor().MaxDateTime;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinDateValueWithOffsetShouldReturnFixedDate()
        {
            // Arrange
            var expected = new DateTimeOffset(1900, 1, 1, 0, 0, 0,TimeSpan.Zero);

            // Act
            var actual = new TimeProviderAccessor().MinDateTimeWithOffset;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void MaxDateValueWithOffsetShouldReturnFixedDate()
        {
            // Arrange
            var expected = new DateTimeOffset(9999, 12, 31, 0, 0, 0, TimeSpan.Zero);

            // Act
            var actual = new TimeProviderAccessor().MaxDateTimeWithOffset;

            // Assert
            actual.Should().Be(expected);
        }
    }
}