using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class GoogleSheetsDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndIdentifier_AsProvided()
        {
            // Arrange
            var title = "Test title";
            var identifier = "testIdentifier";

            // Act
            var dataSourceItem = new GoogleSheetsDataSourceItem(title, identifier);

            // Assert
            Assert.Equal(title, dataSourceItem.Title);
            Assert.Equal(identifier, dataSourceItem.Identifier);
        }

        [Fact]
        public void Constructor_SetsResourceItemToGoogleDrive_WhenCreate()
        {
            // Arrange
            var title = "Test title";
            var identifier = "testIdentifier";

            // Act
            var dataSourceItem = new GoogleSheetsDataSourceItem(title, identifier);

            // Assert
            Assert.Equal(DataSourceProvider.GoogleDrive, dataSourceItem.ResourceItemDataSource.Provider);
            Assert.Equal(identifier, dataSourceItem.Identifier);
        }

        [Fact]
        public void FirstRowContainsLabels_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", "testIdentifier");

            // Act 1
            dataSourceItem.FirstRowContainsLabels = true;

            // Assert
            Assert.True(dataSourceItem.FirstRowContainsLabels);
            Assert.True(dataSourceItem.Parameters.GetValue<bool>("TITLES_IN_FIRST_ROW"));

            // Act 1
            dataSourceItem.FirstRowContainsLabels = false;

            // Assert
            Assert.False(dataSourceItem.FirstRowContainsLabels);
            Assert.False(dataSourceItem.Parameters.GetValue<bool>("TITLES_IN_FIRST_ROW"));
        }

        [Fact]
        public void Identifier_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", "testIdentifier");
            var testIdentifier = "anotherTestIdentifier";

            // Act 1
            dataSourceItem.Identifier = testIdentifier;

            // Assert
            Assert.Equal(testIdentifier, dataSourceItem.Identifier);
            Assert.Equal(testIdentifier, dataSourceItem.ResourceItem.Properties.GetValue<string>("Identifier"));
        }

        [Fact]
        public void ResourceItem_IsGoogleDriveDataSourceItem_WhenCreated()
        {
            // Arrange
            var testIdentifier = "testIdentifier";

            // Act
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", testIdentifier);

            // Assert
            Assert.Equal(DataSourceProvider.GoogleDrive, dataSourceItem.ResourceItem.DataSource.Provider);
            Assert.IsType<GoogleDriveDataSourceItem>(dataSourceItem.ResourceItem);
            Assert.Equal(testIdentifier, dataSourceItem.ResourceItem.Properties.GetValue<string>("Identifier"));
        }

        [Fact]
        public void NamedRange_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleSheetsDataSource();
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", "testIdentifier");
            var namedRange = "NamedRangeTest";

            // Act
            dataSourceItem.NamedRange = namedRange;

            // Assert
            Assert.Equal(namedRange, dataSourceItem.NamedRange);
            Assert.Equal(namedRange, dataSourceItem.Properties.GetValue<string>("NamedRange"));
        }

        [Fact]
        public void PivotTable_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleSheetsDataSource();
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", "testIdentifier");
            var pivotTable = "PivotTableTest";

            // Act
            dataSourceItem.PivotTable = pivotTable;

            // Assert
            Assert.Equal(pivotTable, dataSourceItem.PivotTable);
            Assert.Equal(pivotTable, dataSourceItem.Properties.GetValue<string>("PivotTable"));
        }

        [Fact]
        public void Sheet_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleSheetsDataSource();
            var dataSourceItem = new GoogleSheetsDataSourceItem("Test", "testIdentifier");
            var sheet = "SheetTest";

            // Act
            dataSourceItem.Sheet = sheet;

            // Assert
            Assert.Equal(sheet, dataSourceItem.Sheet);
            Assert.Equal(sheet, dataSourceItem.Properties.GetValue<string>("Sheet"));
        }

        [Fact]
        public void RDashDocument_CreateTwoDataSources_WhenUseGoogleSheetDataSourceItem()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSource = new GoogleSheetsDataSource();
            var dataSourceItem = new GoogleSheetsDataSourceItem("My Google Sheet", "testIdentifier").SetFields(new List<IField>() { new TextField("Test") });

            dataSourceItem.Sheet = "Sheet1";
            dataSourceItem.FirstRowContainsLabels = true;

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Act
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.GSHEET, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.GoogleSheets, document.DataSources[0].Provider);
            Assert.Equal(DataSourceProvider.GoogleDrive, document.DataSources[1].Provider);
        }

        [Fact]
        public void RDashDocument_HasCorrectDataSourceItem_WhenLoadFromFile()
        {
            // Arrange
            var filePath = Path.Combine(Environment.CurrentDirectory, "Dashboards", "TestGoogleSheet.rdash");

            // Act
            var document = RdashDocument.Load(filePath);
            var gsheetDataSource = document.DataSources[0];
            var driveDataSource = document.DataSources[1];
            var dataSourceItem = document.Visualizations[0].DataDefinition.DataSourceItem;

            var value = dataSourceItem.Parameters.GetValue<bool>("TITLES_IN_FIRST_ROW");

            // Assert
            Assert.Equal(gsheetDataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(DataSourceProvider.GoogleSheets, gsheetDataSource.Provider);
            Assert.Equal(DataSourceProvider.GoogleDrive, driveDataSource.Provider);
            Assert.Equal("Class Data", dataSourceItem.Properties.GetValue<string>("Sheet"));
            Assert.True(dataSourceItem.Parameters.GetValue<bool>("TITLES_IN_FIRST_ROW"));
            Assert.Equal("1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms", dataSourceItem.ResourceItem.Properties.GetValue<string>("Identifier"));    
        }

        [Fact]
        public void RDashDocument_CreateExpectedJson_WhenExport()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSource = new GoogleSheetsDataSource();
            var dataSourceItem = new GoogleSheetsDataSourceItem("My Google Sheet", "testIdentifier").SetFields(new List<IField>() { new TextField("Test") });

            dataSourceItem.Sheet = "Sheet1";
            dataSourceItem.FirstRowContainsLabels = true;

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Act
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.GSHEET, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.GoogleSheets, document.DataSources[0].Provider);
            Assert.Equal(DataSourceProvider.GoogleDrive, document.DataSources[1].Provider);
        }
    }
}
