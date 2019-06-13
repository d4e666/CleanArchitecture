#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class DateTimeOffsetValueTests
    {
        #region Methods

        public static DateTimeOffsetValue GetInstance(DateTimeOffset value)
        {
            return new DateTimeOffsetValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             DateTimeOffset.Now
                         };
            yield return new object[]
                         {
                             DateTimeOffset.MinValue
                         };
            yield return new object[]
                         {
                             DateTimeOffset.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(DateTimeOffset value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class DateTimeOffsetValueAccessor : DateTimeOffsetValue
        {
            #region Constructors

            /// <inheritdoc />
            public DateTimeOffsetValueAccessor(DateTimeOffset value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}