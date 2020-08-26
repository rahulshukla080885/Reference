using honeywell.cedevops.featuretoggleclient;
using honeywell.cedevops.featuretoggleclient.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace HCEHB.ScoreService.Web
{
    using HCEHB.ScoreService.Repository.Sql;
    using HCEHB.ScoreService.Service.DependentInterfaces;
    using Honeywell.HCEHB.DatabaseModels.Common;
    using Honeywell.HCEHB.DatabaseModels.DBContexts;
    using Honeywell.HCEHB.DatabaseModels.Factories;

    public class Startup
    {

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(FeatureToggleClientSingleton.Initialize(GetFeatureToggleClientConfig()));
            services.AddTransient<IDbContextFactory<HealthyBuildingDbContext>, HealthyBuildingDbContextFactory>();
            services.AddTransient<IHealthyBuildingRepository, HealthyBuildingRepository>();
            services.AddHealthChecks().AddCheck<HealthCheck>("DB check");

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v0";
                    document.Info.Title = "ToDo API";
                    document.Info.Description = "A simple ASP.NET Core Web API";

                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHealthChecks("/health");

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }

        #region Helper Methods

        private static IFeatureToggleClientConfig GetFeatureToggleClientConfig()
        {
            var config = new FeatureToggleClientConfig()
            {
                SdkKey = RetrieveEnvironmentVariableValue("LAUNCHDARKLYSDKKEY"),
                EnvironmentName = RetrieveEnvironmentVariableValue("LAUNCHDARKLYENVIRONMENTNAME"),
                OfflineFlagJsonFile = RetrieveEnvironmentVariableValue("LAUNCHDARKLYOFFLINEFLAGJSONFILE")
            };
            return config;
        }

        private static string RetrieveEnvironmentVariableValue(string environmentVariable)
        {
            string environmentVariableValue = "SetByDeployment";
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(environmentVariable)) != true)
            {
                environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable);
            }
            else
            {
                Log.Warning($"There is no value found for the environment variable: {environmentVariable}");
            }
            return environmentVariableValue;
        }

        #endregion
    }
}