#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableSingleValueTests
    {
        #region Methods

        public static NullableSingleValue GetInstance(float? value)
        {
            return new NullableSingleValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in SingleValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(float? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableSingleValueAccessor : NullableSingleValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableSingleValueAccessor(float? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}