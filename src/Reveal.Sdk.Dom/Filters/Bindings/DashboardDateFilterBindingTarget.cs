using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;
using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilterBindingTarget : BindingTarget
    {
        [JsonProperty("GlobalFilterId")]
        public string DashboardFilterId { get; set; }

        [OnDeserializing]
        internal void ReadLegacyId(StreamingContext context)
        {
            DashboardFilterId = "_date";
        }

        [JsonProperty("GlobalFilterFieldName")]
        public string DashboardFilterFieldName { get; set; }

        public DashboardDateFilterBindingTarget()
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterBindingTargetType;
        }
    }
}
