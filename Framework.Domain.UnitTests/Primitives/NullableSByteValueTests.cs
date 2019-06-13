#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableSByteValueTests
    {
        #region Methods

        public static NullableSByteValue GetInstance(sbyte? value)
        {
            return new NullableSByteValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in SByteValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(sbyte? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableSByteValueAccessor : NullableSByteValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableSByteValueAccessor(sbyte? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}