using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class NumberFormattingFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WhenConstructed()
        {
            // Act
            var instance = new NumberFormatting();

            // Assert
            Assert.Equal(SchemaTypeNames.NumberFormattingSpecType, instance.SchemaTypeName);
            Assert.Equal("$", instance.CurrencySymbol);
            Assert.Equal(2, instance.DecimalDigits);
            Assert.Equal(NumberFormattingType.Number, instance.FormatType);
            Assert.Equal(NegativeFormatType.MinusSign, instance.NegativeFormat);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
            {
              "_type": "NumberFormattingSpecType",
              "ApplyMkFormat": false,
              "CurrencySymbol": "€",
              "DecimalDigits": 4,
              "FormatType": "Currency",
              "NegativeFormat": "Parenthesis",
              "ShowGroupingSeparator": false,
              "OverrideDefaultSettings": false,
              "ShowDataLabelsInChart": true
            }
            """;

            var instance = new NumberFormatting();
            instance.DecimalDigits = 4;
            instance.CurrencySymbol = "€";
            instance.FormatType = NumberFormattingType.Currency;
            instance.NegativeFormat = NegativeFormatType.Parenthesis;

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private static JObject LegacyFormatting(JObject keys)
        {
            var formatting = new JObject { ["_type"] = "NumberFormattingSpecType", ["OverrideDefaultSettings"] = true, ["FormatType"] = "Number", ["DecimalDigits"] = 0 };
            formatting.Merge(keys);
            return formatting;
        }

        private static JToken RoundTrip(JObject formatting)
        {
            var dataSourceItem = new DataSourceItem { Title = "Data", HasTabularData = true };
            dataSourceItem.Fields.Add(new TextField("Category"));
            dataSourceItem.Fields.Add(new NumberField("Sales"));
            var document = new RdashDocument("Formats");
            document.Visualizations.Add(new BarChartVisualization("Chart", dataSourceItem).SetLabel("Category").SetValue("Sales"));
            var json = JObject.Parse(document.ToJsonString());
            json["Widgets"][0]["VisualizationDataSpec"]["Values"][0]["SummarizationField"]["Formatting"] = formatting;
            var saved = JObject.Parse(RdashDocument.LoadFromJson(json.ToString()).ToJsonString());
            return saved["Widgets"][0]["VisualizationDataSpec"]["Values"][0]["SummarizationField"]["Formatting"];
        }

        [Fact]
        public void LoadFromJson_ReadsApplyMkFormatAsAuto_WhenMKFormatIsMissing()
        {
            var formatting = RoundTrip(LegacyFormatting(new JObject { ["ApplyMkFormat"] = true }));

            Assert.Equal("Auto", (string)formatting["MKFormat"]);
            Assert.Null(formatting["ApplyMkFormat"]);
        }

        [Theory]
        [InlineData("{ \"ApplyMkFormat\": false }", LargeNumberFormat.None)]
        [InlineData("{}", LargeNumberFormat.None)]
        [InlineData("{ \"ApplyMkFormat\": true }", LargeNumberFormat.Auto)]
        [InlineData("{ \"MKFormat\": \"None\", \"ApplyMkFormat\": true }", LargeNumberFormat.None)]
        [InlineData("{ \"ApplyMkFormat\": true, \"MKFormat\": \"None\" }", LargeNumberFormat.None)]
        [InlineData("{ \"ApplyMkFormat\": true, \"MKFormat\": \"K\" }", LargeNumberFormat.K)]
        public void Deserialize_ReadsLargeNumberFormat_AsRevealDoes(string keys, LargeNumberFormat expected)
        {
            var formatting = JsonConvert.DeserializeObject<NumberFormatting>(LegacyFormatting(JObject.Parse(keys)).ToString());

            Assert.Equal(expected, formatting.LargeNumberFormat);
        }
    }
}
