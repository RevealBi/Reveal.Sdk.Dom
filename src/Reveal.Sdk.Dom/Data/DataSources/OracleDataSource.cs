using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class OracleDataSource : HostDataSource
    {
        public OracleDataSource()
        {
            Provider = DataSourceProvider.Oracle;
        }

        //todo: should this be two separate objects?
        [JsonIgnore]
        public string Service
        {
            get => Properties.GetValue<string>("Service");
            set => Properties.SetItem("Service", value);
        }

        //todo: should this be two separate objects?
        [JsonIgnore]
        public string SID
        {
            get => Properties.GetValue<string>("SID");
            set => Properties.SetItem("SID", value);
        }
    }
}
