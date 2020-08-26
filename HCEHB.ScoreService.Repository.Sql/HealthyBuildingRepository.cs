

namespace HCEHB.ScoreService.Repository.Sql
{
    using HCEHB.ScoreService.Service.DependentInterfaces;
    using Honeywell.HCEHB.DatabaseModels.Common;
    using Honeywell.HCEHB.DatabaseModels.DBContexts;
    using Honeywell.HCEHB.DatabaseModels.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;


    public class HealthyBuildingRepository : IHealthyBuildingRepository
    {
        private readonly IDbContextFactory<HealthyBuildingDbContext> _dbContextFactory;

        public HealthyBuildingRepository(IDbContextFactory<HealthyBuildingDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<BuildingScore[]> GetHealthyBuildingScoreTask(string[] facilities, DateTime timeStamp)
        {
            using var buildingDbContext = _dbContextFactory.Create();

            var resultText = await buildingDbContext.BuildingScores
                .Where(x => facilities.Contains(x.FacilityId))
                .OrderByDescending(x => x.TimeStamp)
                .ToArrayAsync();

            return resultText;
        }

        public async Task<BuildingScore[]> GetRecentAvailableScoreTask(string[] facilities)
        {
            using var buildingDbContext = _dbContextFactory.Create();

            var resultText = await buildingDbContext.BuildingScores
                .Where(x => facilities.Contains(x.FacilityId))
                .ToArrayAsync();

            return resultText;
        }
    }
}
