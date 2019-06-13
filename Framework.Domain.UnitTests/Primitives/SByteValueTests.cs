#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class SByteValueTests
    {
        #region Methods

        public static SByteValue GetInstance(sbyte value)
        {
            return new SByteValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (sbyte) 0
                         };

            yield return new object[]
                         {
                             (sbyte) 100
                         };
            yield return new object[]
                         {
                             sbyte.MinValue
                         };
            yield return new object[]
                         {
                             sbyte.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(sbyte value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class SByteValueAccessor : SByteValue
        {
            #region Constructors

            /// <inheritdoc />
            public SByteValueAccessor(sbyte value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}