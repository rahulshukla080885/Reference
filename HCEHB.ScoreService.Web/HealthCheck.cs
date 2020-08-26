namespace HCEHB.ScoreService.Web
{
    using Honeywell.HCEHB.DatabaseModels.DBContexts;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Serilog;
    using System.Threading;
    using System.Threading.Tasks;
    public class HealthCheck : IHealthCheck
    {
        private readonly HealthyBuildingDbContext _healthyBuildingDbContext;

        public HealthCheck(HealthyBuildingDbContext healthyBuildingDbContext)
        {
            _healthyBuildingDbContext = healthyBuildingDbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _healthyBuildingDbContext.Database.CanConnectAsync(cancellationToken) ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
            Log.Information("Database Connection Status :" + result.Status.ToString());
            return result;
        }
    }
}