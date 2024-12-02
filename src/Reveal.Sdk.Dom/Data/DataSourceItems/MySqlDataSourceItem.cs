using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MySqlDataSourceItem : ProcedureDataSourceItem, IProcessDataOnServer
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

        [JsonIgnore]
        public string ConnectionString
        {
            get => Properties.GetValue<string>("ConnectionString");
            set => Properties.SetItem("ConnectionString", value);
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<MySQLDataSource>(dataSource);
        }
    }
}
