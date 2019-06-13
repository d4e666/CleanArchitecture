#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class NullableGuidValueTests
    {
        #region Methods

        public static NullableGuidValue GetInstance(Guid? value)
        {
            return new NullableGuidValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            yield return new object[]
                         {
                             null
                         };

            foreach (var values in GuidValueTests.ConstructorTestData())
                yield return values;
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(Guid? value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        #region Children

        private class NullableGuidValueAccessor : NullableGuidValue
        {
            #region Constructors

            /// <inheritdoc />
            public NullableGuidValueAccessor(Guid? value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}