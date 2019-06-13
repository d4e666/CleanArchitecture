#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableDateTimeValueTests
    {
        #region Methods

        public static NullableDateTimeValue GetInstance(DateTime? value)
        {
            return new NullableDateTimeValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in DateTimeValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(DateTime? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableDateTimeValueAccessor : NullableDateTimeValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableDateTimeValueAccessor(DateTime? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}