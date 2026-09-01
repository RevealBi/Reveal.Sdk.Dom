using System;

namespace Reveal.Sdk.Dom.Core.Constants
{
    //these constants are needed in order to map existing json Schema names to new API names, otherwise we could just generate them in code
    internal class SchemaTypeNames
    {
        //******************* DataSourcs *******************
        internal const string DataSourceType = "DataSourceType";
        internal const string DataSourceItemType = "DataSourceItemType";

        //******************* FILTERS *******************
        internal const string DateGlobalFilterType = "DateGlobalFilterType";
        internal const string TabularGlobalFilterType = "TabularGlobalFilterType";
        internal const string NumberFilterType = "NumberFilterType";
        internal const string DateTimeFilterType = "DateTimeFilterType";
        internal const string StringFilterType = "StringFilterType";
        internal const string TimeFilterType = "TimeFilterType";
        internal const string XmlaGlobalFilterType = "XmlaGlobalFilterType";
        internal const string XmlaDateFilterType = "XmlaDateFilterType";
        internal const string DateRuleType = "DateRuleType";
        internal const string XmlaRegularFilterType = "XmlaRegularFilterType";
        internal const string XmlaStringFilterRuleType = "XmlaStringFilterRuleType";
        internal const string XmlaNumberFilterRuleType = "XmlaNumberFilterRuleType";

        //******************* Visualizations - Settings *******************
        internal const string AssetVisualizationSettingsType = "AssetVisualizationSettingsType";
        internal const string ChartVisualizationSettingsType = "ChartVisualizationSettingsType";
        internal const string ChoroplethMapVisualizationSettingsType = "ChoroplethMapVisualizationSettingsType";
        internal const string DiyVisualizationSettingsType = "DIYVisualizationSettingsType";
        internal const string GaugeVisualizationSettingsType = "GaugeVisualizationSettingsType";
        internal const string GeoMapBaseVisualizationSettingsType = "GeoMapBaseVisualizationSettingsType";
        internal const string GridVisualizationSettingsType = "GridVisualizationSettingsType";
        internal const string HeatMapVisualizationSettingsType = "HeatMapVisualizationSettingsType";
        internal const string IndicatorVisualizationSettingsType = "IndicatorVisualizationSettingsType";
        internal const string IndicatorTargetVisualizationSettingsType = "IndicatorTargetVisualizationSettingsType";
        internal const string MapVisualizationSettingsType = "MapVisualizationSettingsType";
        internal const string PivotVisualizationSettingsType = "PivotVisualizationSettingsType";
        internal const string ScatterMapVisualizationSettingsType = "ScatterMapVisualizationSettingsType";
        internal const string ScriptVisualizationSettingsType = "ScriptVisualizationSettingsType";
        internal const string SparklineVisualizationSettingsType = "SparklineVisualizationSettingsType";
        internal const string TextBoxVisualizationSettingsType = "TextBoxVisualizationSettingsType";
        internal const string TreeMapVisualizationSettingsType = "TreeMapVisualizationSettingsType";

        //******************* Visualizations - Specs *******************
        internal const string AssetVisualizationDataSpecType = "AssetVisualizationDataSpecType";
        internal const string BubbleVisualizationDataSpecType = "BubbleVisualizationDataSpecType";
        internal const string ChoroplethMapVisualizationDataSpecType = "ChoroplethMapVisualizationDataSpecType";
        internal const string CompositeChartVisualizationDataSpecType = "CompositeChartVisualizationDataSpecType";
        internal const string FinancialVisualizationDataSpecType = "FinancialVisualizationDataSpecType";
        internal const string GridVisualizationDataSpecType = "GridVisualizationDataSpecType";
        internal const string IndicatorBaseVisualizationDataSpecType = "IndicatorBaseVisualizationDataSpecType";
        internal const string LinearGaugeVisualizationDataSpecType = "LinearGaugeVisualizationDataSpecType";
        internal const string ScatterMapVisualizationDataSpecType = "ScatterMapVisualizationDataSpecType";
        internal const string ScatterVisualizationDataSpecType = "ScatterVisualizationDataSpecType";
        internal const string SingleGaugeVisualizationDataSpecType = "SingleGaugeVisualizationDataSpecType";
        internal const string SingleValueCategoryVisualizationDataSpecType = "SingleValueCategoryVisualizationDataSpecType";
        internal const string SingleValueVisualizationDataSpecType = "SingleValueVisualizationDataSpecType";
        internal const string TreeMapVisualizationDataSpecType = "TreeMapVisualizationDataSpecType";
        internal const string CategoryVisualizationDataSpecType = "CategoryVisualizationDataSpecType";
        internal const string IndicatorVisualizationDataSpecType = "IndicatorVisualizationDataSpecType";
        internal const string IndicatorTargetVisualizationDataSpecType = "IndicatorTargetVisualizationDataSpecType";
        internal const string PivotVisualizationDataSpecType = "PivotVisualizationDataSpecType";
        internal const string SingleRowVisualizationSettingsType = "SingleRowVisualizationSettingsType";
        internal const string SingleValueLabelsVisualizationDataSpecType = "SingleValueLabelsVisualizationDataSpecType";
        internal const string SparklineVisualizationDataSpecType = "SparklineVisualizationDataSpecType";
        internal const string TimeSeriesVisualizationDataSpecType = "TimeSeriesVisualizationDataSpecType";

        internal const string DateFormattingSpecType = "DateFormattingSpecType";
        internal const string DimensionColumnSpecType = "DimensionColumnSpecType";
        internal const string MeasureColumnSpecType = "MeasureColumnSpecType";
        internal const string TabularDataSpecType = "TabularDataSpecType";
        internal const string NumberFormattingSpecType = "NumberFormattingSpecType";
        internal const string ResourceDataSpecType = "ResourceDataSpecType";
        internal const string TextBoxDataSpecType = "TextBoxDataSpecType";
        internal const string XmlaDataSpecType = "XmlaDataSpecType";

        //******************* XMLA *****************
        internal const string XmlaDimensionType = "XmlaDimensionType";
        internal const string XmlaHierarchyLevelType = "XmlaHierarchyLevelType";
        internal const string XmlaHierarchyType = "XmlaHierarchyType";
        internal const string XmlaSetType = "XmlaSetType";

        //******************* Chart Annotations *****************
        internal const string ChartAnnotationType = "ChartAnnotationType";

        //******************* Fixed Lines *****************
        internal const string FixedLineAverageType = "FixedLineAverageType";
        internal const string FixedLineDataType = "FixedLineDataType";
        internal const string FixedLineFixedValueType = "FixedLineFixedValueType";
        internal const string FixedLineHighestType = "FixedLineHighestType";
        internal const string FixedLineLowestType = "FixedLineLowestType";



        //******************* not sure how these should be classified yet *****************
        internal const string FieldBindingSourceType = "FieldBindingSourceType";
        internal const string ParameterBindingSourceType = "ParameterBindingSourceType";
        internal const string DateGlobalFilterBindingTargetType = "DateGlobalFilterBindingTargetType";
        internal const string DateTimeFieldSettingsType = "DateTimeFieldSettingsType";
        internal const string SummarizationDateFieldType = "SummarizationDateFieldType";
        internal const string SummarizationRegularFieldType = "SummarizationRegularFieldType";
        internal const string SummarizationValueFieldType = "SummarizationValueFieldType";
        internal const string CompositeDataSourceItemType = "CompositeDataSourceItemType";
        internal const string DataBasedGlobalFilterBindingTargetType = "DataBasedGlobalFilterBindingTargetType";
        internal const string GlobalVariableBindingTargetType = "GlobalVariableBindingTargetType";
        internal const string GaugeBandType = "GaugeBandType";
        internal const string ConditionalFormattingBandType = "ConditionalFormattingBandType";
        internal const string TabularColumnSpecType = "TabularColumnSpecType";
        
    }
}
