#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Framework.Domain.Entities;
using Xunit;

#endregion

namespace Framework.Domain.UnitTests.Entities
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class EntityTests
    {
        private static Entity GetInstance(Guid key)
        {
            return new EntityAccessor(key);
        }

        private class EntityAccessor : Entity
        {
            #region Constructors

            /// <inheritdoc />
            public EntityAccessor(Guid key) : base(key)
            {
            }

            #endregion
        }

        private class OtherEntityAccessor : Entity
        {
            #region Constructors

            /// <inheritdoc />
            public OtherEntityAccessor(Guid key) : base(key)
            {
            }

            #endregion
        }

        private class DerivedEntityAccessor : EntityAccessor
        {
            #region Constructors

            /// <inheritdoc />
            public DerivedEntityAccessor(Guid key) : base(key)
            {
            }

            #endregion
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsObjectMethodTheory(Entity instance1, object instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualsMethodData))]
        public void EqualsClassMethodTheory(Entity instance1, Entity instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1.Equals(instance2);

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(EqualityOperatorData))]
        public void EqualsOperatorTheory(Entity instance1, Entity instance2, bool expected, string because)
        {
            // Arrange

            // Act
            var actual = instance1 == instance2;

            // Assert
            actual.Should().Be(expected, because);
        }

        [Theory]
        [MemberData(nameof(InequalityOperatorData))]
        public void InequalsOperatorTheory(Entity instance1, Entity instance2, bool expected, string because)
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
            var instance = GetInstance(Guid.NewGuid());

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
            var key1 = Guid.NewGuid();
            var key2 = Guid.NewGuid();
            var referenceInstance = GetInstance(key1);
            var instance1 = GetInstance(key1);
            var instance2 = GetInstance(key2);
            var otherInstance = new OtherEntityAccessor(key1);
            var derivedInstance = new DerivedEntityAccessor(key1);

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
                             "Different type, same key"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             derivedInstance,
                             false,
                             "Derived type, same key"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             instance2,
                             false,
                             "Same type, different key"
                         };
            yield return new object[]
                         {
                             referenceInstance,
                             instance1,
                             true,
                             "Same type, same key"
                         };
        }

        [Fact]
        public void GetHashCodeShouldReturnKeyHashCode()
        {
            // Arrange
            var key = Guid.NewGuid();
            var instance = GetInstance(key);
            var expected = key.GetHashCode();

            // Act
            var actual = instance.GetHashCode();

            // Assert
            actual.Should().Be(expected);
        }

        #region ConstructorTests

        [Fact]
        public void ConstructorWithEmptyKeyShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(Guid.Empty);

            // Assert
            constructorUnderTest.Should().NotThrow("No constructor logic");
        }

        [Fact]
        public void ConstructorWithNonEmptyKeyShouldNotThrowException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => GetInstance(Guid.NewGuid());

            // Assert
            constructorUnderTest.Should().NotThrow("No constructor logic");
        }

        [Fact]
        public void ConstructorShouldSetKeyProperty()
        {
            // Arrange
            var expected = Guid.NewGuid();
            var instance = GetInstance(expected);

            // Act
            var actual = instance.Key;

            // Assert
            actual.Should().Be(expected, "value is passed in constructor");
        }

        #endregion
    }
}