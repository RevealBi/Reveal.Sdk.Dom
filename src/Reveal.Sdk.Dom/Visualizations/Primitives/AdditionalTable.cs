using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class AdditionalTable
    {
        public string Alias { get; set; }

        [JsonProperty("DataSpec")]
        public DataDefinitionBase DataDefinition { get; set; } //todo: convert to interface

        public List<JoinCondition> JoinConditions { get; set; }
        public AdditionalTable()
        {
            JoinConditions = new List<JoinCondition>();
        }
    }
}
