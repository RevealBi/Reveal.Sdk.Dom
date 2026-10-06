using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateFilterApiFixture
    {
        private static readonly Type[] FilterTypes =
        {
            typeof(DashboardDateFilter), typeof(DateTimeFilter), typeof(XmlaDateFilter)
        };

        public static IEnumerable<object[]> Filters => FilterTypes.Select(type => new object[] { type });

        public static IEnumerable<object[]> LegacyRules =>
            from type in FilterTypes
            from rule in Enum.GetNames(typeof(DateRuleType)).Where(name => name != "CustomRule")
            from includeToday in new[] { false, true }
            select new object[] { type, rule, includeToday };

        public static IEnumerable<object[]> RelativeRules =>
            from type in FilterTypes
            from relation in new[] { "All", "Last", "Previous", "ToDate", "This", "Next" }
            from period in Enum.GetNames(typeof(PeriodType))
            from includeToday in new bool?[] { null, false, true }
            select new object[] { type, relation, period, includeToday };

        [Theory]
        [MemberData(nameof(Filters))]
        public void PublicApi_RequiresRuleAndHidesWireMembers(Type type)
        {
            Assert.All(type.GetConstructors(), ctor =>
                Assert.Contains(ctor.GetParameters(), parameter => parameter.ParameterType == typeof(DateFilterRule)));
            Assert.Equal(type == typeof(DashboardDateFilter) ? 2 : 1, type.GetConstructors().Length);
            foreach (var name in new[] { "RuleType", "CustomDateRange", "IncludeToday", "CustomRule", "RelativePeriod" })
                Assert.Null(type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public));

            Assert.True(type.GetProperty("Rule").CanWrite);
            Assert.Equal(type, type.GetMethod("SetRule").ReturnType);
            Assert.Empty(typeof(DateFilterRule).GetConstructors());
            Assert.DoesNotContain(typeof(DateFilterRule).GetProperties(), property => property.SetMethod?.IsPublic == true);
            var exported = typeof(DateFilterRule).Assembly.GetExportedTypes();
            Assert.DoesNotContain(exported, t => new[] { "DateRuleType", "RelativePeriod", "PeriodRelation", "IDateRuleFilter" }.Contains(t.Name));
        }

        [Theory]
        [MemberData(nameof(LegacyRules))]
        public void LegacyJson_PreservesRuleAndIncludeTodayOnRoundTripAndCopy(Type type, string rule, bool includeToday)
        {
            var json = FilterJson(type, rule);
            json["IncludeToday"] = includeToday;
            if (rule == "CustomRange")
                json["CustomDateRange"] = new JObject
                {
                    ["From"] = new JObject { ["_type"] = "date", ["value"] = "2024-01-01T00:00:00" },
                    ["To"] = new JObject { ["_type"] = "date", ["value"] = "2024-12-31T00:00:00" }
                };

            var loaded = Load(type, json);
            var serialized = Serialize(loaded);
            Assert.Equal(rule, (string)serialized["RuleType"]);
            Assert.Equal(includeToday, (bool)serialized["IncludeToday"]);
            Assert.Null(serialized["CustomRule"]);
            Assert.Null(serialized["Rule"]);
            Assert.True(JToken.DeepEquals(serialized, Serialize(Load(type, serialized))));
            Assert.True(JToken.DeepEquals(serialized, Serialize(JsonConvert.DeserializeObject(serialized.ToString(), type))));

            // Copying a Rule must retain legacy wire semantics, particularly IncludeToday=false.
            foreach (var target in FilterTypes)
            {
                var copy = Serialize(Create(target, ReadRule(loaded)));
                Assert.Equal(serialized["RuleType"], copy["RuleType"]);
                Assert.Equal(serialized["IncludeToday"], copy["IncludeToday"]);
                Assert.True(JToken.DeepEquals(serialized["CustomDateRange"], copy["CustomDateRange"]));
            }
        }

        [Theory]
        [MemberData(nameof(RelativeRules))]
        public void RelativeJson_PreservesEveryWireRelationPeriodAndOptionalFlag(Type type, string relation, string period, bool? includeToday)
        {
            var json = FilterJson(type, "CustomRule");
            json["IncludeToday"] = false;
            var relative = new JObject
            {
                ["_type"] = "DateRuleType", ["Relation"] = relation,
                ["Count"] = 1, ["Period"] = period
            };
            if (includeToday.HasValue)
                relative["IncludeToday"] = includeToday.Value;
            json["CustomRule"] = relative;

            var loaded = Load(type, json);
            var serialized = Serialize(loaded);
            Assert.True(JToken.DeepEquals(relative, serialized["CustomRule"]));
            Assert.False((bool)serialized["IncludeToday"]);
            Assert.True(JToken.DeepEquals(serialized, Serialize(Load(type, serialized))));
            foreach (var target in FilterTypes)
                Assert.True(JToken.DeepEquals(relative, Serialize(Create(target, ReadRule(loaded)))["CustomRule"]));
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void Factories_ProduceExpectedRulesAndActivateFieldFiltering(Type type)
        {
            foreach (PeriodType period in Enum.GetValues(typeof(PeriodType)))
            {
                AssertRelative(type, DateFilterRule.Last(90, period, false), "Last", 90, period, false);
                AssertRelative(type, DateFilterRule.Last(3, period), "Last", 3, period, true);
                AssertRelative(type, DateFilterRule.Next(7, period), "Next", 7, period, null);
                AssertRelative(type, DateFilterRule.Previous(2, period), "Previous", 2, period, null);
                AssertRelative(type, DateFilterRule.This(period), "This", 1, period, null);
                AssertRelative(type, DateFilterRule.ToDate(period, false), "ToDate", 1, period, false);
                AssertRelative(type, DateFilterRule.ToDate(period), "ToDate", 1, period, true);
            }
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void ReplacingRule_ClearsInactiveStateAndSupportsFluentUpdates(Type type)
        {
            var filter = Create(type, DateFilterRule.Custom(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31)));
            if (filter is FilterBase fieldFilter)
            {
                fieldFilter.FilterType = FilterType.SelectedValues;
                fieldFilter.SelectedValues = new List<FilterValue>();
            }

            var snapshot = ReadRule(filter);
            Assert.Same(filter, SetRule(filter, DateFilterRule.Next(7, PeriodType.Day)));
            var relative = Serialize(filter);
            Assert.Null(relative["CustomDateRange"]);
            Assert.Equal("Next", (string)relative["CustomRule"]["Relation"]);
            if (filter is FilterBase)
            {
                Assert.Equal("FilterByRule", (string)relative["FilterType"]);
                Assert.Null(relative["SelectedValues"]);
            }

            SetRule(filter, snapshot);
            var range = Serialize(filter);
            Assert.Equal("CustomRange", (string)range["RuleType"]);
            Assert.NotNull(range["CustomDateRange"]);
            Assert.Null(range["CustomRule"]);

            SetRule(filter, DateFilterRule.AllTime);
            var allTime = Serialize(filter);
            Assert.Equal("AllTime", (string)allTime["RuleType"]);
            Assert.Null(allTime["CustomRule"]);
            Assert.Null(allTime["CustomDateRange"]);
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void MissingLegacyFlag_DefaultsTrueAndDoesNotOverrideNestedFlag(Type type)
        {
            var json = FilterJson(type, "LastYear");
            Assert.True((bool)Serialize(Load(type, json))["IncludeToday"]);
            json["RuleType"] = "CustomRule";
            json["CustomRule"] = new JObject
            {
                ["_type"] = "DateRuleType", ["Relation"] = "Last", ["Count"] = 90,
                ["Period"] = "Day", ["IncludeToday"] = false
            };
            var copy = Serialize(Create(type, ReadRule(Load(type, json))));
            Assert.True((bool)copy["IncludeToday"]);
            Assert.False((bool)copy["CustomRule"]["IncludeToday"]);
        }

        [Fact]
        public void CustomRange_SupportsOpenEndpointsAndRejectsReversedDates()
        {
            var date = new DateTime(2024, 6, 1);
            foreach (var rule in new[] { DateFilterRule.Custom(null, date), DateFilterRule.Custom(date, null), DateFilterRule.Custom(null, null) })
            {
                var filter = new DashboardDateFilter(rule);
                var json = Serialize(filter);
                Assert.True(JToken.DeepEquals(json, Serialize(Load(typeof(DashboardDateFilter), json))));
            }
            Assert.Throws<ArgumentException>(() => DateFilterRule.Custom(date, date.AddDays(-1)));
            Assert.NotNull(DateFilterRule.Custom(date, date));
        }

        [Fact]
        public void Factories_RejectInvalidCountsAndPeriods()
        {
            foreach (var count in new[] { 0, -1 })
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Last(count, PeriodType.Day));
                Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Next(count, PeriodType.Day));
                Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Previous(count, PeriodType.Day));
            }
            var invalid = (PeriodType)99;
            Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Last(1, invalid));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Next(1, invalid));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.Previous(1, invalid));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.This(invalid));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateFilterRule.ToDate(invalid));
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void NullRules_AreRejectedWithoutChangingTheSelection(Type type)
        {
            Assert.Throws<ArgumentNullException>(() => Create(type, null));
            var filter = Create(type, DateFilterRule.Next(7, PeriodType.Day));
            var before = Serialize(filter);
            Assert.Throws<ArgumentNullException>(() => SetRule(filter, null));
            Assert.Throws<ArgumentNullException>(() => AssignRule(filter, null));
            Assert.True(JToken.DeepEquals(before, Serialize(filter)));
        }

        [Fact]
        public void RuleConstructors_WorkWithVisualizationFluentApi()
        {
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateField("OrderDate") });
            var global = new DashboardDateFilter("Sales Date", DateFilterRule.Last(90, PeriodType.Day, false));
            var field = new DateTimeFilter(DateFilterRule.Next(7, PeriodType.Day));
            var visualization = new GridVisualization(source);
            Assert.Same(visualization, visualization.AddDataFilter("OrderDate", field).ConnectDashboardFilter(global, "OrderDate"));
            var document = new RdashDocument();
            document.Filters.Add(global);
            document.Visualizations.Add(visualization);

            var loaded = RdashDocument.LoadFromJson(document.ToJsonString());
            var loadedField = Assert.IsType<DateField>(loaded.Visualizations[0].DataDefinition.AsTabular().Fields.Single());
            Assert.Equal("Next", (string)Serialize(loadedField.DataFilter)["CustomRule"]["Relation"]);
            Assert.Equal(FilterType.FilterByRule, loadedField.DataFilter.FilterType);
            Assert.Single(loaded.Visualizations[0].FilterBindings);
        }

        private static void AssertRelative(Type type, DateFilterRule rule, string relation, int count, PeriodType period, bool? includeToday)
        {
            var json = Serialize(Create(type, rule));
            Assert.Equal("CustomRule", (string)json["RuleType"]);
            Assert.Null(json["CustomDateRange"]);
            var relative = json["CustomRule"];
            Assert.Equal("DateRuleType", (string)relative["_type"]);
            Assert.Equal(relation, (string)relative["Relation"]);
            Assert.Equal(count, (int)relative["Count"]);
            Assert.Equal(period.ToString(), (string)relative["Period"]);
            Assert.Equal(includeToday, (bool?)relative["IncludeToday"]);
            if (type != typeof(DashboardDateFilter))
                Assert.Equal("FilterByRule", (string)json["FilterType"]);
        }

        private static JObject FilterJson(Type type, string rule) => new JObject
        {
            ["_type"] = type == typeof(DashboardDateFilter) ? "DateGlobalFilterType" : type.Name + "Type",
            ["RuleType"] = rule
        };

        private static object Load(Type type, JObject json)
        {
            if (type == typeof(DashboardDateFilter))
                return RdashDocument.LoadFromJson(new JObject { ["GlobalFilters"] = new JArray(json) }.ToString()).Filters[0];
            return JsonConvert.DeserializeObject<IFilter>(json.ToString());
        }

        private static JObject Serialize(object filter) => JObject.Parse(RdashSerializer.SerializeObject(filter));

        private static object Create(Type type, DateFilterRule rule)
        {
            if (type == typeof(DashboardDateFilter)) return new DashboardDateFilter(rule);
            if (type == typeof(DateTimeFilter)) return new DateTimeFilter(rule);
            return new XmlaDateFilter(rule);
        }

        private static DateFilterRule ReadRule(object filter) => filter switch
        {
            DashboardDateFilter dashboard => dashboard.Rule,
            DateTimeFilter field => field.Rule,
            XmlaDateFilter xmla => xmla.Rule,
            _ => throw new ArgumentException(nameof(filter))
        };

        private static object SetRule(object filter, DateFilterRule rule) => filter switch
        {
            DashboardDateFilter dashboard => dashboard.SetRule(rule),
            DateTimeFilter field => field.SetRule(rule),
            XmlaDateFilter xmla => xmla.SetRule(rule),
            _ => throw new ArgumentException(nameof(filter))
        };

        private static void AssignRule(object filter, DateFilterRule rule)
        {
            switch (filter)
            {
                case DashboardDateFilter dashboard: dashboard.Rule = rule; break;
                case DateTimeFilter field: field.Rule = rule; break;
                case XmlaDateFilter xmla: xmla.Rule = rule; break;
            }
        }
    }
}
