using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// An annotation anchored to a data point in a chart series.
    /// </summary>
    public sealed class ChartDataPointAnnotation : ChartAnnotation
    {
        /// <summary>
        /// Initializes a new data-point annotation at the default position.
        /// </summary>
        public ChartDataPointAnnotation() : base(ChartAnnotationType.DataPoint) { }

        /// <summary>
        /// Initializes a new data-point annotation at the specified category and value coordinates.
        /// </summary>
        /// <param name="categoryValue">The position on the category axis.</param>
        /// <param name="value">The position on the value axis.</param>
        public ChartDataPointAnnotation(double categoryValue, double value) : this()
        {
            CategoryValue = categoryValue;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the last-known zero-based index of the target series.
        /// The series field is used first when it is available.
        /// </summary>
        [JsonIgnore]
        public int SeriesIndex
        {
            get => TargetSeriesIndex;
            set => TargetSeriesIndex = value;
        }

        /// <summary>
        /// Gets or sets the data field key that identifies the target series.
        /// </summary>
        [JsonIgnore]
        public string SeriesField
        {
            get => TargetSeriesField;
            set => TargetSeriesField = value;
        }

        /// <summary>
        /// Gets or sets the point's position on the category axis.
        /// </summary>
        [JsonIgnore]
        public double CategoryValue
        {
            get => XValue;
            set => XValue = value;
        }

        /// <summary>
        /// Gets or sets the point's value on the value axis.
        /// </summary>
        [JsonIgnore]
        public double Value
        {
            get => YValue;
            set => YValue = value;
        }
    }
}
