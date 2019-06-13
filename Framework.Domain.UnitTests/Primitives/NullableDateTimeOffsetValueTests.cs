#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableDateTimeOffsetValueTests
    {
        #region Methods

        public static NullableDateTimeOffsetValue GetInstance(DateTimeOffset? value)
        {
            return new NullableDateTimeOffsetValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in DateTimeOffsetValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(DateTimeOffset? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableDateTimeOffsetValueAccessor : NullableDateTimeOffsetValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableDateTimeOffsetValueAccessor(DateTimeOffset? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}