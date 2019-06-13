#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableInt64ValueTests
    {
        #region Methods

        public static NullableInt64Value GetInstance(long? value)
        {
            return new NullableInt64ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in Int64ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(long? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableInt64ValueAccessor : NullableInt64Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableInt64ValueAccessor(long? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}