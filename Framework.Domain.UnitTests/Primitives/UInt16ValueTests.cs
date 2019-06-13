#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class UInt16ValueTests
    {
        #region Methods

        public static UInt16Value GetInstance(ushort value)
        {
            return new UInt16ValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (ushort) 0
                         };

            yield return new object[]
                         {
                             (ushort) 100
                         };
            yield return new object[]
                         {
                             ushort.MinValue
                         };
            yield return new object[]
                         {
                             ushort.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(ushort value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class UInt16ValueAccessor : UInt16Value
        {
            #region Constructors

            /// <inheritdoc />
            public UInt16ValueAccessor(ushort value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}