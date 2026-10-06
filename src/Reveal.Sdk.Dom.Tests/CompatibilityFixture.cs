using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    [CollectionDefinition("Compatibility diagnostics", DisableParallelization = true)]
    public class CompatibilityDiagnosticsCollection { }

    [Collection("Compatibility diagnostics")]
    public class CompatibilityFixture
    {
        [Theory]
        [InlineData("_date")]
        [InlineData("xFiltering_date")]
        [InlineData("explicit-id")]
        public void LegacySelections_LoadAndRoundTripWithOnlyRelevantWarnings(string id)
        {
            using var listener = new CompatibilityListener();
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
            for (var i = 0; i < 3; i++)
            {
                var saved = JObject.Parse(document.ToJsonString());
                Assert.Equal(6, (int)saved["FormatVersion"]);
                Assert.Equal(id, (string)saved["GlobalFilters"][0]["Id"]);
                Assert.Equal("Today", (string)saved["GlobalFilters"][0]["RuleType"]);
                Assert.False((bool)saved["GlobalFilters"][0]["IncludeToday"]);
            }
            Assert.Equal(id == "explicit-id" ? 1 : 0, listener.Messages.Count);
            Assert.All(listener.Messages, message =>
            {
                Assert.Contains("SDK", message);
                Assert.DoesNotContain(id, message);
            });
        }

        [Theory]
        [InlineData("Sales.rdash")]
        [InlineData("Marketing.rdash")]
        [InlineData("Manufacturing.rdash")]
        public void ExistingDashboardFixtures_LoadSaveAndImportWithoutCompatibilityRejection(string file)
        {
            using var listener = new CompatibilityListener();
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
            Assert.Equal(version < 7 ? 1 : 0, listener.Messages.Count(message => message.Contains("Importing legacy")));
        }

        [Fact]
        public void MalformedJson_StillFailsInsteadOfReturningAnEmptyDashboard()
        {
            Assert.ThrowsAny<JsonException>(() => RdashDocument.LoadFromJson("{not json"));
        }

        private sealed class CompatibilityListener : TraceListener
        {
            internal List<string> Messages { get; } = new List<string>();
            internal CompatibilityListener() { Trace.Listeners.Add(this); }
            public override void Write(string message) { }
            public override void WriteLine(string message)
            {
                if (message?.Contains("RdashCompatibility:") == true) Messages.Add(message);
            }
            protected override void Dispose(bool disposing)
            {
                Trace.Listeners.Remove(this);
                base.Dispose(disposing);
            }
        }
    }
}
