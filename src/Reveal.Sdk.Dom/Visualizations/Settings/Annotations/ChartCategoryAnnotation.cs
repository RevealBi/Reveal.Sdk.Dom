using Newtonsoft.Json;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    /// <summary>
    /// An annotation anchored to a single position on the category axis.
    /// </summary>
    public sealed class ChartCategoryAnnotation : ChartAnnotation
    {
        /// <summary>
        /// Initializes a new category annotation at the default position.
        /// </summary>
        public ChartCategoryAnnotation() : base(ChartAnnotationType.Category) { }

        /// <summary>
        /// Initializes a new category annotation at the specified position.
        /// </summary>
        /// <param name="categoryValue">The position on the category axis.</param>
        public ChartCategoryAnnotation(double categoryValue) : this()
        {
            CategoryValue = categoryValue;
        }

        /// <summary>
        /// Gets or sets the annotation's position on the category axis.
        /// </summary>
        [JsonIgnore]
        public double CategoryValue
        {
            get => XValue;
            set => XValue = value;
        }
    }
}
