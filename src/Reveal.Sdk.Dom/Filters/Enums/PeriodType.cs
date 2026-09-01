namespace Reveal.Sdk.Dom.Filters
{
    // Mirrors the engine's Infragistics.ReportPlus.DashboardModel.PeriodType. Member names are
    // serialized verbatim (StringEnumConverter) into a DateRule's "Period", so they must match the
    // engine spelling exactly.
    public enum PeriodType
    {
        Day,
        /// <summary>Week (starting on Monday).</summary>
        Week,
        Month,
        Quarter,
        Semester,
        Year
    }
}
