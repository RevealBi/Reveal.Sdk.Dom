using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
	public class HierarchyVisualizationDataSpec : VisualizationDataSpec
    {
		public int? AdHocFields { get; set; }
		public int FormatVersion { get; set; }
		public List<AdHocExpandedElement> AdHocExpandedElements { get; set; } = new List<AdHocExpandedElement>();
		public List<DimensionColumn> Rows { get; set; } = new List<DimensionColumn>();
	}
}
