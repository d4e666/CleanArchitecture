#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives
{
    public class CaseInsensitiveStringValueTests
    {
        #region Methods

        public static CaseInsensitiveStringValue GetInstance(string value)
        {
            return new CaseInsensitiveStringValueAccessor(value);
        }

        public static IEnumerable<object[]> ConstructorTestData()
        {
            foreach (var values in StringValueTests.ConstructorTestData())
                yield return values;
        }

        public static IEnumerable<object[]> EqualsClassMethodData()
        {
            var value = "TestValue";
            var instance = GetInstance(value);

            yield return new object[]
                         {
                             instance,
                             GetInstance(value),
                             true,
                             "case insensitive string compare is used"
                         };

            yield return new object[]
                         {
                             instance,
                             GetInstance(value.ToLowerInvariant()),
                             true,
                             "case insensitive string compare is used"
                         };
            yield return new object[]
                         {
                             instance,
                             GetInstance(value.ToUpperInvariant()),
                             true,
                             "case insensitive string compare is used"
                         };
        }

        [Theory]
        [MemberData(nameof(ConstructorTestData))]
        public void ConstructorShouldNotThrowException(string value)
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(value);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        [Theory]
        [MemberData(nameof(EqualsClassMethodData))]
        public void EqualsMethodShouldCompareCaseInsensitive(CaseInsensitiveStringValueAccessor instance1, CaseInsensitiveStringValueAccessor instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        #endregion

        #region Children

        public class CaseInsensitiveStringValueAccessor : CaseInsensitiveStringValue
        {
            #region Constructors

            /// <inheritdoc />
            public CaseInsensitiveStringValueAccessor(string value) : base(value)
            {
            }

            #endregion
        }

        #endregion
    }
}