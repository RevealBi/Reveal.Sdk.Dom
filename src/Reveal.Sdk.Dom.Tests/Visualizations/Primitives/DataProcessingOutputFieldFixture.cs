using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class DataProcessingOutputFieldFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new DataProcessingOutputField();

            // Assert
            Assert.Equal(DataType.String, instance.DataType);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "OutputColumnName": "Test OutputColumnName",
              "ResultColumnName": "Test ResultColumnName",
              "DataType": "Number",
              "FeatureName": "Test FeatureName",
              "IsBoolean": true,
              "ReferenceColumn": "Test ReferenceColumn"
            }
            """;

            var instance = new DataProcessingOutputField();
            instance.OutputColumnName = "Test OutputColumnName";
            instance.ResultColumnName = "Test ResultColumnName";
            instance.DataType = DataType.Number;
            instance.FeatureName = "Test FeatureName";
            instance.IsBoolean = true;
            instance.ReferenceColumn = "Test ReferenceColumn";

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
