﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class HeatMapVisualizationSettings : MapBaseVisualizationSettings
    {
        public DashboardHeatMapLocationType LocationType { get; set; } = DashboardHeatMapLocationType.LatitudeLongitudeSingleField;
        public HeatMapLayersSettings Layers { get; set; } = new HeatMapLayersSettings();

        public HeatMapVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.HeatMapVisualizationSettingsType;
        }
    }

    public enum DashboardHeatMapLocationType
    {
        LatitudeLongitudeSingleField,
        LatitudeLongitudeFields
    }

    public class HeatMapLayersSettings
    {
        public bool PinsLayerEnabled { get; set; } = true;
        public bool HeatMapLayerEnabled { get; set; }
    }
}
