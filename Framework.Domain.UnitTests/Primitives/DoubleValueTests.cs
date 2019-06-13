#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class DoubleValueTests
    {
        #region Methods

        public static DoubleValue GetInstance(double value)
        {
            return new DoubleValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (double) 0
                         };

            yield return new object[]
                         {
                             (double) 100
                         };
            yield return new object[]
                         {
                             double.MinValue
                         };
            yield return new object[]
                         {
                             double.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(double value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class DoubleValueAccessor : DoubleValue
        {
            #region Constructors

            /// <inheritdoc />
            public DoubleValueAccessor(double value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}