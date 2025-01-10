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
    public class DataProcessingTaskFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new DataProcessingTask();

            // Assert
            Assert.Empty(instance.InputFields);
            Assert.Empty(instance.OutputFields);
            Assert.Empty(instance.Parameters);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var instance = new DataProcessingTask();
            instance.InputFields = new List<DataProcessingInputField>() { 
                new()
                {
                    ResultColumnName = "Test ResultColumnName 1",
                    InputColumnName = "Test InputColumnName 1",
                    FixedValue = "Test FixedValue 1",
                }
            };
            instance.OutputFields = new List<DataProcessingOutputField>() {
                new()
                {
                    OutputColumnName = "Test OutputColumnName",
                    ResultColumnName = "Test ResultColumnName",
                    DataType = DataType.Number,
                    FeatureName = "Test FeatureName",
                    IsBoolean = true,
                    ReferenceColumn = "Test ReferenceColumn"
                }
            };
            instance.Parameters = new Dictionary<string, object>() { { "Test Key", "Test Value" } };

            var expectedJson = """
            {
              "InputFields": [
                {
                  "ResultColumnName": "Test ResultColumnName 1",
                  "InputColumnName": "Test InputColumnName 1",
                  "FixedValue": "Test FixedValue 1"
                }
              ],
              "OutputFields": [
                {
                  "OutputColumnName": "Test OutputColumnName",
                  "ResultColumnName": "Test ResultColumnName",
                  "DataType": "Number",
                  "FeatureName": "Test FeatureName",
                  "IsBoolean": true,
                  "ReferenceColumn": "Test ReferenceColumn"
                }
              ],
              "Parameters": {
                "Test Key": "Test Value"
              }
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
