using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DateFilterIdentityFixture
    {
        [Theory]
        [InlineData("orders-date")]
        [InlineData("_date")]
        public void Binding_UsesTheSelectedFilterId_AndRoundTrips(string id)
        {
            var filter = new DashboardDateFilter(DateFilterRule.AllTime) { Id = id };
            var binding = new DashboardDateFilterBinding(filter, "OrderedOn");
            var json = JObject.Parse(JsonConvert.SerializeObject(binding));

            Assert.Equal(id, (string)json["Target"]["GlobalFilterId"]);
            Assert.Equal("OrderedOn", (string)json["Source"]["FieldName"]);
            Assert.Equal(BindingOperatorType.Between, binding.Operator);
            var loaded = Assert.IsType<DashboardDateFilterBinding>(JsonConvert.DeserializeObject<Binding>(json.ToString()));
            Assert.Equal(id, loaded.Target.DashboardFilterId);
            Assert.Equal(json, JObject.Parse(JsonConvert.SerializeObject(loaded)));
        }

        [Fact]
        public void Binding_RequiresAFilter_AndDefaultsOnlyTheFieldName()
        {
            var filter = new DashboardDateFilter(DateFilterRule.AllTime) { Id = "actual-filter" };
            var binding = new DashboardDateFilterBinding(filter);

            Assert.Equal(filter.Id, binding.Target.DashboardFilterId);
            Assert.Equal("Date", Assert.IsType<FieldBindingSource>(binding.Source).FieldName);
            Assert.Throws<ArgumentNullException>(() => new DashboardDateFilterBinding(null));
            Assert.Null(typeof(DashboardDateFilterBinding).GetConstructor(new[] { typeof(string) }));
            Assert.Null(typeof(DashboardDateFilterBinding).GetConstructor(Type.EmptyTypes));
        }

        [Fact]
        public void LegacyBinding_WithMissingTargetId_StillLoads()
        {
            const string json = """
                {"Operator":"Between","Source":{"_type":"FieldBindingSourceType","FieldName":"Date"},
                 "Target":{"_type":"DateGlobalFilterBindingTargetType"}}
                """;
            var binding = Assert.IsType<DashboardDateFilterBinding>(JsonConvert.DeserializeObject<Binding>(json));

            Assert.Equal("_date", binding.Target.DashboardFilterId);
            Assert.Equal("Date", Assert.IsType<FieldBindingSource>(binding.Source).FieldName);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void ModernDocument_RoundTripsIndependentDateFiltersBindingsAndLinks(int version)
        {
            var document = ModernDocument(version);
            var orders = new DashboardDateFilter("Order Date", DateFilterRule.Last(7, PeriodType.Day)) { Id = "orders" };
            var shipping = new DashboardDateFilter("Ship Date", DateFilterRule.Next(7, PeriodType.Day)) { Id = "shipping" };
            document.Filters.AddRange(new[] { orders, shipping });
            var visualization = new GridVisualization(DataSource())
                .ConnectDashboardFilter(orders, "OrderDate")
                .ConnectDashboardFilter(shipping, "ShipDate");
            visualization.Linker = new VisualizationLinker().AddDashboard("Details", "details",
                new DateLinkFilter(shipping, "target-date"));
            document.Visualizations.Add(visualization);

            var loaded = RdashDocument.LoadFromJson(document.ToJsonString());

            Assert.Equal(version, loaded.FormatVersion);
            Assert.Equal(new[] { "orders", "shipping" }, loaded.Filters.Select(f => f.Id));
            var bindings = loaded.Visualizations[0].FilterBindings.Cast<DashboardDateFilterBinding>().ToArray();
            Assert.Equal(new[] { "orders", "shipping" }, bindings.Select(b => b.Target.DashboardFilterId));
            Assert.Equal(new[] { "OrderDate", "ShipDate" }, bindings.Select(b => ((FieldBindingSource)b.Source).FieldName));
            var link = Assert.IsType<DashboardLink>(Assert.IsType<GridVisualization>(loaded.Visualizations[0]).Linker.Links[0]);
            Assert.Equal("shipping.Ship Date", link.Filters[0].Value);
            Assert.Equal("target-date", link.Filters[0].TargetFilterId);
        }

        [Theory]
        [InlineData("orders")]
        [InlineData("_date")]
        public void Import_UsesBindingTargetToFindDateFilter_AndDoesNotImportUnboundFilter(string id)
        {
            var source = ModernDocument(8);
            var selected = new DashboardDateFilter("Selected", DateFilterRule.AllTime) { Id = id };
            source.Filters.Add(new DashboardDateFilter("Unbound", DateFilterRule.AllTime) { Id = "other" });
            source.Filters.Add(selected);
            var visualization = new GridVisualization(DataSource()).ConnectDashboardFilter(selected, "OrderDate");
            source.Visualizations.Add(visualization);
            source.Validate();
            var target = ModernDocument(8);

            target.Import(source, visualization, new ImportOptions { IncludeDashboardFilters = true });
            target.Import(source, visualization, new ImportOptions { IncludeDashboardFilters = true });

            var importedFilter = Assert.Single(target.Filters);
            Assert.Equal(id, importedFilter.Id);
            Assert.Equal("Selected", importedFilter.Title);
            Assert.NotSame(selected, importedFilter);
            Assert.All(target.Visualizations, v => Assert.Equal(id,
                Assert.IsType<DashboardDateFilterBinding>(Assert.Single(v.FilterBindings)).Target.DashboardFilterId));
            var loaded = RdashDocument.LoadFromJson(target.ToJsonString());
            Assert.Equal(id, Assert.Single(loaded.Filters).Id);
        }

        [Theory]
        [InlineData(6, "_date")]
        [InlineData(7, "legacy-date")]
        [InlineData(8, "modern-date")]
        [InlineData(9, "modern-date")]
        public void LoadedDocument_KeepsItsFormatAndExplicitDateId(int version, string id)
        {
            var json = new JObject
            {
                ["FormatVersion"] = version,
                ["GlobalFilters"] = new JArray(new JObject
                {
                    ["_type"] = "DateGlobalFilterType", ["Id"] = id, ["RuleType"] = "LastYear"
                })
            };
            var document = RdashDocument.LoadFromJson(json.ToString());
            var serialized = JObject.Parse(document.ToJsonString());

            Assert.Equal(version, (int)serialized["FormatVersion"]);
            Assert.Equal(id, (string)serialized["GlobalFilters"][0]["Id"]);
        }

        [Fact]
        public void LegacyDateFilter_WithMissingId_RetainsTheCompatibilityDefault()
        {
            var filter = JsonConvert.DeserializeObject<DashboardDateFilter>("{\"_type\":\"DateGlobalFilterType\",\"RuleType\":\"LastYear\"}");
            Assert.Equal("_date", filter.Id);
            Assert.Equal("_date", (string)JObject.Parse(JsonConvert.SerializeObject(filter))["Id"]);
        }

        [Fact]
        public void DateLink_UsesDifferentSourceAndTargetFilters()
        {
            var source = new DashboardDateFilter("Order Date", DateFilterRule.AllTime) { Id = "orders" };
            var target = new DashboardDateFilter("Delivery Date", DateFilterRule.AllTime) { Id = "deliveries" };
            var link = new DateLinkFilter(source, target);

            Assert.Equal("Delivery Date", link.Name);
            Assert.Equal("orders.Order Date", link.Value);
            Assert.Equal("deliveries", link.TargetFilterId);
            Assert.Equal(LinkFilterType.GlobalFilter, link.Type);
            var json = JObject.Parse(link.ToJsonString());
            Assert.Equal("deliveries", (string)json["Namespace"]);
            Assert.Equal("orders.Order Date", (string)json["Value"]);
        }

        [Theory]
        [InlineData("target-date")]
        [InlineData("_date")]
        public void DateLink_AcceptsExplicitTargetIdIncludingLegacyAlias(string targetId)
        {
            var source = new DashboardDateFilter("Order Date", DateFilterRule.AllTime) { Id = "source-date" };
            var link = new DateLinkFilter(source, targetId);

            Assert.Equal(targetId, link.TargetFilterId);
            Assert.Equal("source-date.Order Date", link.Value);
        }

        [Theory]
        [InlineData("orders", "deliveries")]
        [InlineData("_date", "_date")]
        public void DateLink_JsonRoundTripPreservesSourceAndTargetIds(string sourceId, string targetId)
        {
            var json = new JObject
            {
                ["Name"] = "Target Date", ["Namespace"] = targetId,
                ["Type"] = "GlobalFilter", ["Value"] = sourceId + ".Source Date"
            };
            var link = JsonConvert.DeserializeObject<DateLinkFilter>(json.ToString());

            Assert.Equal(json, JObject.Parse(link.ToJsonString()));
        }

        [Fact]
        public void DateLink_JsonConstructorRetainsMissingLegacyDefaults_WithoutPublicEmptyConstructor()
        {
            var link = JsonConvert.DeserializeObject<DateLinkFilter>("{}");

            Assert.Equal("_date", link.TargetFilterId);
            Assert.Equal("_date.Date Filter", link.Value);
            Assert.Null(typeof(DateLinkFilter).GetConstructor(Type.EmptyTypes));
        }

        [Fact]
        public void DateLink_RejectsMissingFiltersAndIds()
        {
            var source = new DashboardDateFilter(DateFilterRule.AllTime);
            Assert.Throws<ArgumentNullException>(() => new DateLinkFilter(null, "target"));
            Assert.Throws<ArgumentNullException>(() => new DateLinkFilter(source, (DashboardDateFilter)null));
            Assert.Throws<ArgumentException>(() => new DateLinkFilter(source, (string)null));
            Assert.Throws<ArgumentException>(() => new DateLinkFilter(source, " "));
            source.Id = "";
            Assert.Throws<ArgumentException>(() => new DateLinkFilter(source, "target"));
        }

        private static RdashDocument ModernDocument(int version) =>
            RdashDocument.LoadFromJson(new JObject { ["FormatVersion"] = version }.ToString());

        private static DataSourceItem DataSource() =>
            new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateTimeField("OrderDate"), new DateTimeField("ShipDate") });
    }
}
