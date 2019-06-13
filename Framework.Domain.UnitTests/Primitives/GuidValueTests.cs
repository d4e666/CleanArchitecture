#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class GuidValueTests
    {
        #region Methods

        public static GuidValue GetInstance(Guid value)
        {
            return new GuidValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             Guid.Empty
                         };
            yield return new object[]
                         {
                             Guid.NewGuid()
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(Guid value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class GuidValueAccessor : GuidValue
        {
            #region Constructors

            /// <inheritdoc />
            public GuidValueAccessor(Guid value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}