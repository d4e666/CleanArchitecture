#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class Int64ValueTests
    {
        #region Methods

        public static Int64Value GetInstance(long value)
        {
            return new Int64ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (long) 0
                         };

            yield return new object[]
                         {
                             (long) 100
                         };
            yield return new object[]
                         {
                             long.MinValue
                         };
            yield return new object[]
                         {
                             long.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(long value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class Int64ValueAccessor : Int64Value
        {
            #region Constructors

            /// <inheritdoc />
            public Int64ValueAccessor(long value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}