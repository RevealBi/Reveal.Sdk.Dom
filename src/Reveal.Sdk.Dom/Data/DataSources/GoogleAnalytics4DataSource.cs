using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Core.Utilities;

namespace Reveal.Sdk.Dom.Data
{
    public class GoogleAnalytics4DataSource : DataSource
    {
        public GoogleAnalytics4DataSource()
        {
            Id = DataSourceHelper.GetNonCanonicalUniqueDataSourceIdentifier(DataSourceProvider.GoogleAnalytics4, Properties);
            Provider = DataSourceProvider.GoogleAnalytics4;
        }

        [JsonIgnore]
        public string AccountId
        {
            get => Properties.GetValue<string>("ga4AccountId");
            set => Properties.SetItem("ga4AccountId", value);
        }

        [JsonIgnore]
        public string PropertyId
        {
            get => Properties.GetValue<string>("PropertyId");
            set => Properties.SetItem("PropertyId", value);
        }

        [JsonIgnore]
        public string PropertyName
        {
            get => Properties.GetValue<string>("PropertyName");
            set => Properties.SetItem("PropertyName", value);
        }
    }
}
