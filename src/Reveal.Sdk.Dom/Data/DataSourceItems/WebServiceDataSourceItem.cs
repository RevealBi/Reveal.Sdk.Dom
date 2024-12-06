using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class WebServiceDataSourceItem : DataSourceItem
    {
        public WebServiceDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string Url
        {
            get => Properties.GetValue<string>("Url");
            set => Properties.SetItem("Url", value);
        }
    }
}
