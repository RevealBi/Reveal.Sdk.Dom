﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    internal class AssetVisualizationDataSpec : VisualizationDataSpec
    {
        public TabularColumn UrlColumn { get; set; }

        public AssetVisualizationDataSpec()
        {
            SchemaTypeName = SchemaTypeNames.AssetVisualizationDataSpecType;
        }
    }
}
