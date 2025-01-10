using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class DateTimeFieldSettingsFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new DateTimeFieldSettings();

            // Assert
            Assert.Equal(SchemaTypeNames.DateTimeFieldSettingsType, instance.SchemaTypeName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "_type": "DateTimeFieldSettingsType",
              "DateFiscalYearStartMonth": 1,
              "DisplayInLocalTimeZone": true
            }
            """;

            var instance = new DateTimeFieldSettings()
            {
                DateFiscalYearStartMonth = 1,
                DisplayInLocalTimeZone = true,
            };

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
