#region Usings

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Adapters.Persistence.Database.NetStandard.Concurrency;
using Framework.Adapters.Persistence.Database.NetStandard.EditTracking;
using Framework.Adapters.Persistence.Database.NetStandard.Temporal;
using Framework.Domain.Services.Time;

#endregion

namespace Framework.Adapters.EntityFramework
{
    public abstract class DatabaseContext : DbContext
    {
        #region Constructors

        protected DatabaseContext(ITimeProvider timeProvider, string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.TimeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }

        protected DatabaseContext(ITimeProvider timeProvider, DbConnection connection) : base(connection, true)
        {
            this.TimeProvider = timeProvider ?? throw new ArgumentNullException(nameof(timeProvider));
        }

        #endregion

        #region Properties

        public ITimeProvider TimeProvider { get; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override int SaveChanges()
        {
            this.UpdateTrackingAsync();

            return base.SaveChanges();
        }

        /// <inheritdoc />
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            this.UpdateTrackingAsync();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTrackingAsync()
        {
            this.ChangeTracker.DetectChanges();
            // skip save changes on Add/Remove
            if (!this.ChangeTracker.Entries().Any())
                return;
            var user = "Unknown";
            var moment = this.TimeProvider.GetLocalDateAndTimeWithOffset();
            var maxMoment = this.TimeProvider.MaxDateTimeWithOffset;
            Parallel.ForEach(
                   this.GetEntries(EntityState.Added), entry =>
                                                       {
                                                           if (entry.Entity is IConcurrencyStampAdapter concurrentEntity)
                                                           {
                                                               concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());
                                                           }
                                                           if (entry.Entity is ITrackCreatedAdapter addedEntity)
                                                           {
                                                               addedEntity.SetCreated(user, moment);
                                                           }

                                                           if (entry.Entity is ITrackLastModifiedAdapter modifiedEntity)
                                                           {
                                                               modifiedEntity.SetLastModified(user, moment);
                                                           }

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
                                                                  {
                                                                      concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());
                                                                  }
                                                                  if (entry.Entity is ITrackLastModifiedAdapter modifiedEntity)
                                                                  {
                                                                      modifiedEntity.SetLastModified(user, moment);
                                                                  }

                                                                  if (entry.Entity is ITechnicalTemporalAdapter temporalEntity)
                                                                  {
                                                                      temporalEntity.SetTechnicalVoidBy(user);
                                                                      temporalEntity.SetTechnicalValidTo(moment);
                                                                      var clone = (ITechnicalTemporalAdapter)temporalEntity.Clone();
                                                                      clone.SetTechnicalValidBy(user);
                                                                      clone.SetTechnicalValidFrom(moment);
                                                                      clone.SetTechnicalValidTo(maxMoment);
                                                                      this.Set(entry.Entity.GetType()).Add(clone);
                                                                  }
                                                              });

            Parallel.ForEach(
                 this.GetEntries(EntityState.Deleted), entry =>
                                                               {
                                                                   if (entry.Entity is IConcurrencyStampAdapter concurrentEntity)
                                                                   {
                                                                       concurrentEntity.SetConcurrencyStamp(Guid.NewGuid());
                                                                   }
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

        private IEnumerable<DbEntityEntry> GetEntries(EntityState state)
        {
            return this.ChangeTracker.Entries().Where(entry => entry.State == state).ToList();
        }

        #endregion
    }
}