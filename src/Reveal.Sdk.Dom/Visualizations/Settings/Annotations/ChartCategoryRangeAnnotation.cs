using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// An annotation anchored to a range on the category axis.
    /// </summary>
    public sealed class ChartCategoryRangeAnnotation : ChartAnnotation
    {
        /// <summary>
        /// Initializes a new category-range annotation at the default positions.
        /// </summary>
        public ChartCategoryRangeAnnotation() : base(ChartAnnotationType.CategoryRange) { }

        /// <summary>
        /// Initializes a new category-range annotation with the specified bounds.
        /// </summary>
        /// <param name="startValue">The beginning of the range on the category axis.</param>
        /// <param name="endValue">The end of the range on the category axis.</param>
        public ChartCategoryRangeAnnotation(double startValue, double endValue) : this()
        {
            StartValue = startValue;
            EndValue = endValue;
        }

        /// <summary>
        /// Gets or sets the beginning of the range on the category axis.
        /// </summary>
        [JsonIgnore]
        public double StartValue
        {
            get => RangeStartValue;
            set => RangeStartValue = value;
        }

        /// <summary>
        /// Gets or sets the end of the range on the category axis.
        /// </summary>
        [JsonIgnore]
        public double EndValue
        {
            get => RangeEndValue;
            set => RangeEndValue = value;
        }
    }
}
