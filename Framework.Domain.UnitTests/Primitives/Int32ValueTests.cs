#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class Int32ValueTests
    {
        #region Methods

        public static Int32Value GetInstance(int value)
        {
            return new Int32ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             0
                         };

            yield return new object[]
                         {
                             100
                         };
            yield return new object[]
                         {
                             int.MinValue
                         };
            yield return new object[]
                         {
                             int.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(int value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class Int32ValueAccessor : Int32Value
        {
            #region Constructors

            /// <inheritdoc />
            public Int32ValueAccessor(int value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}