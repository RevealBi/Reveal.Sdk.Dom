using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Data
{
    public class RestDataSourceItem : WebServiceDataSourceItem
    {
        DataSource _dataSource;

        public RestDataSourceItem(string title) :
            this(title, new DataSource())
        { }

        public RestDataSourceItem(string title, string url) :
            this(title, url, new DataSource())
        { }

        public RestDataSourceItem(string title, string url, DataSource dataSource) :
            this(title, dataSource)
        {
            Url = url;
        }

        public RestDataSourceItem(string title, DataSource dataSource) :
            base(title, new JsonDataSource())
        {
            _dataSource = dataSource ?? new DataSource();
            InitializeResourceItem(title);
            UpdateResourceItemDataSource(_dataSource);
        }


        public void AddHeader(HeaderType headerType, string value)
        {
            var propertyKey = "Headers";

            var headerValue = $"{AddDashesToEnumName(headerType.ToString())}={value}";

            if (!ResourceItemDataSource.Properties.ContainsKey(propertyKey))
            {
                ResourceItemDataSource.Properties.Add(propertyKey, new List<string> { headerValue });
            }
            else
            {
                var headers = (List<string>)ResourceItemDataSource.Properties[propertyKey];
                headers.Add(headerValue);
            }
        }


        protected void InitializeResourceItem(string title)
        {
            ResourceItemDataSource = new DataSource { Provider = DataSourceProvider.REST };
            ResourceItem = new DataSourceItem
            {
                DataSource = ResourceItemDataSource,
                DataSourceId = ResourceItemDataSource.Id,
                Title = title
            };

            ResourceItemDataSource = ResourceItemDataSource;
            ResourceItem = ResourceItem;
        }

        string AddDashesToEnumName(string name)
        {
            return string.Concat(name.Select(x => char.IsUpper(x) ? "-" + x : x.ToString())).TrimStart('-');
        }
    }
}