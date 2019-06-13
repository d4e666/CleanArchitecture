#region Usings

using System;
using System.Collections.Generic;
using FluentAssertions;
using Framework.Domain.Entities;
using Xunit;
using HashCode = Framework.Domain.Entities.HashCode;

#endregion

namespace Framework.Domain.UnitTests.Entities
{
    public class ValueObjectTests
    {
        private class ValueObjectAccessor : ValueObject
        {
            #region Properties

            public int SomeProperty { get; set; }

            public string SomeOtherProperty { get; set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return this.SomeProperty;
                yield return this.SomeOtherProperty;
            }

            #endregion
        }

        private class OtherValueObjectAccessor : ValueObject
        {
            #region Methods

            /// <inheritdoc />
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return null;
            }

            #endregion
        }

        private class DerivedValueObjectAccessor : ValueObjectAccessor
        {
            #region Properties

            private bool DerivedProperty { get; set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            protected override IEnumerable<object> GetEqualityComponents()
            {
                foreach (var value in base.GetEqualityComponents())
                    yield return value;

                yield return this.DerivedProperty;
            }

            #endregion
        }

        private static ValueObject GetInstance(int? propertyValue = null, string otherPropertyValue = null)
        {
            var instance = new ValueObjectAccessor();
            if (propertyValue.HasValue)
                instance.SomeProperty = propertyValue.Value;
            if (otherPropertyValue != null)
                instance.SomeOtherProperty = otherPropertyValue;

            return instance;
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsObjectMethodTheory(ValueObject instance1, object instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsClassMethodTheory(ValueObject instance1, ValueObject instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualityOperatorData))]
        public void EqualsOperatorTheory(ValueObject instance1, ValueObject instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(InequalityOperatorData))]
        public void InequalsOperatorTheory(ValueObject instance1, ValueObject instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1 != instance2;

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
            var instance = GetInstance();

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
        }

        public static IEnumerable<object[]> EqualsMethodData()
        {
            var property1 = 10;
            var property2 = 100;
            var otherProperty1 = "test";
            var otherProperty2 = "other test";

            var referenceInstance = GetInstance(property1, otherProperty1);
            var instance1 = GetInstance(property1, otherProperty1);
            var instance2 = GetInstance(property2, otherProperty1);
            var instance3 = GetInstance(property1, otherProperty2);
            var otherInstance = new OtherValueObjectAccessor();
            var derivedInstance = new DerivedValueObjectAccessor();

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
                             instance3,
                             false,
                             "Same type, different values"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             instance1,
                             true,
                             "Same type, same values"
                         };
        }

        #region Constructor tests

        [Fact]
        public void ConstructorShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance();

            // Assert
            constructorUnderTest.Should().NotThrow("no constructor logic");
        }

        #endregion

        [Fact]
        public void GetHashCodeShouldReturnCombinedPropertyHashCode()
        {
            // Arrange
            var property = 100;
            var otherProperty = "test";
            var hash = new HashCode();
            hash.Add(property);
            hash.Add(otherProperty);
            var expected = hash.ToHashCode();
            var instance = GetInstance(property, otherProperty);

            // Act
            var actual = instance.GetHashCode();

            // Assert
            actual.Should().Be(expected);
        }
    }
}