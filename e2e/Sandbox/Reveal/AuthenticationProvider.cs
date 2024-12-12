using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Amazon.Athena;
using Reveal.Sdk.Data.Amazon.S3;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.Snowflake;
using Reveal.Sdk.Data.MySql;
using Reveal.Sdk.Data.PostgreSQL;
using System.Threading.Tasks;
using Reveal.Sdk.Data.Amazon.Redshift;
using Reveal.Sdk.Data.Google.Analytics4;
using Reveal.Sdk.Data.Google.BigQuery;
using Reveal.Sdk.Data.Google.Drive;
using Reveal.Sdk.Data.Excel;
using Reveal.Sdk.Data.MongoDB;
using Reveal.Sdk.Data.MySql;
using Reveal.Sdk.Data.OData;
using Reveal.Sdk.Data.Oracle;
using Reveal.Sdk.Data.Microsoft.SynapseAnalytics;

namespace Sandbox.RevealSDK
{
    internal class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = null;
            if (dataSource is RVAzureSqlDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("azure-username", "password");
            }
            else if (dataSource is RVSqlServerDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential();
            }
            else if (dataSource is RVNativeAnalysisServicesDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password", "domain");
            }
            else if (dataSource is RVSnowflakeDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("snow-flake-username", "snow-flake-password");
            }
            else if (dataSource is RVMySqlDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if (dataSource is RVPostgresDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("postgres", "postgres");
            }
            else if (dataSource is RVAthenaDataSource || dataSource is RVS3DataSource)
            {
                userCredential = new RVAmazonWebServicesCredentials("key", "token");
            }
            else if (dataSource is RVRedshiftDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if (dataSource is RVGoogleAnalytics4DataSource)
            {
                userCredential = new RVBearerTokenDataSourceCredential("token", null);
            }
            else if (dataSource is RVBigQueryDataSource)
            {
                userCredential = new RVBearerTokenDataSourceCredential("token", null);
            }
            else if (dataSource is RVGoogleDriveDataSource)
            {
                userCredential = new RVBearerTokenDataSourceCredential("token", null);
            }
            else if(dataSource is RVGoogleSheetDataSource)
            {

            }
            else if(dataSource is RVHttpAnalysisServicesDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if(dataSource is RVAnalysisServicesDataSource)
            {

            }
            else if(dataSource is RVAzureAnalysisServicesDataSource)
            {
                userCredential = new RVBearerTokenDataSourceCredential("token", null);
            }
            else if(dataSource is RVMongoDBDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if(dataSource is RVMySqlDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if(dataSource is RVODataDataSource)
            {
                userCredential = new RVBearerTokenDataSourceCredential("token", null);
            }
            else if(dataSource is RVOracleDataSource)
            {
                new RVUsernamePasswordDataSourceCredential("username", "password");
            }
            else if(dataSource is RVAzureSynapseDataSource)
            {

            }

            return Task.FromResult(userCredential);
        }
    }
}
