﻿using Reveal.Sdk.Dom.Core.Constants;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    internal class SingleValueLabelsVisualizationDataSpec : LabelsVisualizationDataSpec
    {
        public List<MeasureColumnSpec> Value { get; set; } = new List<MeasureColumnSpec>();

        public SingleValueLabelsVisualizationDataSpec()
        {
            SchemaTypeName = SchemaTypeNames.SingleValueLabelsVisualizationDataSpecType;
        }
    }
}