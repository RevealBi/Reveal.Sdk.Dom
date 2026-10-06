using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Reveal.Sdk.Dom.Tests
{
    public class RdashDocumentFixture
    {
        [Fact]
        public void RdashDocument_DefaultConstructor_ShouldSetDefaultValues()
        {
            // Arrange
            var document = new RdashDocument();

            // Act

            // Assert
            Assert.Equal("New Dashboard", document.Title);
            Assert.Null(document.Description);
            Assert.Equal(Theme.Mountain, document.Theme);
            Assert.Equal(GlobalConstants.RdashDocument.CreatedWith, document.CreatedWith);
            Assert.Equal(string.Empty, document.SavedWith);
            Assert.Equal(6, document.FormatVersion);
            Assert.True(document.UseAutoLayout);
            Assert.Null(document.Tags);
            Assert.Empty(document.DataSources);
            Assert.Empty(document.Filters);
            Assert.Empty(document.GlobalVariables);
            Assert.Empty(document.Visualizations);
        }

        [Fact]
        public void RdashDocument_TitleConstructor_ShouldSetTitle()
        {
            // Arrange
            var title = "My Dashboard";

            // Act
            var document = new RdashDocument(title);

            // Assert
            Assert.Equal(title, document.Title);
        }

        [Fact]
        public void RdashDocument_Import_ShouldImportVisualizations()
        {
            // Arrange
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var sourceDocument = new RdashDocument();
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new GridVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new PivotVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            RdashDocumentValidator.Validate(sourceDocument);

            // Act
            var document = new RdashDocument();
            document.Import(sourceDocument);
            document.Validate();

            // Assert
            Assert.Equal(3, document.Visualizations.Count);
            Assert.Equal(sourceDocument.Visualizations[0].ChartType, document.Visualizations[0].ChartType);
            Assert.NotEqual(sourceDocument.Visualizations[1].Id, document.Visualizations[0].Id);
            Assert.Equal(sourceDocument.Visualizations[1].ChartType, document.Visualizations[1].ChartType);
            Assert.NotEqual(sourceDocument.Visualizations[2].Id, document.Visualizations[1].Id);
            Assert.Equal(sourceDocument.Visualizations[2].ChartType, document.Visualizations[2].ChartType);
            Assert.NotEqual(sourceDocument.Visualizations[2].Id, document.Visualizations[2].Id);
            Assert.Equal(2, document.DataSources.Count);
        }

        [Fact]
        public void RdashDocument_Import_ShouldImportSingleVisualization()
        {
            // Arrange
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var sourceDocument = new RdashDocument();
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new GridVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new PivotVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            sourceDocument.Validate();

            // Act
            var document = new RdashDocument();
            document.Import(sourceDocument, sourceDocument.Visualizations[1].Id);
            document.Validate();

            // Assert
            Assert.Single(document.Visualizations);
            Assert.Equal(sourceDocument.Visualizations[1].Title, document.Visualizations[0].Title);
            Assert.Equal(sourceDocument.Visualizations[1].ChartType, document.Visualizations[0].ChartType);
            Assert.Equal(sourceDocument.Visualizations[1].IsTitleVisible, document.Visualizations[0].IsTitleVisible);
            Assert.Equal(sourceDocument.Visualizations[1].ColumnSpan, document.Visualizations[0].ColumnSpan);
            Assert.Equal(sourceDocument.Visualizations[1].RowSpan, document.Visualizations[0].RowSpan);
            Assert.Equal(sourceDocument.Visualizations[1].Description, document.Visualizations[0].Description);
            Assert.Equal(sourceDocument.Visualizations[1].DataDefinition.DataSourceItem.DataSourceId, document.Visualizations[0].DataDefinition.DataSourceItem.DataSourceId);
            Assert.Equal(sourceDocument.Visualizations[1].Filters.Count, document.Visualizations[0].Filters.Count);
            Assert.Equal(sourceDocument.Visualizations[1].FilterBindings.Count, document.Visualizations[0].FilterBindings.Count);
            Assert.NotEqual(sourceDocument.Visualizations[1].Id, document.Visualizations[0].Id);
            Assert.Equal(2, document.DataSources.Count);
        }

        [Fact]
        public void RdashDocument_Import_ShouldImportVisualizationMultipleTimes()
        {
            // Arrange
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var sourceDocument = new RdashDocument();
            var visualization = new GridVisualization(dataSourceItem);
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(visualization);
            sourceDocument.Visualizations.Add(new PivotVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            sourceDocument.Validate();

            // Act
            var document = new RdashDocument();
            for (int i = 0; i < 3; i++)
            {
                document.Import(sourceDocument, visualization);
            }
            document.Validate();

            // Assert
            Assert.Equal(3, document.Visualizations.Count);
            foreach (var importedVisualization in document.Visualizations)
            {
                Assert.Equal(visualization.Title, importedVisualization.Title);
                Assert.Equal(visualization.ChartType, importedVisualization.ChartType);
                Assert.Equal(visualization.IsTitleVisible, importedVisualization.IsTitleVisible);
                Assert.Equal(visualization.ColumnSpan, importedVisualization.ColumnSpan);
                Assert.Equal(visualization.RowSpan, importedVisualization.RowSpan);
                Assert.Equal(visualization.Description, importedVisualization.Description);
                Assert.Equal(visualization.DataDefinition.DataSourceItem.DataSourceId, importedVisualization.DataDefinition.DataSourceItem.DataSourceId);
                Assert.Equal(visualization.Filters.Count, importedVisualization.Filters.Count);
                Assert.Equal(visualization.FilterBindings.Count, importedVisualization.FilterBindings.Count);
                Assert.NotEqual(visualization.Id, importedVisualization.Id);
            }
            Assert.Equal(2, document.DataSources.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RdashDocument_Import_IncludeDashboardFilters(bool includeDashboardFilters)
        {
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var sourceDocument = new RdashDocument();
            var dateFilter = new DashboardDateFilter("My Date Filter", DateFilterRule.Last(1, PeriodType.Year));
            sourceDocument.Filters.Add(dateFilter);
            var territoryFilter = new DashboardDataFilter("Territory", dataSourceItem);
            sourceDocument.Filters.Add(territoryFilter);
            var visualization = new GridVisualization(dataSourceItem).ConnectDashboardFilter(dateFilter).ConnectDashboardFilter(territoryFilter);
            sourceDocument.Visualizations.Add(visualization);

            // Ensure data sources are added to the data sources collection
            sourceDocument.Validate();

            var document = new RdashDocument();
            document.Import(sourceDocument, visualization, new ImportOptions() { IncludeDashboardFilters = includeDashboardFilters });
            document.Validate();

            Assert.Single(document.Visualizations);
            Assert.Equal(2, document.DataSources.Count);
            if (includeDashboardFilters)
            {
                Assert.Equal(2, document.Filters.Count);
                Assert.Equal(dateFilter.Title, document.Filters[0].Title);
                Assert.Equal(territoryFilter.Title, document.Filters[1].Title);

                Assert.Equal(2, document.Visualizations[0].FilterBindings.Count);
                Assert.Equal(dateFilter.Id, (document.Visualizations[0].FilterBindings[0] as DashboardDateFilterBinding).Target.DashboardFilterId);
                Assert.Equal(territoryFilter.Id, (document.Visualizations[0].FilterBindings[1] as DashboardDataFilterBinding).Target.DashboardFilterId);
            }
            else
            {
                Assert.Empty(document.Filters);
                Assert.Empty(document.Visualizations[0].FilterBindings);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void RdashDocument_Import_IncludeVisualizationFilters(bool includeVisualizationFilters)
        {
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });
            
            var sourceDocument = new RdashDocument();
            var visualization = new GridVisualization(dataSourceItem);
            visualization.Filters.Add(new VisualizationFilter("Test"));
            sourceDocument.Visualizations.Add(visualization);

            // Ensure data sources are added to the data sources collection
            sourceDocument.Validate();

            var document = new RdashDocument();
            document.Import(sourceDocument, visualization, new ImportOptions() { IncludeVisualizationFilters = includeVisualizationFilters });
            document.Validate();

            Assert.Single(document.Visualizations);
            if (includeVisualizationFilters)
            {
                Assert.Single(document.Visualizations[0].Filters);
                Assert.Equal(visualization.Filters[0].FieldName, document.Visualizations[0].Filters[0].FieldName);
            }
            else
            {
                Assert.Empty(document.Visualizations[0].Filters);
            }
        }

        [Fact]
        public void RdashDocument_Import_WithNullDocument_ShouldThrowArgumentNullException()
        {
            // Arrange
            var document = new RdashDocument();
            RdashDocument sourceDocument = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => document.Import(sourceDocument));
        }

        [Fact]
        public void RdashDocument_Import_WithNullVisualizationId_ShouldThrowArgumentException()
        {
            // Arrange
            var document = new RdashDocument();
            var sourceDocument = new RdashDocument();
            var visualizationId = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => document.Import(sourceDocument, visualizationId));
        }

        [Fact]
        public void RdashDocument_Import_WithNullVisualization_ShouldThrowArgumentNullException()
        {
            // Arrange
            var document = new RdashDocument();
            var sourceDocument = new RdashDocument();
            IVisualization visualization = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => document.Import(sourceDocument, visualization));
        }

        [Fact]
        public void RdashDocument_Load_WithFilePath_ShouldLoadDocumentFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "Sales.rdash");

            // Act
            var document = RdashDocument.Load(filePath);

            // Assert
            Assert.NotNull(document);
            // Add more assertions based on the expected content of the loaded document
        }

        [Fact]
        public void RdashDocument_Load_WithStream_ShouldLoadDocumentFromStream()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "Sales.rdash");
            var stream = File.OpenRead(filePath);

            // Act
            var document = RdashDocument.Load(stream);

            // Assert
            Assert.NotNull(document);
            // Add more assertions based on the expected content of the loaded document
        }

        [Fact]
        public void RdashDocument_LoadFromJson_ShouldLoadDocumentFromJsonString()
        {
            // Arrange
            var json = """
            {
                "Title":"My Dashboard",
                "ThemeName":"Mountain"
            }
            """;

            // Act
            var document = RdashDocument.LoadFromJson(json);

            // Assert
            Assert.NotNull(document);
            Assert.Equal("My Dashboard", document.Title);
            Assert.Equal(Theme.Mountain, document.Theme);
            // Add more assertions based on the expected content of the loaded document
        }

        [Fact]
        public void RdashDocument_Save_ShouldSaveDocumentToFile()
        {
            var filePath = Path.Combine(Path.GetTempPath(), $"{Path.GetTempFileName()}.rdash");

            try
            {
                var dashboard = new RdashDocument();

                if (File.Exists(filePath))
                    File.Delete(filePath);

                dashboard.Save(filePath);

                Assert.Equal("Reveal.Sdk.Dom", dashboard.SavedWith);
                Assert.True(File.Exists(filePath));
            }
            catch
            {
                throw;
            }
            finally
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
        }

        [Fact]
        public void RdashDocument_ToJsonString_ShouldReturnJsonStringRepresentation()
        {
            // Arrange
            var document = new RdashDocument();
            string expectedJson = """
            {
              "Title": "New Dashboard",
              "ThemeName": "rvDashboardMountainTheme",
              "CreatedWith": "Reveal.Sdk.Dom",
              "SavedWith": "",
              "FormatVersion": 6,
              "UseAutoLayout": true,
              "DataSources": [],
              "GlobalFilters": [],
              "GlobalVariables": [],
              "Widgets": []
            }
            """;

            // Act
            var jsonString = document.ToJsonString();

            // Deserialize JSON strings to JObjects to make comparing them easier
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(jsonString);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        [Fact]
        public void ImportThrows_WhenDocumentIsNull()
        {
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(new DataSourceItem()));

            Assert.Throws<ArgumentNullException>(() => document.Import(null));
            Assert.Throws<ArgumentNullException>(() => document.Import(null, document.Visualizations[0]));
            Assert.Throws<ArgumentNullException>(() => document.Import(null, "TEST"));
        }

        [Fact]
        public void ImportThrows_WhenVisualizationIdIsNull()
        {
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(new DataSourceItem()));

            Assert.Throws<ArgumentException>(() => document.Import(new RdashDocument(), (string)null));
        }

        [Fact]
        public void ImportThrows_WhenVisualizationIsNull()
        {
            var document = new RdashDocument();
            document.Visualizations.Add(new GridVisualization(new DataSourceItem()));

            Assert.Throws<ArgumentNullException>(() => document.Import(new RdashDocument(), (IVisualization)null));
        }
    }
}
