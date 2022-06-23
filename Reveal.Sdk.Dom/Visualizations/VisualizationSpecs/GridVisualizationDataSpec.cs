﻿using Reveal.Sdk.Dom.Visualizations.Primitives;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Visualizations.VisualizationSpecs
{
    public class GridVisualizationDataSpec : VisualizationDataSpec
    {
        public List<TabularColumnSpec> Columns { get; set; }

        public GridVisualizationDataSpec()
        {
            Columns = new List<TabularColumnSpec>();
        }
    }
}
