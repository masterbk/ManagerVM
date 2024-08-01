using ManagerVM.Data.Entities;
using ManagerVM.Data.Helper;
using ManagerVM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Data
{
    public class VMDbContext: DbContext
    {
        private readonly DateTimeProvider _dateTimeProvider;
        private readonly CurrentUserProvider _currentUserProvider;

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VMEntity> VMEntities { get; set; }
        public DbSet<TenantEntity> TenantEntities { get; set; }
        public DbSet<OpenStackEntity> OpenStackEntities { get; set; }
        public DbSet<OpenStackInTenantEntity> OpenStackInTenantEntities { get; set; }

        public VMDbContext()
        {

        }

        public VMDbContext(DateTimeProvider dateTimeProvider, CurrentUserProvider currentUserProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _currentUserProvider = currentUserProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ServiceConstants.NAME);
            base.OnModelCreating(modelBuilder);

            #region Map    
            //modelBuilder.Entity<Media>().HasQueryFilter(a => !a.IsDeleted && a.TenantId == _tenantId);

            //modelBuilder.Entity<Provider>().HasQueryFilter(a => !a.IsDeleted);
            #endregion
        }

        /// <summary>
        /// Sử dụng cho Migrations code first
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=172.16.1.118;Initial Catalog=ManagerVM;Persist Security Info=True;User ID=sa;Password=sa@123;MultipleActiveResultSets=True;TrustServerCertificate=True");
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            DateTime now = _dateTimeProvider.Now;
            long userId = _currentUserProvider.UserId;
            long tenantId = _currentUserProvider.TenantId;
            foreach (EntityEntry<BaseEntity> item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedAt = now;
                        item.Entity.LastModifiedAt = now;
                        break;
                    case EntityState.Modified:
                        item.Entity.LastModifiedAt = now;
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Modified;
                        item.Entity.LastModifiedAt = now;
                        item.Entity.IsDeleted = true;
                        break;
                }

                if (userId > 0)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            item.Entity.CreatedBy = userId;
                            item.Entity.LastModifiedBy = userId;
                            break;
                        case EntityState.Modified:
                            item.Entity.LastModifiedBy = userId;
                            break;
                    }
                }
            }

            foreach (EntityEntry<AuditableEntity> item2 in ChangeTracker.Entries<AuditableEntity>())
            {
                if (item2.Entity.TenantId == 0L)
                {
                    EntityState state = item2.State;
                    if ((uint)(state - 3) <= 1u)
                    {
                        item2.Entity.TenantId = tenantId;
                    }
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
