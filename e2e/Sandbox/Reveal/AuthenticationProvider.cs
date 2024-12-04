using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.MongoDB;
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
                userCredential = new RVUsernamePasswordDataSourceCredential("testuser", "r3v34ldb4", null);
            }
            else if (dataSource is RVMongoDBDataSource)
            {
                userCredential = new RVUsernamePasswordDataSourceCredential("user01", "DustFillingABlack4Times", "admin");
            }
            return Task.FromResult(userCredential);
        }
    }
}
