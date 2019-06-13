#region Usings

using FluentAssertions;
using Framework.Domain.Services.Culture;
using System;
using System.Globalization;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Services.Culture
{
    public class FixedCultureProviderTests
    {
        private static ICultureProvider GetInstance(CultureInfo culture, CultureInfo uiCulture)
        {
            return new FixedCultureProvider(culture, uiCulture);
        }

        #region Constructor tests

        [Fact]
        public void ConstructorWithMissingCultureShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(null, null);

            // Assert
            constructorUnderTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConstructorWithMissingUICultureShouldThrowArgumentNullException()
        {
            // Arrange
            var culture = CultureInfo.CurrentCulture;

            // Act
            Action constructorUnderTest = () => GetInstance(culture, null);

            // Assert
            constructorUnderTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConstructorWithoutMissingDependenciesShouldNotThrowException()
        {
            // Arrange
            var culture = CultureInfo.CurrentCulture;
            var uiCulture = CultureInfo.CurrentUICulture;

            // Act
            Action constructorUnderTest = () => GetInstance(culture, uiCulture);

            // Assert
            constructorUnderTest.Should().NotThrow();
        }
        #endregion

        [Fact]
        public void GetCurrentCultureShouldReturnSetCulture()
        {
            // Arrange
            var culture = new CultureInfo("aa-AA");
            var uiCulture = new CultureInfo("bb-BB");
            var instance = GetInstance(culture, uiCulture);

            // Act
            var actual = instance.GetCurrentCulture();

            // Assert
            actual.Should().Be(culture);
        }

        [Fact]
        public void GetCurrentUiCultureShouldReturnSetUiCulture()
        {
            // Arrange
            var culture = new CultureInfo("aa-AA");
            var uiCulture = new CultureInfo("bb-BB");
            var instance = GetInstance(culture, uiCulture);

            // Act
            var actual = instance.GetCurrentUiCulture();

            // Assert
            actual.Should().Be(uiCulture);
        }
    }
}