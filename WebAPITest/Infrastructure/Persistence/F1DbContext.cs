using Microsoft.EntityFrameworkCore;
using WebAPITest.Domain.Models.DomainEntities;
using WebAPITest.Domain.Models.DomainEvents;
using WebAPITest.Infrastructure.Persistence.Configuratoin.ConsumedEvents;
using WebAPITest.Infrastructure.Persistence.Configuratoin.PublishedEvents;
using WebAPITest.Infrastructure.Persistence.DbModel;

namespace WebAPITest.Infrastructure.Persistence
{
    public class F1DbContext : DbContext
    {
        public F1DbContext(DbContextOptions<F1DbContext> options) : base(options)
        {

        }
        //Доменные сущности
        public DbSet<Driver> Driver { get; set; }
        public DbSet<DriverRaceParticipation> DriverRaceParticipation { get; set; }
        public DbSet<DriverSeasonParticipation> DriverSeasonParticipation { get; set; }
        public DbSet<DriverTeamContract> DriverTeamContract { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamRaceParticipation> TeamRaceParticipation { get; set; }
        public DbSet<TeamSeasonParticipation> TeamSeasonParticipation { get; set; }
        public DbSet<Track> Track { get; set; }

        //Доменные события
        //Базовые
        public DbSet<ConsumedEventDbModel> ConsumedEvent { get; set; }
        public DbSet<ConsumedEventType> ConsumedEventType { get; set; }
        public DbSet<ConsumedEventState> ConsumedEventState { get; set; }

        public DbSet<PublishedEvent> PublishedEvent { get; set; }
        public DbSet<PublishedEventType> PublishedEventType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ConsumedEventStateConfiguration());
            modelBuilder.ApplyConfiguration(new ConsumedEventTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PublishedEventTypeConfiguration());
        }
    }
}
