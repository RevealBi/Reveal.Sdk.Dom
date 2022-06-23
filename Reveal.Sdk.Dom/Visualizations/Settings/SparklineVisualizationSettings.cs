﻿using Reveal.Sdk.Dom.Core.Constants;
using System.Text.Json.Serialization;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public class SparklineVisualizationSettings : GridVisualizationSettings
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChartType ChartType { get; set; } = ChartType.Line;
        public bool ShowLastTwoValues { get; set; } = true;
        public bool ShowDifference { get; set; } = true;
        public bool PositiveIsRed { get; set; }

        public SparklineVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.SparklineVisualizationSettingsType;
            VisualizationType = VisualizationTypes.SPARKLINE;
        }
    }
}
