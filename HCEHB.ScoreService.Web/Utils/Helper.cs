namespace HCEHB.ScoreService.Web.Utils
{
    using Microsoft.Extensions.Configuration;
    using System.Data.SqlClient;

    public static class Helper
    {
        public static string BuildConnection(string dbServerName, string databaseName, string userName, string password)
        {
            if (!string.IsNullOrEmpty(dbServerName) && !string.IsNullOrEmpty(databaseName) && !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var sqlBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = dbServerName,
                    InitialCatalog = databaseName,
                    IntegratedSecurity = false,
                    UserID = userName,
                    Password = password,
                    MultipleActiveResultSets = true,
                    ApplicationName = "EntityFramework"
                };
                return sqlBuilder.ToString();
            }
            else
            {
                return null;
            }
        }
        public static string GetHealthyBuildingDbConnectionString(IConfiguration configuration)
        {
            return BuildConnection(
                configuration["ForgeDatabase:ServerName"],
                configuration["ForgeDatabase:DatabaseName"],
                configuration["ForgeDatabase:Username"],
                configuration["ForgeDatabase:Password"]);

        }
    }
}
