#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableDecimalValueTests
    {
        #region Methods

        public static NullableDecimalValue GetInstance(decimal? value)
        {
            return new NullableDecimalValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in DecimalValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(decimal? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableDecimalValueAccessor : NullableDecimalValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableDecimalValueAccessor(decimal? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}