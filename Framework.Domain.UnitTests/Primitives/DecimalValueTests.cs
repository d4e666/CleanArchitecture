#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class DecimalValueTests
    {
        #region Methods

        public static DecimalValue GetInstance(decimal value)
        {
            return new DecimalValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (decimal) 0
                         };

            yield return new object[]
                         {
                             (decimal) 100
                         };
            yield return new object[]
                         {
                             decimal.MinValue
                         };
            yield return new object[]
                         {
                             decimal.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(decimal value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class DecimalValueAccessor : DecimalValue
        {
            #region Constructors

            /// <inheritdoc />
            public DecimalValueAccessor(decimal value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}