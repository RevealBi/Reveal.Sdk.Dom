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
    public class ServiceAdditionalTableFixture
    {
        [Fact]
        public void ToJsonString_CreateCorrectJson_WithoutConditions()
        {
            // Arrange
            var expectedJson = """
                {
                  "Alias": "TestAlias",
                  "DataProcessingTask": {
                    "InputFields": [
                      {
                        "ResultColumnName": "TestResultName",
                        "InputColumnName": "TestInputName",
                        "FixedValue": "TestFixedValue"
                      }
                    ],
                    "OutputFields": [
                      {
                        "OutputColumnName": "TestOutputName",
                        "ResultColumnName": "TestResultName",
                        "DataType": "DateTime",
                        "FeatureName": "TestFeatureName",
                        "IsBoolean": false,
                        "ReferenceColumn": "TestReferenceName"
                      }
                    ],
                    "Parameters": {}
                  }
                }
                """;
            var serviceAdditionalTable = new ServiceAdditionalTable()
            {
                Alias = "TestAlias",
                DataProcessingTask = new DataProcessingTask()
                {
                    InputFields = new List<DataProcessingInputField>()
                    {
                        new DataProcessingInputField()
                        {
                            FixedValue = "TestFixedValue",
                            InputColumnName = "TestInputName",
                            ResultColumnName = "TestResultName"
                        }
                    },
                    OutputFields = new List<DataProcessingOutputField>()
                    {
                        new DataProcessingOutputField()
                        {
                            DataType = DataType.DateTime,
                            FeatureName = "TestFeatureName",
                            IsBoolean = false,
                            OutputColumnName = "TestOutputName",
                            ReferenceColumn = "TestReferenceName",
                            ResultColumnName = "TestResultName"
                        }
                    }
                }
            };

            // Act
            var actualJson = serviceAdditionalTable.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
