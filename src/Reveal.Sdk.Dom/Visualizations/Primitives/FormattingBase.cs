using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class FormattingBase : SchemaType
    {
        /// <summary>
        /// Gets or sets whether this format wins over the data source's metadata format.
        /// When <c>false</c>, Reveal treats the format as inherited and replaces it with the metadata format, if the data source has one.
        /// Set it to <c>true</c> to keep a format set from code.
        /// </summary>
        public bool OverrideDefaultSettings { get; set; }

        [JsonProperty]
        internal string Locale { get; set; }
    }
}
