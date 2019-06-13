#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableUInt16ValueTests
    {
        #region Methods

        public static NullableUInt16Value GetInstance(ushort? value)
        {
            return new NullableUInt16ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in UInt16ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(ushort? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableUInt16ValueAccessor : NullableUInt16Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableUInt16ValueAccessor(ushort? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}