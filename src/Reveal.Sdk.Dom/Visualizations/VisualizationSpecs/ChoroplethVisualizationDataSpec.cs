﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    internal class ChoroplethVisualizationDataSpec : SingleValueLabelsVisualizationDataSpec
    {
        public DimensionColumn MapColor { get; set; }

        public ChoroplethVisualizationDataSpec()
        {
            SchemaTypeName = SchemaTypeNames.ChoroplethMapVisualizationDataSpecType;
        }
    }
}
