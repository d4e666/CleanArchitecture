#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableBooleanValueTests
    {
        #region Methods

        public static NullableBooleanValue GetInstance(bool value)
        {
            return new NullableBooleanValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in BooleanValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(bool value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableBooleanValueAccessor : NullableBooleanValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableBooleanValueAccessor(bool? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}