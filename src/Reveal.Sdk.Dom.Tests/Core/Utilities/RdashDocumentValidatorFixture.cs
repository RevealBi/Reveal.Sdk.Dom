using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Core.Utilities
{
    public class RdashDocumentValidatorFixture
    {
        private class DebugOutputListener : TraceListener
        {
            private readonly StringWriter _stringWriter = new StringWriter();

            public override void Write(string message)
            {
                _stringWriter.Write(message);
            }

            public override void WriteLine(string message)
            {
                _stringWriter.WriteLine(message);
            }

            public string GetOutput()
            {
                return _stringWriter.ToString();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _stringWriter.Dispose();
                }
                base.Dispose(disposing);
            }
        }

        [Fact]
        public void Validate_AddsDataSources_ToRdashDocument()
        {
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var document = new RdashDocument();
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));

            Assert.Empty(document.DataSources);

            RdashDocumentValidator.Validate(document);

            Assert.True(document.DataSources.Skip(1).Any());
        }

        [Fact]
        public void Validate_PreventsDuplicateDataSources_FromVisualizations()
        {
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var document = new RdashDocument();
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));

            Assert.Empty(document.DataSources);

            RdashDocumentValidator.Validate(document);

            var jsonDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.JSON);

            Assert.Single(jsonDataSources);
        }

        [Fact]
        public void Validate_PreventsDuplicateDataSources_FromVisualizationsAndDataSources()
        {
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });
            var dataSource = dataSourceItem.DataSource;

            var document = new RdashDocument();
            document.DataSources.Add(dataSource);
            document.DataSources.Add(dataSource);
            document.DataSources.Add(dataSource);
            document.DataSources.Add(dataSource);

            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            document.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));

            RdashDocumentValidator.Validate(document);

            var jsonDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.JSON);
            var restDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.REST);

            Assert.Equal(2, document.DataSources.Count);
            Assert.Single(jsonDataSources);
            Assert.Single(restDataSources);
        }

        [Fact]
        public void Validate_Adds_Fields()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource()).SetFields(new List<IField> { new TextField() });

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            RdashDocumentValidator.Validate(document);

            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().Fields);
        }

        [Fact]
        public void Validate_ThrowsException_When_Fields_Are_Null()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource());
            dataSourceItem.Fields = null;

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            var exception = Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document));

            Assert.Equal("Fields for DataSourceItem Test is null or empty.", exception.Message);
        }

        [Fact]
        public void Validate_ThrowsException_When_Fields_Are_Empty()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource()).SetFields(new List<IField>());

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            var exception = Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document));

            Assert.Equal("Fields for DataSourceItem Test is null or empty.", exception.Message);
        }

        [Fact]
        public void Validate_PreventsDuplicate_Fields()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource()).SetFields(new List<IField>
            {
                new TextField("Test"),
                new TextField("Test"),
                new TextField("Test")
            });

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            RdashDocumentValidator.Validate(document);

            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().Fields);
        }

        [Fact]
        public void Validate_PreventsDuplicate_JoinTables()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource()).SetFields(new List<IField> { new TextField() });
            var joinConditions = new List<JoinCondition> { new JoinCondition("left", "right") };
            var dataSourceItemToJoin = new DataSourceItem().SetFields(new List<IField> { new TextField() });

            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);
            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            Assert.Equal(2,document.Visualizations[0].DataDefinition.AsTabular().JoinTables.Count);

            RdashDocumentValidator.Validate(document);

            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().JoinTables);
        }

        [Fact]
        public void Validate_PreventsDuplicates_OnMultipleValidations()
        {
            var dataSourceItem = new DataSourceItem("Test", new DataSource()).SetFields(new List<IField> { new TextField() });
            var joinConditions = new List<JoinCondition> { new JoinCondition("left", "right") };
            var dataSourceItemToJoin = new DataSourceItem().SetFields(new List<IField> { new TextField() });

            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);

            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().JoinTables);

            RdashDocumentValidator.Validate(document);

            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().JoinTables);

            RdashDocumentValidator.Validate(document);

            //should still be single after another validate call
            Assert.Single(document.Visualizations[0].DataDefinition.AsTabular().JoinTables);
        }

        [Fact]
        public void Validate_Fixes_REST_DataSources()
        {
            var jsonDataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "JSON", "JSON").SetFields(new List<IField>() { new TextField("Test") });
            var csvDataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "CSV", "CSV").SetFields(new List<IField>() { new TextField("Test") }) as RestDataSourceItem;
            csvDataSourceItem.UseCsv();
            var excelDataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "Excel", "Excel").SetFields(new List<IField>() { new TextField("Test") }) as RestDataSourceItem;
            excelDataSourceItem.UseExcel();

            var document = new RdashDocument();

            document.Visualizations.Add(new GridVisualization(jsonDataSourceItem));
            document.Visualizations.Add(new GridVisualization(csvDataSourceItem));
            document.Visualizations.Add(new GridVisualization(excelDataSourceItem));

            RdashDocumentValidator.Validate(document);

            var jsonDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.JSON);
            var csvDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.CSV);
            var excelDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.MicrosoftExcel);
            var restDataSources = document.DataSources.Where(x => x.Provider == DataSourceProvider.REST);

            Assert.Equal(6, document.DataSources.Count);
            Assert.Single(jsonDataSources);
            Assert.Single(csvDataSources);
            Assert.Single(excelDataSources);
            Assert.Equal(3, restDataSources.Count());
        }

        [Fact]
        public void Validate_Warns_WhenDataSourceItem_WithDataSourceId_CantFindDataSource()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem() { DataSourceId = "TEST", Fields = new List<IField>() { new TextField() } };
            var viz = new GridVisualization("TEST", dataSourceItem)
            {
                Title = "Test Visualization",
            };
            var document = new RdashDocument("Test");
            document.Visualizations.Add(viz);

            // Act & Assert
            using (var listener = new DebugOutputListener())
            {
                Trace.Listeners.Add(listener);

                RdashDocumentValidator.Validate(document);

                Trace.Flush(); // Ensure trace output is flushed

                Trace.Listeners.Remove(listener);

                var output = listener.GetOutput().Trim()
                    .Replace("\r\n", "\n")  // Normalize Windows line endings
                    .Replace("\r", "\n");   // Normalize old Mac line endings (just in case)

                // Assert using a more resilient check
                Assert.Contains("warn: Warning: Data source with id TEST not found in the RdashDocument.DataSources collection.", output);
            }
        }

        [Fact]
        public void Validate_ThrowsException_WhenDataSourceItemIsNull()
        {
            // Arrange
            var viz = new GridVisualization("TEST", null)
            {
                Title = "Test Visualization",
                DataDefinition = new TabularDataDefinition()
            };
            var document = new RdashDocument("Test");
            document.Visualizations.Add(viz);

            // Act & Assert
            Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document));
        }

        [Fact]
        public void Validate_ThrowsException_WhenFieldIsNull()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem()
            {
                Fields = new List<IField> { null }
            };

            var viz = new GridVisualization("TEST", dataSourceItem)
            {
                Title = "Test Visualization",
                DataDefinition = new TabularDataDefinition()
            };

            var document = new RdashDocument("Test");
            document.Visualizations.Add(viz);

            // Act & Assert
            Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document));
        }

        [Fact]
        public void Validate_ThrowsException_WhenFieldsAreNullOrEmpty()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem()
            {
                Fields = new List<IField> { null }
            };

            var viz = new GridVisualization("TEST", dataSourceItem)
            {
                Title = "Test Visualization",
                DataDefinition = new TabularDataDefinition()
            };

            var document = new RdashDocument("Test");
            document.Visualizations.Add(viz);

            // Act & Assert
            Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document));

            // Arrange
            var dataSourceItem2 = new DataSourceItem()
            {
                Fields = new List<IField>()
            };

            var viz2 = new GridVisualization("TEST", dataSourceItem2)
            {
                Title = "Test Visualization",
                DataDefinition = new TabularDataDefinition()
            };

            var document2 = new RdashDocument("Test");
            document2.Visualizations.Add(viz2);

            // Act & Assert
            Assert.Throws<Exception>(() => RdashDocumentValidator.Validate(document2));
        }
    }
}
