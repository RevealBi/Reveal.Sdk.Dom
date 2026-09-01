using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateRuleCustomRuleFixture
    {
        [Fact]
        public void GlobalDateFilter_WithCustomRule_SerializesToExpectedJson()
        {
            // Mirrors the target JSON from RevealBi/Reveal.Sdk#713.
            var document = new RdashDocument("Custom rule test");
            document.Filters.Add(new DashboardDateFilter("Date Filter")
            {
                RuleType = DateRuleType.CustomRule,
                IncludeToday = true,
                CustomRule = new DateRule
                {
                    Relation = PeriodRelation.Last,
                    Count = 3,
                    Period = PeriodType.Year
                }
            });

            var json = JObject.Parse(document.ToJsonString());
            var filter = json["GlobalFilters"]![0]!;

            Assert.Equal("DateGlobalFilterType", (string)filter["_type"]!);
            Assert.Equal("_date", (string)filter["Id"]!);
            Assert.Equal("Date Filter", (string)filter["Title"]!);
            Assert.Equal("CustomRule", (string)filter["RuleType"]!);
            Assert.True((bool)filter["IncludeToday"]!);

            var rule = filter["CustomRule"]!;
            Assert.Equal("DateRuleType", (string)rule["_type"]!);
            Assert.Equal("Last", (string)rule["Relation"]!);
            Assert.Equal(3, (int)rule["Count"]!);
            Assert.Equal("Year", (string)rule["Period"]!);
        }

        [Fact]
        public void DateFilter_WithoutCustomRule_OmitsCustomRuleProperty()
        {
            // A null CustomRule must not appear in the JSON (NullValueHandling.Ignore).
            var document = new RdashDocument("No custom rule");
            document.Filters.Add(new DashboardDateFilter("Date Filter") { RuleType = DateRuleType.LastYear });

            var json = JObject.Parse(document.ToJsonString());
            var filter = json["GlobalFilters"]![0]!;

            Assert.Null(filter["CustomRule"]);
        }

        [Fact]
        public void GlobalDateFilter_CustomRule_RoundTrips()
        {
            var document = new RdashDocument("Round trip");
            document.Filters.Add(new DashboardDateFilter("Date Filter")
            {
                RuleType = DateRuleType.CustomRule,
                CustomRule = new DateRule { Relation = PeriodRelation.Next, Count = 7, Period = PeriodType.Day, IncludeToday = false }
            });

            var roundTripped = RdashDocument.LoadFromJson(document.ToJsonString());
            var filter = Assert.IsType<DashboardDateFilter>(roundTripped.Filters[0]);

            Assert.Equal(DateRuleType.CustomRule, filter.RuleType);
            Assert.NotNull(filter.CustomRule);
            Assert.Equal(PeriodRelation.Next, filter.CustomRule!.Relation);
            Assert.Equal(7, filter.CustomRule.Count);
            Assert.Equal(PeriodType.Day, filter.CustomRule.Period);
            Assert.False(filter.CustomRule.IncludeToday);
        }
    }
}
