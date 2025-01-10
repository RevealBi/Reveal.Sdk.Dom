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
    public class VisualizationLinkBaseFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new TestVisualizationLinkBase();

            // Assert
            Assert.Null(instance.Title);
            Assert.Equal(LinkType.OpenDashboard, instance.Type);
            Assert.Empty(instance.Filters);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "Parameters": [
                {
                  "Name": "Filter 1",
                  "Namespace": "target 1",
                  "Type": "Column",
                  "Value": "value 1"
                },
                {
                  "Name": "Filter 2",
                  "Namespace": "target 2",
                  "Type": "Column",
                  "Value": "value 2"
                }
              ],
              "Title": "My Dashboard",
              "Type": "OpenDashboard"
            }
            """;

            var instance = new TestVisualizationLinkBase()
            {
                Title = "My Dashboard",
                Filters = new List<LinkFilter>()
                {
                    new("Filter 1", "target 1", "value 1"),
                    new("Filter 2", "target 2", "value 2"),
                } 
            };

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class TestVisualizationLinkBase: VisualizationLinkBase
        { 
        }
    }
}
