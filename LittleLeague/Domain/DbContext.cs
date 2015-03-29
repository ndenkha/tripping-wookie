using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;
using log4net;
using Domain.Model;
using System.Threading;
using Ninject;

namespace Domain
{
    public class DbContext : System.Data.Entity.DbContext
    {
        readonly IKernel kernel;

        public IDbSet<Team> Teams { get; set; }

        // Should be used for write scenarios.
        public DbContext(IKernel kernel)
            : base("LittleLeague")
        {
            this.kernel = kernel;
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Team>(GetTeamConfig());
            modelBuilder.Configurations.Add<Player>(GetPlayerConfig());
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var item in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
            {
                if (item.Entity is IAuditable)
                {
                    var auditable = (IAuditable)item.Entity;
                    if (item.State == EntityState.Added)
                    {
                        auditable.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                        auditable.CreatedDate = DateTime.UtcNow;
                        auditable.LastUpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                        auditable.LastUpdatedDate = DateTime.UtcNow;
                    }

                    if (item.State == EntityState.Modified)
                    {
                        auditable.LastUpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                        auditable.LastUpdatedDate = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChanges();
        }

        EntityTypeConfiguration<Team> GetTeamConfig()
        {
            var config = new EntityTypeConfiguration<Team>();
            config.ToTable("Team", "dbo");
            config.HasKey(x => x.TeamId);

            config.HasMany(x => x.Players).WithRequired(x => x.Team).HasForeignKey(x => x.TeamId);
            return config;
        }

        EntityTypeConfiguration<Player> GetPlayerConfig()
        {
            var config = new EntityTypeConfiguration<Player>();
            config.ToTable("Player", "dbo");
            config.HasKey(x => x.PlayerId);
            return config;
        }

        void ObjectContext_ObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            if (e.Entity is IDependencyConsumer)
            {
                (e.Entity as IDependencyConsumer).Accept(kernel);
            }
        }
    }
}
