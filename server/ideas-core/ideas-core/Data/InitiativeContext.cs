﻿using CoE.Ideas.Core.Events;
using CoE.Ideas.Shared.Data;
using EnsureThat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoE.Ideas.Core.Data
{
    internal class InitiativeContext : DbContext
    {
        public InitiativeContext(
            DbContextOptions<InitiativeContext> options,
            DomainEvents domainEvents) : base(options)
        {
            EnsureArg.IsNotNull(domainEvents);
            _domainEvents = domainEvents;
        }

        private readonly DomainEvents _domainEvents;

        public DbSet<Initiative> Initiatives { get; set; }
        public DbSet<InitiativeStatusHistory> IdeaStatusHistories { get; set; }
        public DbSet<StringTemplate> StringTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection.
            // From https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/domain-events-design-implementation:
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB. This makes
            // a single transaction including side effects from the domain event
            // handlers that are using the same DbContext with Scope lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB. This makes
            // multiple transactions. You will need to handle eventual consistency and
            // compensatory actions in case of failures.        

            // see https://ardalis.com/using-mediatr-in-aspnet-core-apps for Mediatr examples


            SetAuditing();

            var result = await base.SaveChangesAsync(cancellationToken);

            await _domainEvents.DispatchDomainEventsAsync(this);

            return result;
        }

        protected virtual void SetAuditing()
        {
            var auditableEntities = base.ChangeTracker.Entries()
                .Where(x => x.Entity.GetType().GetInterfaces().Any(y => y == typeof(IAuditEntity)))
                .GroupBy(x => x.State)
                .ToDictionary(x => x.Key, y => y.Select(z => (IAuditEntity)z.Entity));

            DateTime now = DateTime.UtcNow;
            foreach (var addedEntry in auditableEntities[EntityState.Added])
            {
                // set the Audit values using the CurrentValue property because the properties themselves are not public
                //addedEntry.AuditRecord.AuditCreatedOnUtc = now;

            }

            throw new NotImplementedException();

        }
    }
}
