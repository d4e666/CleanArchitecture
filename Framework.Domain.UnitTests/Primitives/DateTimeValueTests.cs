#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class DateTimeValueTests
    {
        #region Methods

        public static DateTimeValue GetInstance(DateTime value)
        {
            return new DateTimeValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             DateTime.Now
                         };

            yield return new object[]
                         {
                             DateTime.MinValue
                         };
            yield return new object[]
                         {
                             DateTime.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(DateTime value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class DateTimeValueAccessor : DateTimeValue
        {
            #region Constructors

            /// <inheritdoc />
            public DateTimeValueAccessor(DateTime value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}