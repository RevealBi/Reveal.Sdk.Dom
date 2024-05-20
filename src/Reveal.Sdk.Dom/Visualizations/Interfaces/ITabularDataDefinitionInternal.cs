using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using Reveal.Sdk.Dom.Data;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    //Temporary interface to access the AdditionalTables property for blending support
    [JsonConverter(typeof(DataSpecConverter))]
    public interface ITabularDataDefinitionInternal
    {
        /// <summary>
        /// Gets or sets the additional tables.
        /// </summary>
       [JsonProperty("AdditionalTables")]
        public List<AdditionalTable> AdditionalTables { get; internal set; }

        
    }
}
