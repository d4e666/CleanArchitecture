#region Usings

using System;
using FluentAssertions;
using Framework.Domain.Entities;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Entities
{
    public class AggregateRootTests
    {
        private class AggregateRootAccessor : AggregateRoot
        {
            #region Constructors

            /// <inheritdoc />
            public AggregateRootAccessor(Guid key) : base(key)
            {
            }

            #endregion
        }

        private static AggregateRoot GetInstance(Guid key)
        {
            return new AggregateRootAccessor(key);
        }

        #region ConstructorTests

        [Fact]
        public void ConstructorWithEmptyKeyShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(Guid.Empty);

            // Assert
            constructorUnderTest.Should().NotThrow("No constructor logic");
        }

        [Fact]
        public void ConstructorWithNonEmptyKeyShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(Guid.NewGuid());

            // Assert
            constructorUnderTest.Should().NotThrow("No constructor logic");
        }

        [Fact]
        public void ConstructorShouldSetKeyProperty()
        {
            // Arrange
            var expected = Guid.NewGuid();
            var instance = GetInstance(expected);

            // Act
            var actual = instance.Key;

            // Assert
            actual.Should().Be(expected, "value is passed in constructor");
        }

        #endregion
    }
}