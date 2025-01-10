namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// These chart types are used internally to map to the JSON properties on the VisualizationSettings element of the RDASH schema. 
    /// DO NOT USE IN PUBLIC API
    /// </summary>
    internal enum RdashChartType
    {
        Column,
        Line,
        Bar,
        Area,
        Spline,
        SplineArea,
        StepArea,
        StepLine,
        Composite,
        Bubble,
        Candlestick,
        OHLC,
        Pie,
        RadialLines,
        Scatter,
        StackedColumn,
        StackedArea,
        StackedBar,
        Doughnut,
        Funnel,
        TimeSeries,
    }
}
