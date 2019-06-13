#region Usings

using FluentAssertions;
using Framework.Domain.Services.Identity;
using System;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Services.Identity
{
    public class IdentityProviderTests
    {

        private static IdentityProvider<int> GetInstance(int value)
        {
            return new IdentityProviderAccessor(value);
        }

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(100);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #region Children

        public class IdentityProviderAccessor : IdentityProvider<int>
        {
            #region Fields

            private readonly int _value;

            #endregion

            #region Constructors

            public IdentityProviderAccessor(int value)
            {
                this._value = value;
            }

            #endregion

            #region Methods

            /// <inheritdoc />
            public override int Create()
            {
                return this._value;
            }

            #endregion
        }

        #endregion
    }

    public class FixedIdentityProviderTests
    {
        private static FixedIdentityProvider<T> GetInstance<T>(T value)
        {
            return new FixedIdentityProvider<T>(value);
        }

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(100);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }


        [Fact]
        public void GetValueShouldReturnSetValue()
        {
            // Arrange
            var value = 100;
            var instance = GetInstance(100);

            // Act
            var actual = instance.Create();

            // Assert
            actual.Should().Be(value);
        }

    }

    public class GuidIdentityProviderTests
    {
        private static GuidIdentityProvider GetInstance()
        {
            return new GuidIdentityProvider();
        }

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance();

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }


        [Fact]
        public void GetValueShouldReturnDifferentValueOnEachCall()
        {
            // Arrange
            var instance = GetInstance();

            // Act
            var actual1 = instance.Create();
            var actual2 = instance.Create();
            
            // Assert
            actual2.Should().NotBe(actual1);
        }
    }
}