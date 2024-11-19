using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class ExcelFileDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetProperties_WithTitleAndDataSourceProvided()
        {
            // Arrange
            string title = "Test Excel File";
            DataSource dataSource = new DataSource();

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.IsType<ExcelDataSource>(excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_SetProperties_WithTitleProvided()
        {
            // Arrange
            string title = "Test Excel File";

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.NotNull(excelFile.DataSource);
            Assert.IsType<ExcelDataSource>(excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);            
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_SetProperties_WithTitleAndPathProvided()
        {
            // Arrange
            string title = "Test Excel File";
            string path = "test.xlsx";

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title, path);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.NotNull(excelFile.DataSource);
            Assert.IsType<ExcelDataSource>(excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal($"local:{path}", excelFile.Path);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);            
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_SetProperties_WithTitlePathAndSheetProvided()
        {
            // Arrange
            string title = "Test Excel File";
            string path = "test.xlsx";
            string sheet = "Sheet1";

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title, path, sheet);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.NotNull(excelFile.DataSource);
            Assert.IsType<ExcelDataSource>(excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal($"local:{path}", excelFile.Path);
            Assert.Equal(sheet, excelFile.Sheet);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Path_GetAndSetCorrectly_WhenSet()
        {
            // Arrange
            string path = "test.xlsx";
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem("Test Excel File");

            // Act
            excelFile.Path = path;

            // Assert
            Assert.Equal($"local:{path}", excelFile.Path);
        }

        [Fact]
        public void Sheet_GetAndSetCorrectly_WhenSet()
        {
            // Arrange
            string sheet = "Sheet1";
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem("Test Excel File");

            // Act
            excelFile.Sheet = sheet;

            // Assert
            Assert.Equal(sheet, excelFile.Sheet);
        }

        [Fact]
        public void Constructor_AddTwoDataSources_WithTitleProvided()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new ExcelFileDataSourceItem("Test").SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.Excel, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, document.DataSources[0].Provider);
        }

        [Fact]
        public void Constructor_AddTwoDataSources_WithTitleAndPathProvided()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new ExcelFileDataSourceItem("Test", "Path").SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.Excel, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, document.DataSources[0].Provider);
        }

        [Fact]
        public void Constructor_AddTwoDataSources_WithTitleAndPathAndSheetProvided()
        {
            // Arrange
            var document = new RdashDocument("Test");
            var dataSourceItem = new ExcelFileDataSourceItem("Test", "Path", "Sheet").SetFields(new List<IField>() { new TextField("Test") });

            document.Visualizations.Add(new GridVisualization(dataSourceItem));

            // Ensure data sources are added to the data sources collection
            document.Validate();

            // Assert
            Assert.Equal(2, document.DataSources.Count);
            Assert.Equal(DataSourceIds.Excel, document.DataSources[0].Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, document.DataSources[0].Provider);
        }
    }
}
