namespace HCEHB.ScoreService.Repository.Sql
{
    using HCEHB.ScoreService.Repository.Sql.DbContexts;
    public interface IHealthyBuildingDbContextFactory
    {
        HealthyBuildingDbContext Create();
    }
}
