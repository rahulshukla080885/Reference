namespace HCEHB.ScoreService.Service.DependentInterfaces
{
    using Honeywell.HCEHB.DatabaseModels.Entities;
    using System;
    using System.Threading.Tasks;
    public interface IHealthyBuildingRepository
    {
        public Task<BuildingScore[]> GetHealthyBuildingScoreTask(string[] facilities, DateTime timeStamp);
        public Task<BuildingScore[]> GetRecentAvailableScoreTask(string[] facilities);
    }
}
