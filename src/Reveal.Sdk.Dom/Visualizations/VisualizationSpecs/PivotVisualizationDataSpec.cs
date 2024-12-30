using Reveal.Sdk.Dom.Core.Constants;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
	internal class PivotVisualizationDataSpec : HierarchyVisualizationDataSpec
    {        
		public List<DimensionColumn> Columns { get; set; } = new List<DimensionColumn>();
		public List<MeasureColumn> Values { get; set; } = new List<MeasureColumn>();
		public bool ShowGrandTotals { get; set; }

		public PivotVisualizationDataSpec()
		{
			SchemaTypeName = SchemaTypeNames.PivotVisualizationDataSpecType;
		}
	}
}
