using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class AmazonRedshiftDataSource : HostDataSource
    {
        public AmazonRedshiftDataSource()
        {
            Provider = DataSourceProvider.AmazonRedshift;
        }

        [JsonIgnore]
        public string Schema
        {
            get => Properties.GetValue<string>("Schema");
            set => Properties.SetItem("Schema", value);
        }
    }
}
