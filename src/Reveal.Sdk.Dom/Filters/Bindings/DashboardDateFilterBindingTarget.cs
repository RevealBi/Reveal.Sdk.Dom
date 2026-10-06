using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilterBindingTarget : BindingTarget
    {
        [JsonProperty("GlobalFilterId")]
        // Compatibility fallback for old JSON that omitted the target ID. New bindings set the actual ID.
        public string DashboardFilterId { get; set; } = "_date";

        [JsonProperty("GlobalFilterFieldName")]
        public string DashboardFilterFieldName { get; set; }

        public DashboardDateFilterBindingTarget()
        {
            SchemaTypeName = SchemaTypeNames.DateGlobalFilterBindingTargetType;
        }
    }
}
