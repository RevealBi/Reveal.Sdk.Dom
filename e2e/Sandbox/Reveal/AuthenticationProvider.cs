using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Amazon.Athena;
using Reveal.Sdk.Data.Amazon.S3;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.Snowflake;
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
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Text;

namespace Sandbox.RevealSDK
{
    internal class AuthenticationProvider : IRVAuthenticationProvider
    {
        public async Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
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
            else if (dataSource is RVGoogleDriveDataSource)
            {
                var _token = RetrieveGoogleDriveBearerToken();

                userCredential = new RVBearerTokenDataSourceCredential(_token, null);
            }
            else if (dataSource is RVSnowflakeDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("snow-flake-username", "snow-flake-password");
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

            return userCredential;
        }

        internal string RetrieveGoogleDriveBearerToken()
        {
            var jsonKey = @"
                {
                  ""type"": ""service_account"",
                  ""project_id"": ""testreveal-henry"",
                  ""private_key_id"": ""***"",
                  ""private_key"": ""******"",
                  ""client_email"": ""henrytestreveal@testreveal-henry.iam.gserviceaccount.com"",
                  ""client_id"": ""***"",
                  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
                  ""token_uri"": ""https://oauth2.googleapis.com/token"",
                  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
                  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/henrytestreveal%40testreveal-henry.iam.gserviceaccount.com"",
                  ""universe_domain"": ""googleapis.com""
                }
            ";

            var memoryStream = new MemoryStream();
            byte[] jsonKeyBytes = Encoding.UTF8.GetBytes(jsonKey);
            memoryStream.Write(jsonKeyBytes, 0, jsonKeyBytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var credential = GoogleCredential.FromStream(memoryStream).CreateScoped("https://www.googleapis.com/auth/drive",
                                                                                     "https://www.googleapis.com/auth/userinfo.email",
                                                                                     "https://www.googleapis.com/auth/userinfo.profile");

            var accessToken = credential.UnderlyingCredential.GetAccessTokenForRequestAsync().Result;

            return accessToken;
        }
    }
}
