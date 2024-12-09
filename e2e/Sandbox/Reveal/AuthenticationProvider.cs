using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using System.Threading.Tasks;

namespace Sandbox.RevealSDK
{
    internal class AuthenticationProvider : IRVAuthenticationProvider
    {
        public Task<IRVDataSourceCredential> ResolveCredentialsAsync(RVDashboardDataSource dataSource)
        {
            IRVDataSourceCredential userCredential = null;
            if (dataSource is RVAzureSqlDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("revealAdmin", "Armonsuites1");
            }
            else if (dataSource is RVSqlServerDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential();
            }
            else if (dataSource is RVNativeAnalysisServicesDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("username", "password", "domain");
            }
            
            return Task.FromResult(userCredential);
        }
    }
}
