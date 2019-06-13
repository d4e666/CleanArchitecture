#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class StringValueTests
    {
        #region Methods

        public static StringValue GetInstance(string value)
        {
            return new StringValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            yield return new object[]
                         {
                             string.Empty
                         };
            yield return new object[]
                         {
                             "SomeValue"
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(string value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class StringValueAccessor : StringValue
        {
            #region Constructors

            /// <inheritdoc />
            public StringValueAccessor(string value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}