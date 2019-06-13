#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableInt16ValueTests
    {
        #region Methods

        public static NullableInt16Value GetInstance(short? value)
        {
            return new NullableInt16ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in Int16ValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(short? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableInt16ValueAccessor : NullableInt16Value
        {
            #region Constructors

            /// <inheritdoc />
            public NullableInt16ValueAccessor(short? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}