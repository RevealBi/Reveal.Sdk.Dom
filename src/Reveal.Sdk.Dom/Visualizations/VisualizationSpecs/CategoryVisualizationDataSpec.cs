using Reveal.Sdk.Dom.Core.Constants;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    internal class CategoryVisualizationDataSpec : LabelsVisualizationDataSpec
    {
		public DimensionColumn Category { get; set; }

		public List<MeasureColumn> Values { get; set; } = new List<MeasureColumn>();

		public CategoryVisualizationDataSpec()
		{
			SchemaTypeName = SchemaTypeNames.CategoryVisualizationDataSpecType;
		}
	}
}
