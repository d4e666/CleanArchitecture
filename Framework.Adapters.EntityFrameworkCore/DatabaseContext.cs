#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Adapters.Persistence.Database.NetStandard.Concurrency;
using Framework.Adapters.Persistence.Database.NetStandard.EditTracking;
using Framework.Adapters.Persistence.Database.NetStandard.Temporal;
using Framework.Domain.Services.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

#endregion

namespace Framework.Adapters.EntityFrameworkCore
{
    public abstract class DatabaseContext : DbContext
    {
        #region Constructors

        protected DatabaseContext(ITimeProvider timeProvider, DbContextOptions options) : base(options)
        {
            this.TimeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }

        #endregion

        #region Properties

        public ITimeProvider TimeProvider { get; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.UpdateTrackingAsync();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            this.UpdateTrackingAsync();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateTrackingAsync()
        {
            this.ChangeTracker.DetectChanges();

            var user = "Unknown";
            var moment = this.TimeProvider.GetLocalDateAndTimeWithOffset();
            var maxMoment = this.TimeProvider.MaxDateTimeWithOffset;
            Parallel.ForEach(
                this.GetEntries(EntityState.Added), entry =>
                                                    {
                                                        if (entry.Entity is IConcurrencyStampAdapter concurrentEntity)
                                                            concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());

                                                        if (entry.Entity is ITrackCreatedAdapter addedEntity)
                                                            addedEntity.SetCreated(user, moment);

                                                        if (entry.Entity is ITrackLastModifiedAdapter modifiedEntity)
                                                            modifiedEntity.SetLastModified(user, moment);

                                                        if (entry.Entity is ITechnicalTemporalAdapter temporalEntity)
                                                        {
                                                            temporalEntity.SetTechnicalValidBy(user);
                                                            temporalEntity.SetTechnicalValidFrom(moment);
                                                            temporalEntity.SetTechnicalValidTo(maxMoment);
                                                        }
                                                    });
            Parallel.ForEach(
                this.GetEntries(EntityState.Modified), entry =>
                                                       {
                                                           if (entry.Entity is IConcurrencyStampAdapter concurrentEntity)
                                                               concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());

                                                           if (entry.Entity is ITrackLastModifiedAdapter modifiedEntity)
                                                               modifiedEntity.SetLastModified(user, moment);

                                                           if (entry.Entity is ITechnicalTemporalAdapter temporalEntity)
                                                           {
                                                               temporalEntity.SetTechnicalVoidBy(user);
                                                               temporalEntity.SetTechnicalValidTo(moment);
                                                               var clone = (ITechnicalTemporalAdapter)temporalEntity.Clone();
                                                               clone.SetTechnicalValidBy(user);
                                                               clone.SetTechnicalValidFrom(moment);
                                                               clone.SetTechnicalValidTo(maxMoment);
                                                               this.Add(clone);
                                                           }
                                                       });

            Parallel.ForEach(
                this.GetEntries(EntityState.Deleted), entry =>
                                                      {
                                                          if (entry.Entity is IConcurrencyStampAdapter concurrentEntity)
                                                              concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());

                                                          if (entry.Entity is ITrackDeletedAdapter deletedEntity)
                                                          {
                                                              entry.State = EntityState.Modified;
                                                              deletedEntity.SetDeleted(user, moment);
                                                          }

                                                          if (entry.Entity is ITechnicalTemporalAdapter temporalEntity)
                                                          {
                                                              entry.State = EntityState.Modified;
                                                              temporalEntity.SetTechnicalVoidBy(user);
                                                              temporalEntity.SetTechnicalValidTo(moment);
                                                          }
                                                      });
        }

        private IEnumerable<EntityEntry> GetEntries(EntityState state)
        {
            return this.ChangeTracker.Entries().Where(entry => entry.State == state).ToList();
        }

        #endregion
    }
}