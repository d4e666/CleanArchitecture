#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class ByteValueTests
    {
        #region Methods

        public static ByteValue GetInstance(byte value)
        {
            return new ByteValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (byte) 0
                         };

            yield return new object[]
                         {
                             (byte) 100
                         };
            yield return new object[]
                         {
                             byte.MinValue
                         };
            yield return new object[]
                         {
                             byte.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(byte value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class ByteValueAccessor : ByteValue
        {
            #region Constructors

            /// <inheritdoc />
            public ByteValueAccessor(byte value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}