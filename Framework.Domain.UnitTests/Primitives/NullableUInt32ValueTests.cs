#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableUInt32ValueTests
    {
        #region Methods

        public static NullableUInt32Value GetInstance(uint? value)
        {
            return new NullableUInt32ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in UInt32ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(uint? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableUInt32ValueAccessor : NullableUInt32Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableUInt32ValueAccessor(uint? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}