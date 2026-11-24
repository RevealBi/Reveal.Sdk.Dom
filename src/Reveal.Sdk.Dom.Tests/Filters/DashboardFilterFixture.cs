using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class DashboardFilterFixture
    {
        [Fact]
        public void IsHidden_IsFalseAndWritten_ByDefault()
        {
            // Arrange
            var filter = new DashboardDateFilter();

            // Act
            var json = JObject.Parse(filter.ToJsonString());

            // Assert
            Assert.False(filter.IsHidden);
            Assert.False((bool)json["IsHidden"]);
        }

        [Fact]
        public void LoadFromJson_KeepsIsHidden_WhenFiltersAreHidden()
        {
            // Arrange: dashboard filters hidden in the Reveal editor
            var source = new DataSourceItemFactory().Create(DataSourceType.REST, "", "")
                .SetFields(new List<IField> { new DateField("Date"), new TextField("City"), new NumberField("Sales") });
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(source).SetColumns("City", "Sales"));
            document.Filters.Add(new DashboardDateFilter());
            document.Filters.Add(new DashboardDataFilter("City", source));
            var json = JObject.Parse(document.ToJsonString());
            foreach (var filter in json["GlobalFilters"])
                filter["IsHidden"] = true;

            // Act
            var loaded = RdashDocument.LoadFromJson(json.ToString());
            var saved = JObject.Parse(loaded.ToJsonString());

            // Assert
            Assert.Equal(2, loaded.Filters.Count);
            Assert.All(loaded.Filters, filter => Assert.True(filter.IsHidden));
            Assert.All(saved["GlobalFilters"], filter => Assert.True((bool)filter["IsHidden"]));
        }
    }
}
