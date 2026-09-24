using System;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateRuleCustomRuleFixture
    {
        [Fact]
        public void Constructor_WithRelativeRule_SerializesToExpectedJson()
        {
            // Mirrors the target JSON from RevealBi/Reveal.Sdk#713.
            var document = new RdashDocument("Custom rule test");
            document.Filters.Add(new DashboardDateFilter(DateFilterRule.Last(3, PeriodType.Year)));

            var f = JObject.Parse(document.ToJsonString())["GlobalFilters"]![0]!;

            Assert.Equal("DateGlobalFilterType", (string)f["_type"]!);
            Assert.Equal("_date", (string)f["Id"]!);
            Assert.Equal("Date Filter", (string)f["Title"]!);
            Assert.Equal("CustomRule", (string)f["RuleType"]!);

            // Wire name stays "CustomRule" even though the public API is DateFilterRule.
            var rule = f["CustomRule"]!;
            Assert.Equal("DateRuleType", (string)rule["_type"]!);
            Assert.Equal("Last", (string)rule["Relation"]!);
            Assert.Equal(3, (int)rule["Count"]!);
            Assert.Equal("Year", (string)rule["Period"]!);
        }

        [Fact]
        public void Constructor_WithTitleAndRule_UsesTitle()
        {
            var filter = new DashboardDateFilter("Sales Date", DateFilterRule.Next(7, PeriodType.Day));
            Assert.Equal("Sales Date", filter.Title);

            var document = new RdashDocument("t");
            document.Filters.Add(filter);
            var f = JObject.Parse(document.ToJsonString())["GlobalFilters"]![0]!;
            var rule = f["CustomRule"]!;
            Assert.Equal("Next", (string)rule["Relation"]!);
            Assert.Equal(7, (int)rule["Count"]!);
            // Next has no includeToday (only valid for Last/ToDate), so it's omitted.
            Assert.Null(rule["IncludeToday"]);
        }

        [Fact]
        public void DateFilter_WithBuiltInRule_OmitsCustomRuleProperty()
        {
            // AllTime is a built-in rule with no relative period, so CustomRule is omitted.
            var document = new RdashDocument("No custom rule");
            document.Filters.Add(new DashboardDateFilter(DateFilterRule.AllTime));

            var f = JObject.Parse(document.ToJsonString())["GlobalFilters"]![0]!;

            Assert.Equal("AllTime", (string)f["RuleType"]!);
            Assert.Null(f["CustomRule"]);
        }

        [Fact]
        public void RelativeRule_RoundTrips_WithIncludeToday()
        {
            var document = new RdashDocument("Round trip");
            document.Filters.Add(new DashboardDateFilter(DateFilterRule.Last(90, PeriodType.Day, includeToday: false)));

            var roundTripped = RdashDocument.LoadFromJson(document.ToJsonString());
            var filter = Assert.IsType<DashboardDateFilter>(roundTripped.Filters[0]);

            // Internal raw fields are visible to the test project (InternalsVisibleTo).
            Assert.Equal(DateRuleType.CustomRule, filter.RuleType);
            Assert.NotNull(filter.CustomRule);
            Assert.Equal(PeriodRelation.Last, filter.CustomRule.Relation);
            Assert.Equal(90, filter.CustomRule.Count);
            Assert.Equal(PeriodType.Day, filter.CustomRule.Period);
            Assert.False(filter.CustomRule.IncludeToday!.Value);
        }

        [Fact]
        public void CustomRange_RoundTrips()
        {
            var document = new RdashDocument("cr");
            document.Filters.Add(new DashboardDateFilter(DateFilterRule.Custom(new DateTime(2023, 1, 1), new DateTime(2023, 6, 30))));

            var f = JObject.Parse(document.ToJsonString())["GlobalFilters"]![0]!;
            Assert.Equal("CustomRange", (string)f["RuleType"]!);
            Assert.Null(f["CustomRule"]);
            Assert.NotNull(f["CustomDateRange"]);
        }

        [Fact]
        public void SettingRule_ReplacesThePreviousSelection()
        {
            // Range first, then a relative rule: the range must be cleared, and vice versa.
            var filter = new DashboardDateFilter(DateFilterRule.Custom(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)));
            Assert.Equal(DateRuleType.CustomRange, filter.RuleType);
            Assert.NotNull(filter.CustomDateRange);

            filter.Rule = DateFilterRule.This(PeriodType.Quarter);
            Assert.Equal(DateRuleType.CustomRule, filter.RuleType);
            Assert.Null(filter.CustomDateRange);
            Assert.Equal(PeriodRelation.This, filter.CustomRule.Relation);
        }

        [Fact]
        public void Rule_WorksForAllDateFilterTypes()
        {
            var dateTime = new DateTimeFilter(DateFilterRule.Last(30, PeriodType.Day));
            var xmla = new XmlaDateFilter(DateFilterRule.Next(2, PeriodType.Quarter));

            Assert.Equal(DateRuleType.CustomRule, dateTime.RuleType);
            Assert.Equal(30, dateTime.CustomRule.Count);

            Assert.Equal(DateRuleType.CustomRule, xmla.RuleType);
            Assert.Equal(PeriodType.Quarter, xmla.CustomRule.Period);
        }

        [Fact]
        public void Constructor_NullRule_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new DashboardDateFilter((DateFilterRule)null!));
        }
    }
}
