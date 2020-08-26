namespace HCEHB.ScoreService.Repository.Sql
{
    using Microsoft.Data.SqlClient;

    public static class ConnectionStringBuilder
    {
        public static string Build(string serverName, string databaseName, string userId, string password)
        {
            if (string.IsNullOrEmpty(serverName) || string.IsNullOrEmpty(databaseName) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = false,
                UserID = userId,
                Password = password,
                MultipleActiveResultSets = true,
                ApplicationName = "Healthy Building Score Service"
            };

            return sqlBuilder.ToString();
        }
    }
}
