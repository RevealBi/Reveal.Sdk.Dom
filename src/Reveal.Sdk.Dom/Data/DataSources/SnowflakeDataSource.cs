using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class SnowflakeDataSource : SchemaDataSource
    {
        public SnowflakeDataSource()
        {
            Provider = DataSourceProvider.Snowflake;
        }

        [JsonIgnore]
        public string Account
        {
            get => Properties.GetValue<string>("Account");
            set => Properties.SetItem("Account", value);
        }

        [JsonIgnore]
        public string Role
        {
            get => Properties.GetValue<string>("Role");
            set => Properties.SetItem("Role", value);
        }

        [JsonIgnore]
        public string Warehouse
        {
            get => Properties.GetValue<string>("Warehouse");
            set => Properties.SetItem("Warehouse", value);
        }
    }
}
