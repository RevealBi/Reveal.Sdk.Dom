using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MongoDBDataSource : DataSource
    {
        public MongoDBDataSource()
        {
            Provider = DataSourceProvider.MongoDB;
        }

        [JsonIgnore]
        public bool ProcessDataOnServerDefaultValue
        {
            get => Properties.GetValue<bool>("ServerAggregationDefault");
            set => Properties.SetItem("ServerAggregationDefault", value);
        }

        [JsonIgnore]
        public bool ProcessDataOnServerReadOnly
        {
            get => Properties.GetValue<bool>("ServerAggregationReadOnly");
            set => Properties.SetItem("ServerAggregationReadOnly", value);
        }
    }
}
