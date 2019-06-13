#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableUInt64ValueTests
    {
        #region Methods

        public static NullableUInt64Value GetInstance(ulong? value)
        {
            return new NullableUInt64ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in UInt64ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(ulong? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableUInt64ValueAccessor : NullableUInt64Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableUInt64ValueAccessor(ulong? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}