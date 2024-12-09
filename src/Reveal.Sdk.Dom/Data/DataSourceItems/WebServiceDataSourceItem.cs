using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;
using System;

namespace Reveal.Sdk.Dom.Data
{
    public class WebServiceDataSourceItem : DataSourceItem
    {
        public WebServiceDataSourceItem(string title) :
            this(title, new DataSource())
        { }

        public WebServiceDataSourceItem(string title, string uri) :
            this(title, uri, new DataSource())
        { }

        public WebServiceDataSourceItem(string title, string uri, DataSource dataSource) :
            this(title, dataSource)
        {
            Url = uri;
        }

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
