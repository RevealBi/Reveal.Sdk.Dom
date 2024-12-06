using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class WebServiceDataSource : DataSource
    {
        public WebServiceDataSource()
        {
            Provider = DataSourceProvider.WebService;
        }

        [JsonIgnore]
        public string Url
        {
            get => Properties.GetValue<string>("URL");
            set => Properties.SetItem("URL", value);
        }

        [JsonIgnore]
        public bool UseAnonymousAuthentication
        {
            get => Properties.GetValue<bool>("UseAnonymousAuthentication");
            set => Properties.SetItem("UseAnonymousAuthentication", value);
        }
    }
}
