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
    public class MapZoomRectangleFixture
    {
        [Fact]
        public void ToJsonString_CreateCorrectJson_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "X": 2.1,
                  "Y": 5.5,
                  "Width": 12.1,
                  "Height": 10.5
                }
                """;
            var mapZoomRectangle = new MapZoomRectangle()
            {
                DegreesLatitude = 10.5,
                DegreesLongitude = 12.1,
                Latitude = 5.5,
                Longitude = 2.1
            };

            // Act
            var actualJson = mapZoomRectangle.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
