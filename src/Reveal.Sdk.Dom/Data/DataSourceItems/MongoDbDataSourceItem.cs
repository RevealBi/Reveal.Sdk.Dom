using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MongoDbDataSourceItem : DataSourceItem, IProcessDataOnServer
    {
        public MongoDbDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string Collection
        {
            get => Properties.GetValue<string>("Table");
            set => Properties.SetItem("Table", value);
        }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }
    }
}
