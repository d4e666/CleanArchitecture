#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class UInt64ValueTests
    {
        #region Methods

        public static UInt64Value GetInstance(ulong value)
        {
            return new UInt64ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (ulong) 0
                         };

            yield return new object[]
                         {
                             (ulong) 100
                         };
            yield return new object[]
                         {
                             ulong.MinValue
                         };
            yield return new object[]
                         {
                             ulong.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(ulong value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class UInt64ValueAccessor : UInt64Value
        {
            #region Constructors

            /// <inheritdoc />
            public UInt64ValueAccessor(ulong value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}