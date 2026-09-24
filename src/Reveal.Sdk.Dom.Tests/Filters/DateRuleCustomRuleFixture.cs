using System;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateRuleCustomRuleFixture
    {
        [Fact]
        public void SetRelativePeriod_SerializesToExpectedJson()
        {
            // Mirrors the target JSON from RevealBi/Reveal.Sdk#713.
            var document = new RdashDocument("Custom rule test");
            var filter = new DashboardDateFilter("Date Filter")
                .SetRelativePeriod(PeriodRelation.Last, 3, PeriodType.Year);
            document.Filters.Add(filter);

            var json = JObject.Parse(document.ToJsonString());
            var f = json["GlobalFilters"]![0]!;

            Assert.Equal("DateGlobalFilterType", (string)f["_type"]!);
            Assert.Equal("_date", (string)f["Id"]!);
            Assert.Equal("Date Filter", (string)f["Title"]!);
            Assert.Equal("CustomRule", (string)f["RuleType"]!);
            Assert.True((bool)f["IncludeToday"]!);

            // Wire name stays "CustomRule" even though the API property is RelativePeriod.
            var rule = f["CustomRule"]!;
            Assert.Equal("DateRuleType", (string)rule["_type"]!);
            Assert.Equal("Last", (string)rule["Relation"]!);
            Assert.Equal(3, (int)rule["Count"]!);
            Assert.Equal("Year", (string)rule["Period"]!);
        }

        [Fact]
        public void DateFilter_WithoutRelativePeriod_OmitsCustomRuleProperty()
        {
            // A null RelativePeriod must not appear in the JSON (NullValueHandling.Ignore).
            var document = new RdashDocument("No custom rule");
            document.Filters.Add(new DashboardDateFilter("Date Filter") { RuleType = DateRuleType.LastYear });

            var json = JObject.Parse(document.ToJsonString());
            var f = json["GlobalFilters"]![0]!;

            Assert.Null(f["CustomRule"]);
        }

        [Fact]
        public void SetRelativePeriod_RoundTrips()
        {
            var document = new RdashDocument("Round trip");
            document.Filters.Add(new DashboardDateFilter("Date Filter")
                .SetRelativePeriod(PeriodRelation.Next, 7, PeriodType.Day, includeToday: false));

            var roundTripped = RdashDocument.LoadFromJson(document.ToJsonString());
            var filter = Assert.IsType<DashboardDateFilter>(roundTripped.Filters[0]);

            Assert.Equal(DateRuleType.CustomRule, filter.RuleType);
            Assert.NotNull(filter.RelativePeriod);
            Assert.Equal(PeriodRelation.Next, filter.RelativePeriod!.Relation);
            Assert.Equal(7, filter.RelativePeriod.Count);
            Assert.Equal(PeriodType.Day, filter.RelativePeriod.Period);
            Assert.False(filter.RelativePeriod.IncludeToday!.Value);
        }

        [Fact]
        public void SetRelativePeriod_ClearsAnyCustomRange()
        {
            var filter = new DashboardDateFilter("Date Filter")
                .SetCustomRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31))
                .SetRelativePeriod(PeriodRelation.Last, 3, PeriodType.Year);

            Assert.Equal(DateRuleType.CustomRule, filter.RuleType);
            Assert.NotNull(filter.RelativePeriod);
            Assert.Null(filter.CustomDateRange);
        }

        [Fact]
        public void SetCustomRange_SetsRuleTypeAndClearsRelativePeriod()
        {
            var filter = new DashboardDateFilter("Date Filter")
                .SetRelativePeriod(PeriodRelation.Last, 3, PeriodType.Year)
                .SetCustomRange(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));

            Assert.Equal(DateRuleType.CustomRange, filter.RuleType);
            Assert.Null(filter.RelativePeriod);
            Assert.NotNull(filter.CustomDateRange);
            Assert.Equal(new DateTime(2024, 1, 1), filter.CustomDateRange.From);
            Assert.Equal(new DateTime(2024, 12, 31), filter.CustomDateRange.To);
        }

        [Fact]
        public void SetRelativePeriod_WorksForAllDateRuleFilterTypes()
        {
            // The helper is defined once on IDateRuleFilter and applies to every date filter type.
            var dateTime = new DateTimeFilter().SetRelativePeriod(PeriodRelation.Last, 30, PeriodType.Day);
            var xmla = new XmlaDateFilter().SetRelativePeriod(PeriodRelation.Next, 2, PeriodType.Quarter);

            Assert.Equal(DateRuleType.CustomRule, dateTime.RuleType);
            Assert.Equal(30, dateTime.RelativePeriod!.Count);

            Assert.Equal(DateRuleType.CustomRule, xmla.RuleType);
            Assert.Equal(PeriodType.Quarter, xmla.RelativePeriod!.Period);
        }
    }
}
