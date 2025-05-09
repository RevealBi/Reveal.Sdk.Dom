using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    /// <summary>
    /// DO NOT MAKE PUBLIC. WE ARE NOT SUPPORTING THIS YET.
    /// </summary>
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
