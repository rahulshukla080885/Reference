using HCEHB.ScoreService.Repository.Sql.DbContexts;
using System;

namespace HCEHB.ScoreService.Repository.Sql
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class HealthyBuildingDbContextFactory : IHealthyBuildingDbContextFactory
    {
        private readonly string _connectionString;

        public HealthyBuildingDbContextFactory(IConfiguration configuration)
        {
            _connectionString = ConnectionStringBuilder.Build(
                configuration["Database:ServerName"],
                configuration["Database:DatabaseName"],
                configuration["Database:Username"],
                configuration["Database:Password"]);
        }
        public HealthyBuildingDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthyBuildingDbContext>();

            optionsBuilder
                .UseSqlServer(_connectionString, providerOptions =>
                {
                    providerOptions.CommandTimeout(15);
                    providerOptions.EnableRetryOnFailure(3, TimeSpan.FromMilliseconds(100), null);
                })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return new HealthyBuildingDbContext(optionsBuilder.Options);
        }
    }
}
