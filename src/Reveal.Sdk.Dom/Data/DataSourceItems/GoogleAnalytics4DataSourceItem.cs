using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class GoogleAnalytics4DataSourceItem : DataSourceItem
    {
        public GoogleAnalytics4DataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { 
            if (dataSource is GoogleAnalytics4DataSource ga4DS)
            {
                AccountId = ga4DS.AccountId;
                PropertyId = ga4DS.PropertyId;
                PropertyName = ga4DS.PropertyName;
                HasTabularData = false;
            }
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
            set {
                Id = value;
                Properties.SetItem("PropertyId", value);
            }
        }

        [JsonIgnore]
        public string PropertyName
        {
            get => Properties.GetValue<string>("PropertyName");
            set => Properties.SetItem("PropertyName", value);
        }
    }
}
