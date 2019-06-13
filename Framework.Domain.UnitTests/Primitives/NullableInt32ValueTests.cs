#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableInt32ValueTests
    {
        #region Methods

        public static NullableInt32Value GetInstance(int? value)
        {
            return new NullableInt32ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in Int32ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(int? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableInt32ValueAccessor : NullableInt32Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableInt32ValueAccessor(int? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}