using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Domain
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public IDbSet<Player> Players { get; set; }
        public IDbSet<Team> Teams { get; set; }

        public DbContext()
            : base("LittleLeague")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Team>(GetTeamConfig());
            modelBuilder.Configurations.Add<Player>(GetPlayerConfig());
            base.OnModelCreating(modelBuilder);
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
    }
}
