using System;

namespace Reveal.Sdk.Dom.Data
{
    public class DataSourceItemFactory : IDataSourceItemFactory
    {
        public DataSourceItem Create(DataSourceType type, string id, string title)
        {
            return Create(type, id, title, new DataSource());
        }

        public DataSourceItem Create(DataSourceType type, string id, string title, DataSource dataSource)
        {
            return Create(type, id, title, null, dataSource);
        }

        public DataSourceItem Create(DataSourceType type, string id, string title, string subtitle)
        {
            return Create(type, id, title, subtitle, new DataSource());
        }

        public DataSourceItem Create(DataSourceType type, string id, string title, string subtitle, DataSource dataSource)
        {
            DataSourceItem item = type switch
            {
                DataSourceType.MicrosoftSqlServer => new MicrosoftSqlServerDataSourceItem(title, dataSource),
                DataSourceType.REST => new RestDataSourceItem(title, dataSource),
                DataSourceType.MySql => new MySqlDataSourceItem(title, dataSource),
                _ => throw new NotImplementedException($"No builder implemented for provider: {type}")
            };

            item.Id = id;
            item.Subtitle = subtitle;
            return item;
        }
    }
}
