using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MicrosoftAzureSqlServerDataSource : MicrosoftSqlServerDataSource
    {
        public MicrosoftAzureSqlServerDataSource()
        {
            Provider = DataSourceProvider.MicrosoftAzureSqlServer;
        }

        [JsonIgnore]
        public bool TrustServerCertificate
        {
            get => Properties.GetValue<bool>("TrustServerCertificate");
            set => Properties.SetItem("TrustServerCertificate", value);
        }
    }
}
