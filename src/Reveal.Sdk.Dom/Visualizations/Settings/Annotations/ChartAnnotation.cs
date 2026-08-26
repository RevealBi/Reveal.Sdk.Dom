using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// Base class for annotations displayed on category charts.
    /// </summary>
    [JsonConverter(typeof(ChartAnnotationConverter))]
    public abstract class ChartAnnotation : SchemaType
    {
        /// <summary>
        /// Initializes a new chart annotation of the specified type.
        /// </summary>
        /// <param name="type">The annotation's chart anchor type.</param>
        protected ChartAnnotation(ChartAnnotationType type)
        {
            SchemaTypeName = SchemaTypeNames.ChartAnnotationType;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the persistent identifier used to update this annotation.
        /// </summary>
        [JsonProperty("Identifier")]
        public string Id { get; set; }

        /// <summary>
        /// Gets the kind of chart anchor represented by this annotation.
        /// </summary>
        [JsonProperty("AnnotationType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChartAnnotationType Type { get; private set; }

        /// <summary>
        /// Gets or sets the annotation title.
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the annotation description.
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the serialized target-series index.
        /// </summary>
        [JsonProperty("TargetSeriesIndex")]
        protected int TargetSeriesIndex { get; set; }

        /// <summary>
        /// Gets or sets the serialized target-series field key.
        /// </summary>
        [JsonProperty("TargetSeriesField")]
        protected string TargetSeriesField { get; set; }

        /// <summary>
        /// Gets or sets the serialized category-axis position.
        /// </summary>
        [JsonProperty("XValue")]
        protected double XValue { get; set; }

        /// <summary>
        /// Gets or sets the serialized value-axis position.
        /// </summary>
        [JsonProperty("YValue")]
        protected double YValue { get; set; }

        /// <summary>
        /// Gets or sets the serialized category-range start.
        /// </summary>
        [JsonProperty("StartValue")]
        protected double RangeStartValue { get; set; }

        /// <summary>
        /// Gets or sets the serialized category-range end.
        /// </summary>
        [JsonProperty("EndValue")]
        protected double RangeEndValue { get; set; }

        /// <summary>
        /// Gets or sets the annotation marker color as an RGB hex string.
        /// When omitted, no marker is displayed.
        /// </summary>
        [JsonProperty("MarkerColor")]
        public string MarkerColor { get; set; }
    }
}
