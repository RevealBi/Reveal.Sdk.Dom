using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Filters;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateFilterDefaultRuleFixture
    {
        private static readonly Type[] FilterTypes =
        {
            typeof(DashboardDateFilter), typeof(DateTimeFilter), typeof(XmlaDateFilter)
        };

        public static IEnumerable<object[]> Filters
        {
            get { foreach (var type in FilterTypes) yield return new object[] { type }; }
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void DefaultConstructor_MatchesExplicitAllTimeAndSurvivesRoundTripAndCopy(Type type)
        {
            var filter = Activator.CreateInstance(type);
            var json = Serialize(filter);
            var expected = Serialize(Activator.CreateInstance(type, DateFilterRule.AllTime));
            if (type == typeof(DashboardDateFilter))
            {
                Assert.True(Guid.TryParse((string)json["Id"], out _));
                expected["Id"] = json["Id"];
            }
            Assert.Equal(expected, json);
            Assert.Equal("AllTime", (string)json["RuleType"]);
            Assert.Equal(json, Serialize(JsonConvert.DeserializeObject(json.ToString(), type)));
            Assert.Equal(json, Serialize(Clone(filter)));
            foreach (var target in FilterTypes)
            {
                var copy = Activator.CreateInstance(target, ReadRule(filter));
                Assert.Equal("AllTime", (string)Serialize(copy)["RuleType"]);
            }
        }

        [Fact]
        public void DashboardConstructors_DefaultOnlyOmittedRulesAndRetainTitles()
        {
            var defaultTitle = new DashboardDateFilter();
            var titled = new DashboardDateFilter("Orders");
            Assert.Equal("Date Filter", defaultTitle.Title);
            Assert.Equal("Orders", titled.Title);
            Assert.NotEqual(defaultTitle.Id, titled.Id);
            Assert.Equal("AllTime", (string)Serialize(titled)["RuleType"]);

            var explicitRule = new DashboardDateFilter("Orders", DateFilterRule.Next(7, PeriodType.Day, true));
            Assert.Equal("Orders", explicitRule.Title);
            Assert.Equal("Next", (string)Serialize(explicitRule)["CustomRule"]["Relation"]);
            Assert.Throws<ArgumentNullException>(() => new DashboardDateFilter("Orders", null));
        }

        [Theory]
        [MemberData(nameof(Filters))]
        public void LoadingMissingFields_UsesDashboardDefaultsAndPreservesFieldModes(Type type)
        {
            var json = new JObject { ["_type"] = type == typeof(DashboardDateFilter) ? "DateGlobalFilterType" : type.Name + "Type" };
            var loaded = JsonConvert.DeserializeObject(json.ToString(), type);
            var saved = Serialize(loaded);
            Assert.Equal("AllTime", (string)saved["RuleType"]);
            Assert.True((bool)saved["IncludeToday"]);
            if (type == typeof(DashboardDateFilter))
                Assert.True(Guid.TryParse((string)saved["Id"], out _));
            else
                Assert.Equal("AllValues", (string)saved["FilterType"]);
            Assert.Equal(saved, Serialize(JsonConvert.DeserializeObject(saved.ToString(), type)));
            Assert.Equal(saved, Serialize(Clone(loaded)));
        }

        [Theory]
        [InlineData(typeof(DateTimeFilter))]
        [InlineData(typeof(XmlaDateFilter))]
        public void LoadingExplicitFieldModes_DoesNotActivateOrClearSavedSelections(Type type)
        {
            foreach (var mode in new[] { "AllValues", "SelectedValues", "FilterByRule" })
            {
                var json = new JObject
                {
                    ["_type"] = type.Name + "Type", ["RuleType"] = "LastMonth", ["IncludeToday"] = false,
                    ["FilterType"] = mode, ["SelectedValues"] = new JArray()
                };
                var loaded = JsonConvert.DeserializeObject(json.ToString(), type);
                var saved = Serialize(loaded);
                foreach (var property in json.Properties()) Assert.Equal(property.Value, saved[property.Name]);
                Assert.Equal(saved, Serialize(Clone(loaded)));
            }
        }

        [Theory]
        [InlineData(6)]
        [InlineData(8)]
        public void DashboardRoundTrip_PreservesNewDefaultsAndExistingSelections(int version)
        {
            var document = RdashDocument.LoadFromJson(new JObject
            {
                ["FormatVersion"] = version,
                ["GlobalFilters"] = new JArray(new JObject
                {
                    ["_type"] = "DateGlobalFilterType", ["Id"] = "existing", ["RuleType"] = "LastMonth", ["IncludeToday"] = false
                }, new JObject { ["_type"] = "DateGlobalFilterType" })
            }.ToString());
            document.Filters.Add(new DashboardDateFilter());
            document.Filters.Add(new DashboardDateFilter("Orders"));
            var saved = JObject.Parse(document.ToJsonString());
            var filters = saved["GlobalFilters"];
            Assert.Equal("LastMonth", (string)filters[0]["RuleType"]);
            Assert.False((bool)filters[0]["IncludeToday"]);
            Assert.Equal("AllTime", (string)filters[1]["RuleType"]);
            Assert.Equal("AllTime", (string)filters[2]["RuleType"]);
            Assert.Equal("AllTime", (string)filters[3]["RuleType"]);
            Assert.Equal("Orders", (string)filters[3]["Title"]);
            Assert.Equal(saved, JObject.Parse(RdashDocument.LoadFromJson(saved.ToString()).ToJsonString()));
        }

        private static JObject Serialize(object filter) => JObject.Parse(RdashSerializer.SerializeObject(filter));
        private static DateFilterRule ReadRule(object filter) => (DateFilterRule)filter.GetType().GetProperty("Rule").GetValue(filter);
        private static object Clone(object filter) => filter switch
        {
            DashboardDateFilter dashboard => CloneUtility.Clone(dashboard),
            DateTimeFilter field => CloneUtility.Clone(field),
            XmlaDateFilter xmla => CloneUtility.Clone(xmla),
            _ => throw new ArgumentException(nameof(filter))
        };
    }
}
