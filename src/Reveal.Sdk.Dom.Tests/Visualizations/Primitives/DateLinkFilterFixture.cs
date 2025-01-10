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
    public class DateLinkFilterFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new DateLinkFilter();

            // Assert
            Assert.Equal("Date Filter", instance.Name);
            Assert.Equal("_date.Date Filter", instance.Value);
            Assert.Equal("_date", instance.TargetFilterId);
            Assert.Equal(LinkFilterType.GlobalFilter, instance.Type);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "Name": "Date Filter",
              "Namespace": "_date",
              "Type": "GlobalFilter",
              "Value": "_date.Date Filter"
            }
            """;

            var instance = new DateLinkFilter();

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
