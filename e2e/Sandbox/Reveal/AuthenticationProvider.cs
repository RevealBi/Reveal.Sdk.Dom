using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.MySql;
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
            else if (dataSource is RVMySqlDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("root", "armonsuites");
            }
            return Task.FromResult(userCredential);
        }
    }
}
