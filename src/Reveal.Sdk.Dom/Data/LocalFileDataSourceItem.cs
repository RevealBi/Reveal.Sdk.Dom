using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reveal.Sdk.Dom.Data
{
    public abstract class LocalFileDataSourceItem : DataSourceItem
    {
        public LocalFileDataSourceItem(string title) : this(title, new LocalFileDataSource())
        { }

        public LocalFileDataSourceItem(string title, string path) : this(title, new LocalFileDataSource())
        {
            Path = path;
        }

        public LocalFileDataSourceItem(string title, DataSource dataSource) : base(title, dataSource)
        {
            InitializeResourceItem(title);
        }

        [JsonIgnore]
        public string Path
        {
            get { return ResourceItem.Properties.GetValue<string>("URI"); }
            set { ResourceItem.Properties.SetItem("URI", $"local:{value}"); }
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            if (dataSource is LocalFileDataSource)
                return dataSource;

            return new LocalFileDataSource();
        }

        protected void InitializeResourceItem(string title)
        {
            ResourceItemDataSource = new LocalFileDataSource();
            ResourceItem = new DataSourceItem
            {
                DataSource = ResourceItemDataSource,
                DataSourceId = ResourceItemDataSource.Id,
                Title = title
            };
        }
    }
}
