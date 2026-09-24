namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// Internal seam over the raw date-rule schema fields shared by <see cref="DashboardDateFilter"/>,
    /// <see cref="DateTimeFilter"/>, and <see cref="XmlaDateFilter"/>, so the mapping to and from the
    /// public <see cref="DateFilterRule"/> is written once. Not part of the public API — user code uses
    /// each filter's <c>Rule</c> property.
    /// </summary>
    internal interface IDateRuleFilter
    {
        DateRuleType RuleType { get; set; }
        RelativePeriod CustomRule { get; set; }
        DateRange CustomDateRange { get; set; }
    }
}
