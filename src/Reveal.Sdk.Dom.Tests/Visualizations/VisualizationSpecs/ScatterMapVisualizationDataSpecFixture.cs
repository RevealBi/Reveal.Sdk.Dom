using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class ScatterMapVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_CreateObjWithDefaultValue_WithoutParameters()
        {
            // Act
            var scatterMapVSDataSpec = new ScatterMapVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.ScatterMapVisualizationDataSpecType, scatterMapVSDataSpec.SchemaTypeName);
            Assert.True(scatterMapVSDataSpec.IsColorByValue);
            Assert.Empty(scatterMapVSDataSpec.MapColor);
            Assert.Empty(scatterMapVSDataSpec.Radius);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "Test Schema Type Name",
                  "IsSingleLocationField": true,
                  "IsColorByValue": true,
                  "Location": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Longitude": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "Label": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "MapColorCategory": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "MapColor": [],
                  "Radius": []
                }
                """;
            var scatterMapVSDataSpec = new ScatterMapVisualizationDataSpec()
            {
                IsColorByValue = true,
                SchemaTypeName = "Test Schema Type Name",
                IsSingleLocationField = true,
                Label = new DimensionColumn(),
                Location = new DimensionColumn(),
                Longitude = new DimensionColumn(),
                MapColor = new List<MeasureColumn>(),
                MapColorCategory = new DimensionColumn(),
                Radius = new List<MeasureColumn>(),
            };

            // Act
            var actualJson = scatterMapVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
