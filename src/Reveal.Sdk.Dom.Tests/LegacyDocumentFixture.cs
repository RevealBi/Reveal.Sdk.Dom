using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class LegacyDocumentFixture
    {
        [Theory]
        [InlineData("_date")]
        [InlineData("xFiltering_date")]
        [InlineData("explicit-id")]
        public void LegacySelections_LoadAndRoundTrip(string id)
        {
            var json = new JObject
            {
                ["FormatVersion"] = 6,
                ["GlobalFilters"] = new JArray(new JObject
                {
                    ["_type"] = "DateGlobalFilterType", ["Id"] = id, ["RuleType"] = "Today", ["IncludeToday"] = false
                })
            };
            var document = RdashDocument.LoadFromJson(json.ToString());
            var date = Assert.IsType<DashboardDateFilter>(Assert.Single(document.Filters));
            Assert.Equal(DateRuleType.Today, date.RuleType);
            Assert.False(date.IncludeToday);
            var saved = JObject.Parse(document.ToJsonString());
            Assert.Equal(6, (int)saved["FormatVersion"]);
            Assert.Equal(id, (string)saved["GlobalFilters"][0]["Id"]);
            Assert.Equal("Today", (string)saved["GlobalFilters"][0]["RuleType"]);
            Assert.False((bool)saved["GlobalFilters"][0]["IncludeToday"]);
            var reloaded = RdashDocument.LoadFromJson(saved.ToString());
            Assert.True(JToken.DeepEquals(saved, JObject.Parse(reloaded.ToJsonString())));
        }

        [Theory]
        [InlineData("Sales.rdash")]
        [InlineData("Marketing.rdash")]
        [InlineData("Manufacturing.rdash")]
        public void ExistingDashboardFixtures_LoadSaveAndImport(string file)
        {
            var source = RdashDocument.Load(Path.Combine(Environment.CurrentDirectory, "Dashboards", file));
            var version = source.FormatVersion;
            var saved = RdashDocument.LoadFromJson(source.ToJsonString());
            Assert.Equal(version, saved.FormatVersion);
            Assert.Equal(source.Visualizations.Count, saved.Visualizations.Count);
            Assert.Equal(source.Filters.Select(f => f.Id), saved.Filters.Select(f => f.Id));

            var target = new RdashDocument();
            var options = new ImportOptions { IncludeDashboardFilters = true, IncludeVisualizationFilters = true };
            target.Import(source, options: options);
            target.Import(source, options: options);
            Assert.Equal(source.Visualizations.Count * 2, target.Visualizations.Count);
            Assert.Equal(8, (int)JObject.Parse(target.ToJsonString())["FormatVersion"]);
        }

        [Fact]
        public void MalformedJson_StillFailsInsteadOfReturningAnEmptyDashboard()
        {
            Assert.ThrowsAny<JsonException>(() => RdashDocument.LoadFromJson("{not json"));
        }

    }
}
