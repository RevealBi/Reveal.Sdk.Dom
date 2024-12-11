﻿using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Amazon.Athena;
using Reveal.Sdk.Data.Amazon.S3;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.PostgreSQL;
using System.Threading.Tasks;

namespace Sandbox.RevealSDK
{
    internal class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = null;
            if (dataSource is RVSqlServerDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential();
            }
            else if (dataSource is RVNativeAnalysisServicesDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password", "domain");
            }
            else if (dataSource is RVPostgresDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("postgres", "postgres");
            }
            else if (dataSource is RVAthenaDataSource || dataSource is RVS3DataSource)
            {
                userCredential = new RVAmazonWebServicesCredentials("key", "token");
            }
            return Task.FromResult(userCredential);
        }
    }
}
