using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations.VisualizationSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.VisualizationSpecs
{
    public class TreeMapVisualizationDataSpecFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithoutParameters()
        {
            // Act
            var treeMapVSDataSpec = new TreeMapVisualizationDataSpec();

            // Assert
            Assert.Equal(SchemaTypeNames.TreeMapVisualizationDataSpecType, treeMapVSDataSpec.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "_type": "AssetVisualizationSettingsType",
                  "Value": [],
                  "AdHocFields": 11,
                  "FormatVersion": 3,
                  "AdHocExpandedElements": [],
                  "Rows": []
                }
                """;
            var treeMapVSDataSpec = new TreeMapVisualizationDataSpec()
            {
                AdHocExpandedElements = new List<Dom.Visualizations.AdHocExpandedElement>(),
                AdHocFields = 11,
                FormatVersion = 3,
                Rows = new List<Dom.Visualizations.DimensionColumn>(),
                SchemaTypeName = SchemaTypeNames.AssetVisualizationSettingsType,
                Value = new List<Dom.Visualizations.MeasureColumn>(),
            };

            // Act
            var actualJson = treeMapVSDataSpec.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
