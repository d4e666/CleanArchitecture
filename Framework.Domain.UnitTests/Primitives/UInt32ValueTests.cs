#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class UInt32ValueTests
    {
        #region Methods

        public static UInt32Value GetInstance(uint value)
        {
            return new UInt32ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (uint) 0
                         };

            yield return new object[]
                         {
                             (uint) 100
                         };
            yield return new object[]
                         {
                             uint.MinValue
                         };
            yield return new object[]
                         {
                             uint.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(uint value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class UInt32ValueAccessor : UInt32Value
        {
            #region Constructors

            /// <inheritdoc />
            public UInt32ValueAccessor(uint value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}