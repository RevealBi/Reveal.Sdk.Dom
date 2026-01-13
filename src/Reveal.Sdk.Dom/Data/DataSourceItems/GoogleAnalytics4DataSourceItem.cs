using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    //DO NOT IMPLEMENT
    //Some data sources are used in Slingshot, but not the SDK. We have decided not to implement
    //this data sourec in the SDK which means we do not need to support it in the DOM.
    //Leaving the class here just in case we need to implement it in the future.
    internal class GoogleAnalytics4DataSourceItem : DataSourceItem
    {
        public GoogleAnalytics4DataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string AccountId
        {
            get => Properties.GetValue<string>("AccountId");
            set => Properties.SetItem("AccountId", value);
        }

        [JsonIgnore]
        public string PropertyId
        {
            get => Properties.GetValue<string>("PropertyId");
            set => Properties.SetItem("PropertyId", value);
        }
    }
}
