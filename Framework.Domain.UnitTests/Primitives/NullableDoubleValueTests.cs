#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableDoubleValueTests
    {
        #region Methods

        public static NullableDoubleValue GetInstance(double? value)
        {
            return new NullableDoubleValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in DoubleValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(double? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableDoubleValueAccessor : NullableDoubleValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableDoubleValueAccessor(double? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}