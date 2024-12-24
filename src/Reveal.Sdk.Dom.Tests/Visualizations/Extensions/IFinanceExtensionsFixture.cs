using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions
{
    public class IFinanceExtensionsFixture
    {
        [Fact]
        public void SetOpen_UpdateOpen_WithFieldName()
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Opens = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };
            var fieldName = "TestFieldName";
            var field = new NumberDataField(fieldName);
            field.Formatting = new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true
            };
            var expectedOpens = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetOpen(fieldName);

            // Assert
            Assert.Equivalent(expectedOpens, visualization.Opens, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetOpen_UpdateOpen_WithNumberDataField(bool isFormattingNull)
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Opens = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };

            var fieldFormat = isFormattingNull ? null : new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true,
                ApplyMkFormat = false,
                CurrencySymbol = "AUD",
                FormatType = NumberFormattingType.Currency,
                NegativeFormat = NegativeFormatType.Parenthesis,
                OverrideDefaultSettings = true,
                SchemaTypeName = "TestSchema"
            };
            var field = new NumberDataField("TestField")
            {
                Formatting = fieldFormat
            };

            var expectedOpens = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetOpen(field);

            // Assert
            Assert.Equivalent(expectedOpens, visualization.Opens, true);
        }

        [Fact]
        public void SetClose_UpdateClose_WithFieldName()
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Closes = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };
            var fieldName = "TestFieldName";
            var field = new NumberDataField(fieldName);
            field.Formatting = new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true
            };
            var expectedCloses = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetClose(fieldName);

            // Assert
            Assert.Equivalent(expectedCloses, visualization.Closes, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetClose_UpdateClose_WithNumberDataField(bool isFormattingNull)
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Closes = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };

            var fieldFormat = isFormattingNull ? null : new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true,
                ApplyMkFormat = false,
                CurrencySymbol = "AUD",
                FormatType = NumberFormattingType.Currency,
                NegativeFormat = NegativeFormatType.Parenthesis,
                OverrideDefaultSettings = true,
                SchemaTypeName = "TestSchema"
            };
            var field = new NumberDataField("TestField")
            {
                Formatting = fieldFormat
            };

            var expectedCloses = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetClose(field);

            // Assert
            Assert.Equivalent(expectedCloses, visualization.Closes, true);
        }

        [Fact]
        public void SetHigh_UpdateHigh_WithFieldName()
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Highs = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };
            var fieldName = "TestFieldName";
            var field = new NumberDataField(fieldName);
            field.Formatting = new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true
            };
            var expectedHighs = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetHigh(fieldName);

            // Assert
            Assert.Equivalent(expectedHighs, visualization.Highs, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetHigh_UpdateHigh_WithNumberDataField(bool isFormattingNull)
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Highs = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };

            var fieldFormat = isFormattingNull ? null : new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true,
                ApplyMkFormat = false,
                CurrencySymbol = "AUD",
                FormatType = NumberFormattingType.Currency,
                NegativeFormat = NegativeFormatType.Parenthesis,
                OverrideDefaultSettings = true,
                SchemaTypeName = "TestSchema"
            };
            var field = new NumberDataField("TestField")
            {
                Formatting = fieldFormat
            };

            var expectedHighs = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetHigh(field);

            // Assert
            Assert.Equivalent(expectedHighs, visualization.Highs, true);
        }

        [Fact]
        public void SetLow_UpdateLow_WithFieldName()
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Lows = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };
            var fieldName = "TestFieldName";
            var field = new NumberDataField(fieldName);
            field.Formatting = new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true
            };
            var expectedLows = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetLow(field);

            // Assert
            Assert.Equivalent(expectedLows, visualization.Lows, true);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetLow_UpdateLow_WithNumberDataField(bool isFormattingNull)
        {
            // Arrange
            var visualization = new MockIFiananceVisualization() { Lows = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } } };

            var fieldFormat = isFormattingNull ? null : new NumberFormatting()
            {
                DecimalDigits = 0,
                ShowGroupingSeparator = true,
                ApplyMkFormat = false,
                CurrencySymbol = "AUD",
                FormatType = NumberFormattingType.Currency,
                NegativeFormat = NegativeFormatType.Parenthesis,
                OverrideDefaultSettings = true,
                SchemaTypeName = "TestSchema"
            };
            var field = new NumberDataField("TestField")
            {
                Formatting = fieldFormat
            };

            var expectedLows = new List<MeasureColumn> { new MeasureColumn() { DataField = field } };

            // Act
            visualization.SetLow(field);

            // Assert
            Assert.Equivalent(expectedLows, visualization.Lows, true);
        }


        private class MockIFiananceVisualization : IFinance
        {
            public List<MeasureColumn> Opens { get; set; } = new List<MeasureColumn>();

            public List<MeasureColumn> Highs { get; set; } = new List<MeasureColumn>();

            public List<MeasureColumn> Lows { get; set; } = new List<MeasureColumn>();

            public List<MeasureColumn> Closes { get; set; } = new List<MeasureColumn>();
        }
    }
}
