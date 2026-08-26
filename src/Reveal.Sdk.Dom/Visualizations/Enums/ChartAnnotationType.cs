using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// Identifies what a chart annotation is anchored to.
    /// </summary>
    public enum ChartAnnotationType
    {
        /// <summary>
        /// A data point in a chart series.
        /// </summary>
        [EnumMember(Value = "Point")]
        DataPoint,

        /// <summary>
        /// A single position on the category axis.
        /// </summary>
        [EnumMember(Value = "Slice")]
        Category,

        /// <summary>
        /// A range on the category axis.
        /// </summary>
        [EnumMember(Value = "Strip")]
        CategoryRange
    }
}
