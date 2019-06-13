using FluentAssertions;
using Framework.Domain.Services.Culture;
using System;
using System.Globalization;
using Xunit;

namespace Framework.Domain.UnitTests.Services.Culture
{
    public class SystemCultureProviderTests
    {
        private static ICultureProvider GetInstance()
        {
            return new SystemCultureProvider();
        }

        [Fact]
        public void ConstructorWithoutMissingDependenciesShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance();

            // Assert
            constructorUnderTest.Should().NotThrow();
        }

        [Fact]
        public void GetCurrentCultureShouldReturnSetCulture()
        {
            // Arrange
            var expected = CultureInfo.CurrentCulture;
            var instance = GetInstance();

            // Act
            var actual = instance.GetCurrentCulture();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetCurrentUiCultureShouldReturnSetUiCulture()
        {
            // Arrange
            var expected = CultureInfo.CurrentUICulture;
            var instance = GetInstance();

            // Act
            var actual = instance.GetCurrentUiCulture();

            // Assert
            actual.Should().Be(expected);
        }
    }
}