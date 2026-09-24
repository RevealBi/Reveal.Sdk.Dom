using System;

namespace Reveal.Sdk.Dom.Filters
{
    /// <summary>
    /// Fluent helpers for applying date rules to any <see cref="IDateRuleFilter"/>. These keep the
    /// filter's <see cref="IDateRuleFilter.RuleType"/>, <see cref="IDateRuleFilter.CustomDateRange"/>,
    /// and <see cref="IDateRuleFilter.RelativePeriod"/> consistent so an invalid combination can't be
    /// produced through the public API.
    /// </summary>
    public static class IDateRuleFilterExtensions
    {
        /// <summary>
        /// Applies a relative window (e.g. <c>SetRelativePeriod(PeriodRelation.Last, 3, PeriodType.Year)</c>).
        /// Sets <see cref="IDateRuleFilter.RuleType"/> to <see cref="DateRuleType.CustomRule"/> and clears
        /// any custom range.
        /// </summary>
        /// <param name="includeToday">Whether today is included; only meaningful for
        /// <see cref="PeriodRelation.Last"/> and <see cref="PeriodRelation.ToDate"/>.</param>
        public static T SetRelativePeriod<T>(this T filter, PeriodRelation relation, int count = 1, PeriodType period = PeriodType.Day, bool? includeToday = null)
            where T : IDateRuleFilter
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            filter.RuleType = DateRuleType.CustomRule;
            filter.CustomDateRange = null;
            filter.SetRelativePeriodCore(new RelativePeriod(relation, count, period, includeToday));
            return filter;
        }

        /// <summary>
        /// Applies an explicit date range. Sets <see cref="IDateRuleFilter.RuleType"/> to
        /// <see cref="DateRuleType.CustomRange"/> and clears any relative period.
        /// </summary>
        public static T SetCustomRange<T>(this T filter, DateTime from, DateTime to)
            where T : IDateRuleFilter
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            filter.RuleType = DateRuleType.CustomRange;
            filter.CustomDateRange = new DateRange { From = from, To = to };
            filter.SetRelativePeriodCore(null);
            return filter;
        }

        // Writes the internal-set RelativePeriod on the concrete filter types. Kept in one place so the
        // three filter classes carry no per-type rule-setting code.
        internal static void SetRelativePeriodCore(this IDateRuleFilter filter, RelativePeriod period)
        {
            switch (filter)
            {
                case DashboardDateFilter d: d.RelativePeriod = period; break;
                case DateTimeFilter dt: dt.RelativePeriod = period; break;
                case XmlaDateFilter x: x.RelativePeriod = period; break;
            }
        }
    }
}
