namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// A date filter that supports rule-based windows — a <see cref="DateRuleType"/> with an optional
    /// custom range or <see cref="RelativePeriod"/>. Implemented by <see cref="DashboardDateFilter"/>,
    /// <see cref="DateTimeFilter"/>, and <see cref="XmlaDateFilter"/> so the rule-building helpers in
    /// <see cref="IDateRuleFilterExtensions"/> are written once.
    /// </summary>
    public interface IDateRuleFilter
    {
        /// <summary>The kind of date rule the filter applies.</summary>
        DateRuleType RuleType { get; set; }

        /// <summary>The explicit range used when <see cref="RuleType"/> is <see cref="DateRuleType.CustomRange"/>.</summary>
        DateRange CustomDateRange { get; set; }

        /// <summary>The relative window used when <see cref="RuleType"/> is <see cref="DateRuleType.CustomRule"/>.
        /// Set it through <see cref="IDateRuleFilterExtensions.SetRelativePeriod{T}"/>.</summary>
        RelativePeriod RelativePeriod { get; }

        /// <summary>Whether today is included, for the built-in relative rule types.</summary>
        bool IncludeToday { get; set; }
    }
}
