#region Usings

using FluentAssertions;
using Framework.Domain.Services.Culture;
using Moq;
using System;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Services.Culture
{
    public class CultureProviderTests
    {
        [Fact]
        public void SetCurrentShouldNotAllowNull()
        {
            // Arrange

            // Act
            Action methodUnderTest = () => CultureProvider.SetCurrent(null);

            // Assert
            methodUnderTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetCurrentShouldSetCurrentProperty()
        {
            // Arrange
            var providerMock = new Mock<ICultureProvider>();

            // Act
            CultureProvider.SetCurrent(providerMock?.Object);

            // Assert
            CultureProvider.Current.Should().NotBeNull();
            CultureProvider.Current.Should().BeEquivalentTo(providerMock.Object);
        }
    }
}