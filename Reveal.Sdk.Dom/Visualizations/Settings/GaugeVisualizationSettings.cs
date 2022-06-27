﻿using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.Primitives;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public class GaugeVisualizationSettings : VisualizationSettings
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        internal GaugeViewType ViewType { get; set; } = GaugeViewType.Circular;
        public Bound Minimum { get; set; }
        public Bound Maximum { get; set; }

        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        internal List<GaugeBand> GaugeBands { get; set; } = new List<GaugeBand>()
        {
            new GaugeBand()
            {
                Color = BandColorType.Green,
                Value = 80.0,
            },
            new GaugeBand()
            {
                Color = BandColorType.Yellow,
                Value = 50.0,
            },
            new GaugeBand()
            {
                Color = BandColorType.Red,
            },
        };

        public string ValueColumnName { get; set; }
        public string LabelColumnName { get; set; }
        public string TargetColumnName { get; set; }
        public bool SingleValueFormattingEnabled { get; set; } = true;

        public GaugeVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.GaugeVisualizationSettingsType;
            VisualizationType = VisualizationTypes.GAUGE;
        }
    }
}
