#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class BooleanValueTests
    {
        #region Methods

        public static BooleanValue GetInstance(bool value)
        {
            return new BooleanValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             false
                         };
            yield return new object[]
                         {
                             true
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(bool value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class BooleanValueAccessor : BooleanValue
        {
            #region Constructors

            /// <inheritdoc />
            public BooleanValueAccessor(bool value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}