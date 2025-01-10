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
    public class UrlLinkFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WithoutParams()
        {
            // Act
            var instance = new UrlLink();

            // Assert
            Assert.Null(instance.Title);
            Assert.Null(instance.Url);
            Assert.Equal(LinkType.OpenUrl, instance.Type);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithParams()
        {
            // Act
            var instance = new UrlLink("Title", "http://url");

            // Assert
            Assert.Equal("Title", instance.Title);
            Assert.Equal("http://url", instance.Url);
            Assert.Equal(LinkType.OpenUrl, instance.Type);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "Url": "Dashboard",
              "Parameters": [],
              "Title": "My Dashboard",
              "Type": "OpenDashboard"
            }
            """;

            var instance = new DashboardLink("My Dashboard", "Dashboard");

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
