#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class SingleValueTests
    {
        #region Methods

        public static SingleValue GetInstance(float value)
        {
            return new SingleValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             (float) 0
                         };

            yield return new object[]
                         {
                             (float) 100
                         };
            yield return new object[]
                         {
                             float.MinValue
                         };
            yield return new object[]
                         {
                             float.MaxValue
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(float value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class SingleValueAccessor : SingleValue
        {
            #region Constructors

            /// <inheritdoc />
            public SingleValueAccessor(float value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}