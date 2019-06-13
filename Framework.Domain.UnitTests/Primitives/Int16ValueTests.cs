#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class Int16ValueTests
    {
        #region Methods

        public static Int16Value GetInstance(short value)
        {
            return new Int16ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (short) 0
                         };

            yield return new object[]
                         {
                             (short) 100
                         };
            yield return new object[]
                         {
                             short.MinValue
                         };
            yield return new object[]
                         {
                             short.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(short value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class Int16ValueAccessor : Int16Value
        {
            #region Constructors

            /// <inheritdoc />
            public Int16ValueAccessor(short value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}