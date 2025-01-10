using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class ScatterVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var scatterVSDataSpec = new ScatterVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.ScatterVisualizationDataSpecType, scatterVSDataSpec.SchemaTypeName);
            Assert.Empty(scatterVSDataSpec.XAxis);
            Assert.Empty(scatterVSDataSpec.YAxis);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "ScatterVisualizationDataSpecType",
                  "Category": {
                    "_type": "DimensionColumnSpecType"
                  },
                  "XAxis": [],
                  "YAxis": [],
                  "AdHocFields": 13,
                  "FormatVersion": 5,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var scatterVSDataSpec = new ScatterVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 13,
                FormatVersion = 5,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Category = new Dom.Visualizations.DimensionColumn(),
                XAxis = new List<Dom.Visualizations.MeasureColumn>(),
                YAxis = new List<Dom.Visualizations.MeasureColumn>(),
            };

            // Act
            var actualJson = scatterVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
