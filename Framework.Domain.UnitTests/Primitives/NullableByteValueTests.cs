#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableByteValueTests
    {
        #region Methods

        public static NullableByteValue GetInstance(byte? value)
        {
            return new NullableByteValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in ByteValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(byte? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableByteValueAccessor : NullableByteValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableByteValueAccessor(byte? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}