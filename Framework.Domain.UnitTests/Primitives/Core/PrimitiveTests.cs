#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Primitives.Core;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Primitives.Core
{
    public class PrimitiveTests
    {
        private class PrimitiveAccessor : Primitive<int>
        {
            #region Constructors

            /// <inheritdoc />
            public PrimitiveAccessor(int value) : base(value)
            {
            }

            #endregion
        }

        private class OtherPrimitiveAccessor : Primitive<int>
        {
            #region Constructors

            /// <inheritdoc />
            public OtherPrimitiveAccessor(int value) : base(value)
            {
            }

            #endregion
        }

        private class DerivedPrimitiveAccessor : PrimitiveAccessor
        {
            #region Constructors

            /// <inheritdoc />
            public DerivedPrimitiveAccessor(int value) : base(value)
            {
            }

            #endregion
        }

        private static Primitive<int> GetInstance(int value)
        {
            return new PrimitiveAccessor(value);
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsObjectMethodTheory(Primitive<int> instance1, object instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsClassMethodTheory(Primitive<int> instance1, Primitive<int> instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualityOperatorData))]
        public void EqualsOperatorTheory(Primitive<int> instance1, object instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = false;
            if (instance2 == null)
                actual = instance1 == (Primitive<int>) instance2;
            if (instance2 is Primitive<int> otherPrimitive)
                actual = instance1 == otherPrimitive;
            else if (instance2 is int otherPrimitiveValue)
                actual = instance1 == otherPrimitiveValue;

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(InequalityOperatorData))]
        public void InequalsOperatorTheory(Primitive<int> instance1, object instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = false;
            if (instance2 == null)
                actual = instance1 != (Primitive<int>) instance2;
            if (instance2 is Primitive<int> otherPrimitive)
                actual = instance1 != otherPrimitive;
            else if (instance2 is int otherPrimitiveValue)
                actual = instance1 != otherPrimitiveValue;

            // Assert
            actual.Should().Be(expected, because);
        }

        public static IEnumerable<object[]> InequalityOperatorData()
        {
            foreach (var values in EqualityOperatorData())
            {
                values[2] = !(bool) values[2];

                yield return values;
            }
        }

        public static IEnumerable<object[]> EqualityOperatorData()
        {
            var value1 = 100;
            var value2 = 10;
            var instance = GetInstance(value1);

            yield return new object[]
                         {
                             null,
                             null,
                             true,
                             "null == null always evaluates true"
                         };

            yield return new object[]
                         {
                             instance,
                             null,
                             false,
                             "null == non-null always evaluates false"
                         };
            yield return new object[]
                         {
                             null,
                             instance,
                             false,
                             "non-null == null always evaluates false"
                         };
            yield return new object[]
                         {
                             instance,
                             value1,
                             true,
                             "Equal value"
                         };
            yield return new object[]
                         {
                             instance,
                             value2,
                             false,
                             "Different value"
                         };
        }

        public static IEnumerable<object[]> EqualsMethodData()
        {
            var property1 = 10;
            var property2 = 100;

            var referenceInstance = GetInstance(property1);
            var instance1 = GetInstance(property1);
            var instance2 = GetInstance(property2);
            var otherInstance = new OtherPrimitiveAccessor(property1);
            var derivedInstance = new DerivedPrimitiveAccessor(property1);

            yield return new object[]
                         {
                             referenceInstance,
                             null,
                             false,
                             "Equals null is always false"
                         };

            yield return new object[]
                         {
                             referenceInstance,
                             referenceInstance,
                             true,
                             "Same instance is always true"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             otherInstance,
                             false,
                             "Different type"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             derivedInstance,
                             false,
                             "Derived type"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             instance2,
                             false,
                             "Same type, different values"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             instance1,
                             true,
                             "Same type, same value"
                         };
        }

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(100);

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        [Fact]
        public void ImplicitOperatorShouldReturnUnderlyingValue()
        {
            // Arrange
            var expected = 100;
            var instance = GetInstance(expected);

            // Act
            var actual = (int) instance;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void GetHashCodeShouldReturnValueHashCode()
        {
            // Arrange
            var value = 100;
            var expected = value.GetHashCode();
            var instance = GetInstance(value);

            // Act
            var actual = instance.GetHashCode();

            // Assert
            actual.Should().Be(expected, $"{nameof(this.GetHashCode)} from value is to be used.");
        }

        [Fact]
        public void ToStringShouldReturnValueToStringForNonEmptyValue()
        {
            // Arrange
            var value = 100;
            var expected = value.ToString();
            var instance = GetInstance(value);

            // Act
            var actual = instance.ToString();

            // Assert
            actual.Should().Be(expected, $"{nameof(this.ToString)} from value is to be used.");
        }

        [Fact]
        public void EqualsOperatorWithNullShouldReturnFalse()
        {
            // Arrange
            var value = 100;
            var instance = GetInstance(value);
            var expected = false;

            // Act
            var actual = instance == null;

            // Assert
            actual.Should().Be(expected);
        }
    }
}