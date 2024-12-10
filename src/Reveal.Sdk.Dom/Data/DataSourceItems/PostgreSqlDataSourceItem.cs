using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class PostgreSqlDataSourceItem : FunctionDataSourceItem, IProcessDataOnServer
    {
        public PostgreSqlDataSourceItem(string title, PostgreSQLDataSource dataSource) :
            base(title, dataSource)
        { }

        public PostgreSqlDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        public PostgreSqlDataSourceItem(string title, string table, PostgreSQLDataSource dataSource) :
           this(title, dataSource)
        {
            Table = table;
        }

        public PostgreSqlDataSourceItem(string title, string table, DataSource dataSource) :
            this(title, dataSource)
        {
            Table = table;
        }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<PostgreSQLDataSource>(dataSource);
        }
    }
}
