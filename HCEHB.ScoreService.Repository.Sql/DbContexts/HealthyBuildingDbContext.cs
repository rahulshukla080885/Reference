namespace HCEHB.ScoreService.Repository.Sql.DbContexts
{
    using HCEHB.ScoreService.Repository.Sql.Entities;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class HealthyBuildingDbContext : DbContext
    {
        public HealthyBuildingDbContext() { }

        public HealthyBuildingDbContext(DbContextOptions<HealthyBuildingDbContext> options) : base(options)
        {

        }
        public virtual DbSet<ScoreCategory> ScoreCategories { get; set; }

        public virtual DbSet<BuildingScore> BuildingScores { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreCategory>().HasKey(c => new { c.MaximumScore, c.MinimumScore, c.ScoreCategoryId });

            modelBuilder.Entity<BuildingScore>().HasKey(c => new
            {
                c.FacilityId,
                c.Score,
                c.ScoreCategory,
                c.IndoorClimateIndex,
                c.IndoorClimateIndexCategory,
                c.PollutantIndex,
                c.PollutantIndexCategory,
                c.SiteId,
                c.SiteName,
                c.ZoneId,
                c.ZoneName,
                c.TimeStamp
            });
        }
    }
}
