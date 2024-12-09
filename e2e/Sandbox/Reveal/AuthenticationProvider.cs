using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Google.Drive;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using System.IO;

namespace Sandbox.RevealSDK
{
    internal class AuthenticationProvider : IRVAuthenticationProvider
    {
        public async Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = null;
            string _token = string.Empty;
            if (dataSource is RVSqlServerDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential();
            }
            else if (dataSource is RVNativeAnalysisServicesDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password", "domain");
            } else if (dataSource is RVGoogleDriveDataSource)
            {
                if (string.IsNullOrEmpty(_token))
                    _token = await CreateJwtToken();

                userCredential = new RVBearerTokenDataSourceCredential(_token, null);
            }
            return userCredential;
        }

        async Task<string> CreateJwtToken()
        {
            var pathToJsonFile = Path.Combine(Environment.CurrentDirectory, "Data/GoogleServiceAccountAuth.json");
            var credentials = GoogleCredential.FromFile(pathToJsonFile).CreateScoped("https://www.googleapis.com/auth/drive",
                                                                                     "https://www.googleapis.com/auth/userinfo.email",
                                                                                     "https://www.googleapis.com/auth/userinfo.profile");
            var token = await credentials.UnderlyingCredential.GetAccessTokenForRequestAsync().ConfigureAwait(false);
            return token;
        }
    }
}
