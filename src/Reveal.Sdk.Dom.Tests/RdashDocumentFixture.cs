using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            RdashDocumentValidator.Validate(sourceDocument);

            // Act
            var document = new RdashDocument();
            document.Import(sourceDocument);

            // Assert
            Assert.Equal(3, document.Visualizations.Count);
            Assert.Contains(sourceDocument.Visualizations[0], document.Visualizations);
            Assert.Contains(sourceDocument.Visualizations[1], document.Visualizations);
            Assert.Contains(sourceDocument.Visualizations[2], document.Visualizations);
            Assert.Equal(2, document.DataSources.Count);
        }

        [Fact]
        public void RdashDocument_Import_ShouldImportSingleVisualization()
        {
            // Arrange
            var dataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "", "").SetFields(new List<IField>() { new TextField("Test") });

            var sourceDocument = new RdashDocument();
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));
            sourceDocument.Visualizations.Add(new KpiTimeVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            sourceDocument.Validate();

            // Act
            var document = new RdashDocument();
            document.Import(sourceDocument, sourceDocument.Visualizations[1].Id);

            // Assert
            Assert.Single(document.Visualizations);
            Assert.Equal(sourceDocument.Visualizations[1], document.Visualizations[0]);
            Assert.Equal(2, document.DataSources.Count);
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
            var json = "{\"Title\":\"My Dashboard\",\"ThemeName\":\"Mountain\"}";

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
            string expectedJson = @"
            {
              ""Title"": ""New Dashboard"",
              ""ThemeName"": ""rvDashboardMountainTheme"",
              ""CreatedWith"": ""Reveal.Sdk.Dom"",
              ""SavedWith"": """",
              ""FormatVersion"": 6,
              ""UseAutoLayout"": true,
              ""DataSources"": [],
              ""GlobalFilters"": [],
              ""GlobalVariables"": [],
              ""Widgets"": []
            }";

            // Act
            var jsonString = document.ToJsonString();

            // Deserialize JSON strings to JObjects to make comparing them easier
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(jsonString);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        [Fact]
        public void ToJsonString_IsValidSchema()
        {
            var schemaJson = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Schemas", "RdashDocument.json"));
            var schema = JSchema.Parse(schemaJson);

            var dashboard = new RdashDocument()
            {
                Title = "New Dashboard",
                Description = "This is a test dashboard",
                Theme = Theme.Aurora,
                Tags = "tag1,tag2,tag3"
            };
            var json = dashboard.ToJsonString();

            var jsonDocument = JObject.Parse(json);

            bool isValid = jsonDocument.IsValid(schema);

            Assert.True(isValid);
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

        private RdashDocument TestDashboardForJoins()
        {
            var dashboard = new RdashDocument();

            var productDataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "http://localhost:8080/api", "Sales")
                .SetFields(new List<IField>()
                {
                    new TextField("Product"),
                    new NumberField("Revenue")
                });

            var productDetailsDataSourceItem = new DataSourceItemFactory().Create(DataSourceType.REST, "http://localhost:8080/api", "ProductDetails")
                .SetFields(new List<IField>()
                {
                    new TextField("Product"),
                    new TextField("Product2"),
                    new TextField("Category")
                });

            productDataSourceItem.Join("A", "Product", "Product", productDetailsDataSourceItem);

            var visualization = new GridVisualization("JoinTest",productDataSourceItem);

            ((ITabularColumns)visualization).Columns.Add(new TabularColumn("Product"));
            ((ITabularColumns)visualization).Columns.Add(new TabularColumn("Revenue"));
            ((ITabularColumns)visualization).Columns.Add(new TabularColumn("Category"));

            dashboard.Visualizations.Add(visualization);

            return dashboard;
        }

        [Fact]
        public void ToJsonString_DontDuplicateJoins()
        {
            var dashboard = TestDashboardForJoins();

            //sanity check
            var json = dashboard.ToJsonString();
            var jsonDocument = JObject.Parse(json);

            var joins = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[*]");

            Assert.Equal(1, joins.Count());


            //second ToJsonString should still return a single join
            json = dashboard.ToJsonString();
            jsonDocument = JObject.Parse(json);

            joins = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[*]");

            Assert.Equal(1, joins.Count());
        }

        [Fact]
        public void ToJsonString_ReplaceUpdatedJoin()
        {
            var dashboard = TestDashboardForJoins();

            //sanity check
            var json = dashboard.ToJsonString();
            var jsonDocument = JObject.Parse(json);

            var firstJoinRightFieldName = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[0].JoinConditions[0].RightFieldName").First().ToString();
            var joins = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[*]");

            Assert.Equal(1, joins.Count());
            Assert.Equal("A.[Product]", firstJoinRightFieldName);

            //retrieve the right DSI to not create it again
            var productDetailsDataSourceItem = dashboard.Visualizations[0].DataDefinition.AsTabular().DataSourceItem.JoinTables[0].DataDefinition.DataSourceItem;

            dashboard.Visualizations[0].DataDefinition.AsTabular().DataSourceItem.Join("A","Product","Product2",productDetailsDataSourceItem);

            //second ToJsonString should have the updated value
            json = dashboard.ToJsonString();
            jsonDocument = JObject.Parse(json);

            firstJoinRightFieldName = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[0].JoinConditions[0].RightFieldName").First().ToString();
            joins = jsonDocument.SelectTokens("$.Widgets[0].DataSpec.AdditionalTables[*]");

            Assert.Equal(1, joins.Count());
            Assert.Equal("A.[Product2]", firstJoinRightFieldName);
        }
    }
}
