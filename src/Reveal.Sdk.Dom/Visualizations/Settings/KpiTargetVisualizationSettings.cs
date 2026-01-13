using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class KpiTargetVisualizationSettings : KpiVisualizationSettingsBase
    {
        public KpiTargetVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.IndicatorTargetVisualizationSettingsType;
            VisualizationType = VisualizationTypes.INDICATOR_TARGET;
        }

        /// <summary>
        /// Gets or sets the goal period.
        /// </summary>
        [JsonIgnore]
        public KpiGoalPeriod GoalPeriod 
        {
            get { return VisualizationDataSpec.DateFilterType; }
            set { VisualizationDataSpec.DateFilterType = value; } 
        }

        [JsonIgnore]
        internal IndicatorTargetVisualizationDataSpec VisualizationDataSpec { get; set; }
    }
}
