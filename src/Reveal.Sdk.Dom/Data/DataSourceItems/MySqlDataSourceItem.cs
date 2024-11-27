using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    internal class MySqlDataSourceItem : ProcedureDataSourceItem, IProcessDataOnServer
    {
        public MySqlDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }
        
        /// <summary>
        /// Gets or sets the connection string specifically for MySQL data sources.
        /// </summary>
        [JsonIgnore]
        public string ConnectionString
        {
            get => Properties.GetValue<string>("ConnectionString");
            set => Properties.SetItem("ConnectionString", value);
        }

        /// <summary>
        /// Overrides to ensure proper initialization specific to MySQL.
        /// </summary>
        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            if (dataSource is MySQLDataSource mySqlDataSource)
            {
                return mySqlDataSource;
            }

            return new MySQLDataSource
            {
                Id = dataSource.Id,
                Title = dataSource.Title,
                Subtitle = dataSource.Subtitle,
                DefaultRefreshRate = dataSource.DefaultRefreshRate
            };
        }
    }
}
