using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MySqlDataSourceItem : ProcedureDataSourceItem, IProcessDataOnServer
    {
        public MySqlDataSourceItem(string title, string table, MicrosoftSqlServerDataSource dataSource) :
            this(title, dataSource)
        {
            Table = table;
        }

        public MySqlDataSourceItem(string title, MySqlDataSource dataSource) :
            base(title, dataSource)
        { }

        public MySqlDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<MySqlDataSource>(dataSource);
        }
    }
}
