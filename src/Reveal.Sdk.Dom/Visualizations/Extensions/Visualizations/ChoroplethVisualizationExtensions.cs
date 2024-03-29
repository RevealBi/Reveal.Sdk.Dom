﻿using Reveal.Sdk.Dom.Visualizations.Settings;
using System;

namespace Reveal.Sdk.Dom.Visualizations
{
    public static class ChoroplethVisualizationExtensions
    {
        public static ChoroplethVisualization SetLocation(this ChoroplethVisualization visualization, string field)
        {
            return visualization.SetLocation(new TextDataField(field));
        }
        
        public static ChoroplethVisualization SetLocation(this ChoroplethVisualization visualization, DimensionDataField field)
        {
            visualization.Locations.Clear();
            visualization.Locations.Add(new DimensionColumn()
            {
                DataField = field
            });
            return visualization;
        }

        public static ChoroplethVisualization ConfigureSettings(this ChoroplethVisualization visualization, Action<ChoroplethVisualizationSettings> settings)
        {
            return visualization.ConfigureSettings<ChoroplethVisualization, ChoroplethVisualizationSettings>(settings);
        }
    }
}
