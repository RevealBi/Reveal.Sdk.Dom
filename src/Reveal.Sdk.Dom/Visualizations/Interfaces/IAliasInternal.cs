using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using Reveal.Sdk.Dom.Data;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations
{
    //Temporary interface to access the TableAlias property for blending support
    [JsonConverter(typeof(DataSpecConverter))]
    public interface IAliasInternal
    {
        //used when joining data from multiple sources
        [JsonProperty("TableAlias")]
        public string TableAlias { get; set; }

        
    }
}
