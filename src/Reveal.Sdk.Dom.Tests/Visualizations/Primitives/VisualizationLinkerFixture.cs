using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class VisualizationLinkerFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new VisualizationLinker();

            // Assert
            Assert.Equal(LinkTriggerType.SelectRow, instance.Trigger);
            Assert.Empty(instance.Links);
        }

        [Fact]
        public void AddUrl_AddNewLink_WhenUsed()
        {             
            // Arrange
            var instance = new VisualizationLinker();
            var newTitle = "My Dashboard";
            var newUrl = "https://dash.board/url";
            // Act
            instance.AddUrl(newTitle, newUrl);

            // Assert
            Assert.Single(instance.Links);
            Assert.IsType<UrlLink>(instance.Links[0]);  
            Assert.Equal(LinkType.OpenUrl, instance.Links[0].Type);
            Assert.Equal(newTitle, instance.Links[0].Title);
            Assert.Equal(newUrl, ((UrlLink)instance.Links[0]).Url);
        }

        [Fact]
        public void AddDashboard_AddNewDashboardLink_WhenUsed()
        {
            // Arrange
            var instance = new VisualizationLinker();
            var newTitle = "My Dashboard";
            var newDashboardId = "Dashboard ID";
            LinkFilter[] newFilters =
            {
                new("filter 1", "target 1", "value 1"),
                new("filter 2", "target 2", "value 2", LinkFilterType.Literal),
            };
            // Act
            instance.AddDashboard(newTitle, newDashboardId, newFilters);

            // Assert
            Assert.Single(instance.Links);
            Assert.IsType<DashboardLink>(instance.Links[0]);
            Assert.Equal(LinkType.OpenDashboard, instance.Links[0].Type);
            Assert.Equal(newTitle, instance.Links[0].Title);
            Assert.Equal(newDashboardId, ((DashboardLink)instance.Links[0]).Dashboard);
            Assert.Equal(newFilters, ((DashboardLink)instance.Links[0]).Filters);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var instance = new VisualizationLinker();

            LinkFilter[] newFilters =
            {
                new("filter 1", "target 1", "value 1"),
                new("filter 2", "target 2", "value 2", LinkFilterType.Literal),
            };
            instance.AddDashboard("My Dashboard", "Dashboard ID", newFilters);

            instance.AddUrl("My Url", "https://dash.board/url");

            var expectedJson = """
            {
              "Trigger": "SelectRow",
              "Actions": [
                {
                  "Url": "Dashboard ID",
                  "Parameters": [
                    {
                      "Name": "filter 1",
                      "Namespace": "target 1",
                      "Type": "Column",
                      "Value": "value 1"
                    },
                    {
                      "Name": "filter 2",
                      "Namespace": "target 2",
                      "Type": "Literal",
                      "Value": "value 2"
                    }
                  ],
                  "Title": "My Dashboard",
                  "Type": "OpenDashboard"
                },
                {
                  "Url": "https://dash.board/url",
                  "Parameters": [],
                  "Title": "My Url",
                  "Type": "OpenUrl"
                }
              ]
            }
            """;

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
