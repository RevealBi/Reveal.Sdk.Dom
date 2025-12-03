using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class KpiTimeVisualizationSettings : KpiVisualizationSettingsBase
    {
        public KpiTimeVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.IndicatorVisualizationSettingsType;
            VisualizationType = VisualizationTypes.INDICATOR;
        }

        /// <summary>
        /// Gets or sets the time period to use when comparing values
        /// </summary>
        [JsonIgnore]
        public KpiTimePeriod TimePeriod
        {
            get { return ConvertIndicatorVisualizationTypeToKpiTimePeriod(VisualizationDataSpec.IndicatorType); }
            set { VisualizationDataSpec.IndicatorType = ConvertKpiTimePeriodToIndicatorVisualizationType(value); }
        }

        [JsonIgnore]
        internal IndicatorVisualizationDataSpec VisualizationDataSpec { get; set; }

        internal IndicatorVisualizationType ConvertKpiTimePeriodToIndicatorVisualizationType(KpiTimePeriod timePeriod)
        {
            return timePeriod switch
            {
                KpiTimePeriod.MonthToDatePreviousMonth => IndicatorVisualizationType.MonthToDatePreviousMonth,
                KpiTimePeriod.MonthToDatePreviousYear => IndicatorVisualizationType.MonthToDatePreviousYear,
                KpiTimePeriod.QuarterToDatePreviousQuarter => IndicatorVisualizationType.QuarterToDatePreviousQuarter,
                KpiTimePeriod.QuarterToDatePreviousYear => IndicatorVisualizationType.QuarterToDatePreviousYear,
                KpiTimePeriod.YearToDatePreviousYear => IndicatorVisualizationType.YearToDatePreviousYear,
                _ => IndicatorVisualizationType.MonthToDatePreviousMonth,
            };
        }

        internal KpiTimePeriod ConvertIndicatorVisualizationTypeToKpiTimePeriod(IndicatorVisualizationType visualizationType)
        {
            return visualizationType switch
            {
                IndicatorVisualizationType.MonthToDatePreviousMonth => KpiTimePeriod.MonthToDatePreviousMonth,
                IndicatorVisualizationType.MonthToDatePreviousYear => KpiTimePeriod.MonthToDatePreviousYear,
                IndicatorVisualizationType.QuarterToDatePreviousQuarter => KpiTimePeriod.QuarterToDatePreviousQuarter,
                IndicatorVisualizationType.QuarterToDatePreviousYear => KpiTimePeriod.QuarterToDatePreviousYear,
                IndicatorVisualizationType.YearToDatePreviousYear => KpiTimePeriod.YearToDatePreviousYear,
                _ => KpiTimePeriod.MonthToDatePreviousMonth
            };
        }
    }
}
