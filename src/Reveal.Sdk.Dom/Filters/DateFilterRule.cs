using System;

namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// The date selection applied by a date filter — a relative window ("Last 3 Years", "Next 7 Days"),
    /// an explicit range, or all time. Create one with the factory methods and assign it to a filter's
    /// <c>Rule</c> property (or pass it to the filter's constructor).
    /// </summary>
    /// <example>
    /// <code>
    /// var filter = new DashboardDateFilter(DateFilterRule.Next(7, PeriodType.Day));
    /// filter.Rule = DateFilterRule.This(PeriodType.Quarter);
    /// </code>
    /// </example>
    public sealed class DateFilterRule
    {
        // The rule's serialized shape lives on the filter (RuleType + CustomRule + CustomDateRange).
        // This type is the public, immutable facade over that trio.
        internal DateRuleType RuleType { get; private set; }
        internal RelativePeriod RelativePeriod { get; private set; }
        internal DateRange CustomDateRange { get; private set; }

        private DateFilterRule() { }

        /// <summary>The last <paramref name="count"/> <paramref name="period"/>(s) up to today (e.g. Last 3 Years).</summary>
        /// <param name="includeToday">Whether today is included in the window.</param>
        public static DateFilterRule Last(int count, PeriodType period, bool includeToday = true)
            => Relative(PeriodRelation.Last, count, period, includeToday);

        /// <summary>The next <paramref name="count"/> <paramref name="period"/>(s) starting from tomorrow (e.g. Next 7 Days).</summary>
        public static DateFilterRule Next(int count, PeriodType period)
            => Relative(PeriodRelation.Next, count, period, null);

        /// <summary>The <paramref name="count"/> full <paramref name="period"/>(s) immediately before the current one.</summary>
        public static DateFilterRule Previous(int count, PeriodType period)
            => Relative(PeriodRelation.Previous, count, period, null);

        /// <summary>The current <paramref name="period"/> in full (e.g. This Quarter).</summary>
        public static DateFilterRule This(PeriodType period)
            => Relative(PeriodRelation.This, 1, period, null);

        /// <summary>The current <paramref name="period"/> from its start up to today (e.g. Year to date).</summary>
        /// <param name="includeToday">Whether today is included in the window.</param>
        public static DateFilterRule ToDate(PeriodType period, bool includeToday = true)
            => Relative(PeriodRelation.ToDate, 1, period, includeToday);

        /// <summary>An explicit date range.</summary>
        public static DateFilterRule Custom(DateTime from, DateTime to)
            => new DateFilterRule
            {
                RuleType = DateRuleType.CustomRange,
                CustomDateRange = new DateRange { From = from, To = to }
            };

        /// <summary>Matches all dates (no date restriction).</summary>
        public static DateFilterRule AllTime => new DateFilterRule { RuleType = DateRuleType.AllTime };

        private static DateFilterRule Relative(PeriodRelation relation, int count, PeriodType period, bool? includeToday)
            => new DateFilterRule
            {
                RuleType = DateRuleType.CustomRule,
                RelativePeriod = new RelativePeriod(relation, count, period, includeToday)
            };

        // Rebuilds the facade from a filter's raw fields (used by the filter's Rule getter).
        internal static DateFilterRule FromFilter(IDateRuleFilter filter)
            => new DateFilterRule
            {
                RuleType = filter.RuleType,
                RelativePeriod = filter.CustomRule,
                CustomDateRange = filter.CustomDateRange
            };

        // Writes this rule onto a filter's raw fields (used by the filter's Rule setter).
        internal void ApplyTo(IDateRuleFilter filter)
        {
            filter.RuleType = RuleType;
            filter.CustomRule = RelativePeriod;
            filter.CustomDateRange = CustomDateRange;
        }
    }
}
