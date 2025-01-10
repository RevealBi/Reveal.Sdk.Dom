using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class CompositeChartVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var compositeChartVSDataSpec = new CompositeChartVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.CompositeChartVisualizationDataSpecType, compositeChartVSDataSpec.SchemaTypeName);
            Assert.Empty(compositeChartVSDataSpec.Chart1);
            Assert.Empty(compositeChartVSDataSpec.Chart2);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "CompositeChartVisualizationDataSpecType",
                  "Chart1": [],
                  "Chart2": [],
                  "AdHocFields": 15,
                  "FormatVersion": 3,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var compositeChartVSDataSpec = new CompositeChartVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 15,
                FormatVersion = 3,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                Chart1 = new List<Dom.Visualizations.MeasureColumn>(),
                Chart2 = new List<Dom.Visualizations.MeasureColumn>()
            };

            // Act
            var actualJson = compositeChartVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
