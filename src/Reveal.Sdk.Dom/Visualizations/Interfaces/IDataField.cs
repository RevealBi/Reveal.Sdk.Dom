using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Visualizations
{
    [JsonConverter(typeof(DataFieldConverter))]
    public interface IDataField
    {
        /// <summary>
        /// Gets or sets the field name of the data field.
        /// </summary>
        string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the description of the data field.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the axis title.
        /// </summary>
        string AxisTitle { get; set; }
    }
}
