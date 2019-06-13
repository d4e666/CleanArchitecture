#region Usings

using System;
using System.Linq;
using FluentAssertions;
using Framework.Adapters.Persistence.Database.NetStandard.EditTracking;
using Framework.Adapters.Persistence.Database.NetStandard.Temporal;
using Framework.Domain.Services.Time;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

#endregion

namespace Framework.Adapters.EntityFrameworkCore.UnitTests
{
    public class DatabaseContextTests
    {
        private const string FixedUser = "Unknown";
        private static readonly DateTimeOffset FixedMoment1 = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private const string OtherFixedUser = "Other user";
        private static readonly DateTimeOffset FixedMoment2 = new DateTimeOffset(2001, 12, 31, 0, 0, 0, TimeSpan.Zero);
        private static readonly DateTimeOffset MaxMoment = new DateTimeOffset(9999, 12, 31, 0, 0, 0, TimeSpan.Zero);

        private abstract class AbstractEntity
        {
            #region Properties

            public long Id { get; set; }

            #endregion
        }

        private class EntityWithoutTracking : AbstractEntity
        {
        }

        private class EntityWithCreatedTracking : AbstractEntity, ITrackCreated, ITrackCreatedAdapter
        {
            #region Properties

            /// <inheritdoc />
            public string CreatedBy { get; set; }

            /// <inheritdoc />
            public DateTimeOffset CreatedOn { get; set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            public void SetCreated(string createdBy, DateTimeOffset createdOn)
            {
                this.CreatedBy = createdBy;
                this.CreatedOn = createdOn;
            }

            #endregion
        }

        private class EntityWithLastModifiedTracking : AbstractEntity, ITrackLastModified, ITrackLastModifiedAdapter
        {
            #region Properties

            /// <inheritdoc />
            public string LastModifiedBy { get; private set; }

            /// <inheritdoc />
            public DateTimeOffset LastModifiedOn { get; private set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            public void SetLastModified(string lastModifiedBy, DateTimeOffset lastModifiedOn)
            {
                this.LastModifiedBy = lastModifiedBy;
                this.LastModifiedOn = lastModifiedOn;
            }

            #endregion
        }

        private class EntityWithDeletedTracking : AbstractEntity, ITrackDeleted, ITrackDeletedAdapter
        {
            #region Properties

            /// <inheritdoc />
            public string DeletedBy { get; private set; }

            /// <inheritdoc />
            public DateTimeOffset? DeletedOn { get; private set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            public void SetDeleted(string deletedBy, DateTimeOffset deletedOn)
            {
                this.DeletedBy = deletedBy;
                this.DeletedOn = deletedOn;
            }

            #endregion
        }

        private class TemporalEntityAccessor : ITechnicalTemporal, ITechnicalTemporalAdapter
        {
            #region Properties

            public long Id { get; set; }

            /// <inheritdoc />
            public string TechnicalValidBy { get; set; }

            /// <inheritdoc />
            public DateTimeOffset TechnicalValidFrom { get; set; }

            /// <inheritdoc />
            public string TechnicalVoidBy { get; set; }

            /// <inheritdoc />
            public DateTimeOffset TechnicalValidTo { get; set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            public object Clone()
            {
                return new TemporalEntityAccessor
                {
                    Id = this.Id,
                    TechnicalValidBy = this.TechnicalValidBy,
                    TechnicalValidFrom = this.TechnicalValidFrom,
                    TechnicalValidTo = this.TechnicalValidTo
                };
            }

            /// <inheritdoc />
            public void SetTechnicalValidBy(string user)
            {
                this.TechnicalValidBy = user;
            }

            /// <inheritdoc />
            public void SetTechnicalValidFrom(DateTimeOffset moment)
            {
                this.TechnicalValidFrom = moment;
            }

            /// <inheritdoc />
            public void SetTechnicalVoidBy(string user)
            {
                this.TechnicalVoidBy = user;
            }

            /// <inheritdoc />
            public void SetTechnicalValidTo(DateTimeOffset moment)
            {
                this.TechnicalValidTo = moment;
            }

            #endregion
        }

        private class DbContextAccessor : DatabaseContext
        {
            #region Constructors

            /// <inheritdoc />
            public DbContextAccessor(ITimeProvider timeProvider, DbContextOptions options) : base(timeProvider, options)
            {
            }

            #endregion

            #region Properties

            public DbSet<EntityWithoutTracking> EntitiesWithoutTracking { get; private set; }

            public DbSet<EntityWithCreatedTracking> EntitiesWithCreatedTracking { get; private set; }

            public DbSet<EntityWithLastModifiedTracking> EntitiesWithLastModifiedTracking { get; private set; }

            public DbSet<EntityWithDeletedTracking> EntitiesWithDeletedTracking { get; private set; }

            public DbSet<TemporalEntityAccessor> TemporalEntities { get; set; }

            #endregion

            #region Methods

            /// <inheritdoc />
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<AbstractEntity>()
                            .HasKey(e => e.Id);
                modelBuilder.Entity<TemporalEntityAccessor>()
                            .HasKey(
                                e => new
                                {
                                    e.Id,
                                    e.TechnicalValidFrom
                                });
            }

            #endregion
        }

        private static DbContextAccessor GetInstance()
        {
            var timeProviderMock = new Mock<ITimeProvider>();
            timeProviderMock.SetupSequence(mock => mock.GetLocalDateAndTimeWithOffset())
                            .Returns(FixedMoment1)
                            .Returns(FixedMoment2);
            timeProviderMock.Setup(mock => mock.MaxDateTimeWithOffset)
                            .Returns(MaxMoment);

            var dbOptions = new DbContextOptionsBuilder<DbContextAccessor>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;
            var context = new DbContextAccessor(timeProviderMock.Object, dbOptions);

            return context;
        }

        #region Add
        [Fact]
        public void SaveChangesForNonTrackedEntityShouldNotThrowExceptionOnAdd()
        {
            // Arrange
            var context = GetInstance();
            var nonTrackedEntity = new EntityWithoutTracking();

            // Act
            context.EntitiesWithoutTracking.Add(nonTrackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithoutTracking.Local.Count().Should().Be(1);
        }

        [Fact]
        public void SaveChangesForCreatedTrackedEntityShouldSetCreatedInfoOnAdd()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithCreatedTracking();

            // Act
            context.EntitiesWithCreatedTracking.Add(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithCreatedTracking.Local.Count().Should().Be(1);
            context.EntitiesWithCreatedTracking.Local.First().Should().BeSameAs(trackedEntity);
            trackedEntity.CreatedBy.Should().Be(FixedUser);
            trackedEntity.CreatedOn.Should().Be(FixedMoment1);
        }

        [Fact]
        public void SaveChangesForLastModifiedTrackedEntityShouldSetLastModifiedInfoOnAdd()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithLastModifiedTracking();

            // Act
            context.EntitiesWithLastModifiedTracking.Add(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithLastModifiedTracking.Local.Count().Should().Be(1);
            context.EntitiesWithLastModifiedTracking.Local.First().Should().BeSameAs(trackedEntity);
            trackedEntity.LastModifiedBy.Should().Be(FixedUser);
            trackedEntity.LastModifiedOn.Should().Be(FixedMoment1);
        }

        [Fact]
        public void SaveChangesForDeletedTrackedEntityShouldNotSetDeletedInfoOnAdd()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithDeletedTracking();

            // Act
            context.EntitiesWithDeletedTracking.Add(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithDeletedTracking.Local.Count().Should().Be(1);
            context.EntitiesWithDeletedTracking.Local.First().Should().BeSameAs(trackedEntity);
            trackedEntity.DeletedBy.Should().Be(null);
            trackedEntity.DeletedOn.Should().Be(null);
        }
        #endregion

        #region Update
        [Fact]
        public void SaveChangesForNonTrackedEntityShouldNotThrowExceptionOnUpdate()
        {
            // Arrange
            var context = GetInstance();
            var nonTrackedEntity = new EntityWithoutTracking();
            context.EntitiesWithoutTracking.Add(nonTrackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithoutTracking.Update(nonTrackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithoutTracking.Local.Count().Should().Be(1);
        }

        [Fact]
        public void SaveChangesForTrackCreatedEntityShouldNotChangeCreatedInfoOnUpdate()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithCreatedTracking();
            context.EntitiesWithCreatedTracking.Add(trackedEntity);
            context.SaveChanges();
            trackedEntity.CreatedBy = OtherFixedUser;

            // Act
            context.EntitiesWithCreatedTracking.Update(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithCreatedTracking.Local.Count().Should().Be(1);
            trackedEntity.CreatedBy.Should().Be(OtherFixedUser);
            trackedEntity.CreatedOn.Should().Be(FixedMoment1);
        }

        [Fact]
        public void SaveChangesForTrackLastModifiedEntityShouldChangeLastModifiedInfoOnUpdate()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithLastModifiedTracking();
            context.EntitiesWithLastModifiedTracking.Add(trackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithLastModifiedTracking.Update(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithLastModifiedTracking.Local.Count().Should().Be(1);
            trackedEntity.LastModifiedOn.Should().Be(FixedMoment2);
        }

        [Fact]
        public void SaveChangesForTrackDeletedEntityShouldNotSetDeletedInfoOnUpdate()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithDeletedTracking();
            context.EntitiesWithDeletedTracking.Add(trackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithDeletedTracking.Update(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithDeletedTracking.Local.Count().Should().Be(1);
            trackedEntity.DeletedBy.Should().Be(null);
            trackedEntity.DeletedOn.Should().Be(null);
        }

        #endregion

        #region Delete
        [Fact]
        public void SaveChangesForNonTrackedEntityShouldDeleteEntityOnDelete()
        {
            // Arrange
            var context = GetInstance();
            var nonTrackedEntity = new EntityWithoutTracking();
            context.EntitiesWithoutTracking.Add(nonTrackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithoutTracking.Remove(nonTrackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithoutTracking.Local.Count().Should().Be(0);
        }

        [Fact]
        public void SaveChangesForTrackCreatedEntityShouldDeleteEntityOnDelete()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithCreatedTracking();
            context.EntitiesWithCreatedTracking.Add(trackedEntity);
            context.SaveChanges();
            trackedEntity.CreatedBy = OtherFixedUser;

            // Act
            context.EntitiesWithCreatedTracking.Remove(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithCreatedTracking.Local.Count().Should().Be(0);
        }

        [Fact]
        public void SaveChangesForTrackLastModifiedEntityShouldDeleteEntityOnDelete()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithLastModifiedTracking();
            context.EntitiesWithLastModifiedTracking.Add(trackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithLastModifiedTracking.Remove(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.EntitiesWithLastModifiedTracking.Local.Count().Should().Be(0);
        }

        [Fact]
        public void SaveChangesForTrackDeletedEntityShouldSetDeletedInfoOnDelete()
        {
            // Arrange
            var context = GetInstance();
            var trackedEntity = new EntityWithDeletedTracking();
            context.EntitiesWithDeletedTracking.Add(trackedEntity);
            context.SaveChanges();

            // Act
            context.EntitiesWithDeletedTracking.Remove(trackedEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();

            //entity is not not be pysically deleted.
            context.EntitiesWithDeletedTracking.Local.Count().Should().Be(1);
            trackedEntity.DeletedBy.Should().Be(FixedUser);
            trackedEntity.DeletedOn.Should().Be(FixedMoment2);
        }
        #endregion

        [Fact]
        public void SaveChangesForTemporalEntityShouldSetTechnicalTemporalInfoOnAdd()
        {
            // Arrange
            var context = GetInstance();
            var temporalEntity = new TemporalEntityAccessor();
            context.TemporalEntities.Add(temporalEntity);

            // Act
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.TemporalEntities.Local.Count().Should().Be(1);
            temporalEntity.TechnicalValidBy.Should().Be(FixedUser);
            temporalEntity.TechnicalValidFrom.Should().Be(FixedMoment1);
            temporalEntity.TechnicalVoidBy.Should().Be(null);
            temporalEntity.TechnicalValidTo.Should().Be(MaxMoment);
        }

        [Fact]
        public void SaveChangesForTemporalEntityShouldCloneRecordSetBothTechnicalTemporalInfoOnUpdate()
        {
            // Arrange
            var context = GetInstance();
            var temporalEntity = new TemporalEntityAccessor();
            context.TemporalEntities.Add(temporalEntity);
            context.SaveChanges();
            temporalEntity.TechnicalValidBy = OtherFixedUser;
            temporalEntity.TechnicalValidFrom = FixedMoment1;

            // Act
            context.TemporalEntities.Update(temporalEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.TemporalEntities.Local.Count().Should().Be(2);
            temporalEntity.TechnicalValidBy.Should().Be(OtherFixedUser);
            temporalEntity.TechnicalValidFrom.Should().Be(FixedMoment1);
            temporalEntity.TechnicalVoidBy.Should().Be(FixedUser);
            temporalEntity.TechnicalValidTo.Should().Be(FixedMoment2);

            var clonedEntity = context.TemporalEntities.Local.Last();
            clonedEntity.TechnicalValidBy.Should().Be(FixedUser);
            clonedEntity.TechnicalValidFrom.Should().Be(FixedMoment2);
            clonedEntity.TechnicalVoidBy.Should().Be(null);
            clonedEntity.TechnicalValidTo.Should().Be(MaxMoment);
        }

        [Fact]
        public void SaveChangesForTemporalEntityShouldUpdateTechnicalTemporalInfoOnDelete()
        {
            // Arrange
            var context = GetInstance();
            var temporalEntity = new TemporalEntityAccessor();
            context.TemporalEntities.Add(temporalEntity);
            context.SaveChanges();
            temporalEntity.TechnicalValidBy = OtherFixedUser;
            temporalEntity.TechnicalValidFrom = FixedMoment1;

            // Act
            context.TemporalEntities.Remove(temporalEntity);
            Action methodUnderTest = () => context.SaveChanges();

            // Assert
            methodUnderTest.Should().NotThrow();
            context.TemporalEntities.Local.Count().Should().Be(1);
            temporalEntity.TechnicalValidBy.Should().Be(OtherFixedUser);
            temporalEntity.TechnicalValidFrom.Should().Be(FixedMoment1);
            temporalEntity.TechnicalVoidBy.Should().Be(FixedUser);
            temporalEntity.TechnicalValidTo.Should().Be(FixedMoment2);
        }

        #region Constructor tests

        [Fact]
        public void ConstructorWithMissingTimeProviderShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            Action constructorUnderTest = () => new DbContextAccessor(null, null);

            // Assert
            constructorUnderTest.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ConstructorWithMissingOptionsShouldThrowArgumentNullException()
        {
            // Arrange
            var timeProviderMock = new Mock<ITimeProvider>();

            // Act
            Action constructorUnderTest = () => new DbContextAccessor(timeProviderMock.Object, null);

            // Assert
            constructorUnderTest.Should().Throw<ArgumentNullException>();
        }

        

        [Fact]
        public void ConstructorWithoutMissingDependenciesShouldNotThrowException()
        {
            // Arrange
            var dbOptions = new DbContextOptionsBuilder<DbContextAccessor>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;
            var timeProviderMock = new Mock<ITimeProvider>();

            // Act
            Action constructorUnderTest = () => new DbContextAccessor(timeProviderMock.Object, dbOptions);

            // Assert
            constructorUnderTest.Should().NotThrow();
        }

        #endregion
    }
}