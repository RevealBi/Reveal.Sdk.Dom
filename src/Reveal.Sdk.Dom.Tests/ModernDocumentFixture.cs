using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class ModernDocumentFixture
    {
        [Fact]
        public void Creation_UsesUniqueIdsAndModernFormat_WithoutLegacyDefaults()
        {
            var first = new DashboardDateFilter(DateFilterRule.AllTime);
            var second = new DashboardDateFilter("Delivery", DateFilterRule.AllTime);
            Assert.True(Guid.TryParse(first.Id, out _));
            Assert.True(Guid.TryParse(second.Id, out _));
            Assert.NotEqual(first.Id, second.Id);
            Assert.Null(new DashboardDateFilterBindingTarget().DashboardFilterId);
            var document = new RdashDocument();
            document.Filters.AddRange(new[] { first, second });
            var grid = new GridVisualization(DataSource()).ConnectDashboardFilter(first, "Date").ConnectDashboardFilter(second, "Date");
            grid.Linker = new VisualizationLinker().AddDashboard("Target", "target", new DateLinkFilter(first, second));
            document.Visualizations.Add(grid);
            var json = JObject.Parse(document.ToJsonString());
            Assert.Equal(8, (int)json["FormatVersion"]);
            Assert.DoesNotContain("_date", json.ToString());
            var loaded = RdashDocument.LoadFromJson(json.ToString());
            Assert.Equal(new[] { first.Id, second.Id }, loaded.Filters.Select(f => f.Id));
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void LoadedDocument_DoesNotClaimItWasMigrated(int version)
        {
            var document = RdashDocument.LoadFromJson($"{{\"FormatVersion\":{version}}}");
            Assert.Equal(version, (int)JObject.Parse(document.ToJsonString())["FormatVersion"]);
            Assert.Equal(6, RdashDocument.LoadFromJson("{}").FormatVersion);
        }

        [Fact]
        public void LegacyDocument_RejectsNewIdsAndImportIntoModernDocumentBeforeMutation()
        {
            var legacy = RdashDocument.LoadFromJson("{\"FormatVersion\":6}");
            legacy.Filters.Add(new DashboardDateFilter(DateFilterRule.AllTime));
            Assert.Throws<InvalidOperationException>(() => legacy.ToJsonString());
            var target = new RdashDocument();
            Assert.Throws<InvalidOperationException>(() => target.Import(legacy));
            Assert.Empty(target.Visualizations);
            Assert.Empty(target.Filters);
            ((DashboardDateFilter)legacy.Filters[0]).CrossFilteringSourceWidgetId = "widget";
            Assert.Throws<InvalidOperationException>(() => legacy.ToJsonString());
            legacy.Filters[0].Id = "xFiltering_date";
            Assert.Contains("xFiltering_date", legacy.ToJsonString());
        }

        [Theory]
        [InlineData(DateAggregationType.Year, 5, "Year,Month,Day,Hour,Minute")]
        [InlineData(DateAggregationType.Month, 4, "Month,Day,Hour,Minute")]
        [InlineData(DateAggregationType.Day, 3, "Day,Hour,Minute")]
        [InlineData(DateAggregationType.Hour, 2, "Hour,Minute")]
        [InlineData(DateAggregationType.Minute, 1, "Minute")]
        [InlineData(DateAggregationType.Quarter, 1, "Quarter")]
        public void ModernDateHierarchy_WritesExplicitLevelsAndIsIdempotent(DateAggregationType aggregation, int count, string levels)
        {
            var chart = new ColumnChartVisualization(DataSource()).SetLabel(new DateDataField("Date") { AggregationType = aggregation }).SetValue("Sales");
            var document = new RdashDocument(); document.Visualizations.Add(chart);
            var json = JObject.Parse(document.ToJsonString());
            var spec = json["Widgets"][0]["VisualizationDataSpec"];
            Assert.Equal(1, (int)spec["FormatVersion"]);
            Assert.Equal(count, spec["Rows"].Count());
            Assert.Equal(levels.Split(','), spec["Rows"].Select(row => (string)row["SummarizationField"]["DateAggregationType"]));
            if (count > 1) Assert.Equal(count, (int)spec["AdHocFields"]);
            var roundTrip = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());
            Assert.True(JToken.DeepEquals(spec, roundTrip["Widgets"][0]["VisualizationDataSpec"]));
            Assert.Single(chart.Labels); // Serialization does not mutate the developer's configured labels.
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ModernWriter_PreservesFiscalSettingsAndExistingExplicitSettings(bool dateOnly)
        {
            var dateSettings = new DateTimeFieldSettings { DateFiscalYearStartMonth = 4, DisplayInLocalTimeZone = true };
            IField field = dateOnly ? new DateField("Date") { Settings = dateSettings }
                : new DateTimeField("Date") { Settings = dateSettings };
            var data = DataSource(); data.Fields = new List<IField> { field };
            var document = new RdashDocument(); document.Visualizations.Add(new GridVisualization(data));
            var json = JObject.Parse(document.ToJsonString());
            var settings = json["Widgets"][0]["DataSpec"]["Fields"][0]["Settings"];
            Assert.Equal(4, (int)settings["DateFiscalYearStartMonth"]);
            Assert.True((bool)settings["DisplayInLocalTimeZone"]);
            Assert.Equal(settings, JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString())["Widgets"][0]["DataSpec"]["Fields"][0]["Settings"]);
            Assert.Null(typeof(DateTimeFilter).GetProperty("DateFiscalYearStartMonth"));
            Assert.Null(typeof(DateTimeFilter).GetProperty("DisplayInLocalTimeZone"));
        }

        [Fact]
        public void ModernWriter_ProjectsLegacyDateFieldSettingsFromLoadedFilter()
        {
            var filter = JsonConvert.DeserializeObject<DateTimeFilter>("{\"_type\":\"DateTimeFilterType\",\"RuleType\":\"Today\",\"DateFiscalYearStartMonth\":4,\"DisplayInLocalTimeZone\":true}");
            var data = DataSource(); ((DateTimeField)data.Fields[0]).DataFilter = filter;
            var document = new RdashDocument(); document.Visualizations.Add(new GridVisualization(data));
            var field = JObject.Parse(document.ToJsonString())["Widgets"][0]["DataSpec"]["Fields"][0];
            Assert.Equal(4, (int)field["Settings"]["DateFiscalYearStartMonth"]);
            Assert.True((bool)field["Settings"]["DisplayInLocalTimeZone"]);
            Assert.Equal(0, (int)field["Filter"]["DateFiscalYearStartMonth"]);
        }

        [Fact]
        public void ModernDateHierarchy_DoesNotReuseTheFirstLevelsFormattingOrDrillSelection()
        {
            var json = JObject.Parse("""
                {"Widgets":[{"DataSpec":{"Fields":[{"FieldName":"Date","FieldType":"Date"}]},
                  "VisualizationDataSpec":{"FormatVersion":0,"Rows":[{"SummarizationField":{
                    "_type":"SummarizationDateFieldType","FieldName":"Date","DateAggregationType":"Year",
                    "DateFormatting":{"_type":"DateFormattingSpecType","DateFormat":"yyyy"},"DrillDownElements":["2026"]
                  }}]}}]}
                """);
            ModernDocumentWriter.Prepare(json);
            var rows = json["Widgets"][0]["VisualizationDataSpec"]["Rows"];
            Assert.NotNull(rows[0]["SummarizationField"]["DateFormatting"]);
            Assert.Single(rows[0]["SummarizationField"]["DrillDownElements"]);
            foreach (var row in rows.Skip(1))
            {
                Assert.Null(row["SummarizationField"]["DateFormatting"]);
                Assert.Empty(row["SummarizationField"]["DrillDownElements"]);
            }
        }

        [Fact]
        public void ModernWriter_CarriesSingleValueFormattingToTheMeasure()
        {
            var text = new TextVisualization(DataSource()).SetValue("Sales");
            text.Settings.ConditionalFormattingEnabled = true;
            text.Settings.UpperBand.Value = 90;
            text.Settings.MiddleBand.Value = 40;
            var document = new RdashDocument(); document.Visualizations.Add(text);
            var spec = JObject.Parse(document.ToJsonString())["Widgets"][0]["VisualizationDataSpec"];
            var bands = spec["Value"][0]["SummarizationField"]["ConditionalFormatting"]["Bands"];
            Assert.Equal(3, bands.Count());
            Assert.Equal(90, (int)bands[0]["Value"]);
            Assert.Equal(40, (int)bands[1]["Value"]);
            Assert.All(bands, band => Assert.Equal("ConditionalFormattingBandType", (string)band["_type"]));
        }

        [Fact]
        public void ModernWriter_ConvertsXmlaDrillNamesWithoutDuplicatingMembers()
        {
            var json = JObject.Parse("""
                {"Widgets":[{"VisualizationDataSpec":{"Rows":[{"XmlaElement":{
                  "DrillDownElements":["[Date].[2026]","[Date].[Q1]"],
                  "DrillDownMembers":[{"UniqueName":"[Date].[2026]","Caption":"Fiscal 2026"}]
                }}]}}]}
                """);
            ModernDocumentWriter.Prepare(json);
            var element = json["Widgets"][0]["VisualizationDataSpec"]["Rows"][0]["XmlaElement"];
            Assert.Empty(element["DrillDownElements"]);
            Assert.Equal(2, element["DrillDownMembers"].Count());
            Assert.Equal("Fiscal 2026", (string)element["DrillDownMembers"][0]["Caption"]);
            Assert.Equal("Q1", (string)element["DrillDownMembers"][1]["Caption"]);
            var once = json.DeepClone(); ModernDocumentWriter.Prepare(json);
            Assert.Equal(once, json);
        }

        private static DataSourceItem DataSource() => new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
            .SetFields(new List<IField> { new DateTimeField("Date"), new NumberField("Sales") });
    }
}
