﻿using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class ExcelFileDataSourceItemFixture
    {
        [Fact]
        public void Constructor_WithTitleAndDataSource_ShouldSetProperties()
        {
            // Arrange
            string title = "Test Excel File";
            DataSource dataSource = new DataSource();

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.Equal(dataSource, excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_WithTitle_ShouldSetProperties()
        {
            // Arrange
            string title = "Test Excel File";

            // Act
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem(title);

            // Assert
            Assert.Equal(title, excelFile.Title);
            Assert.Equal(title, excelFile.ResourceItem.Title);
            Assert.NotNull(excelFile.DataSource);
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);            
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_WithTitleAndPath_ShouldSetProperties()
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
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal($"local:{path}", excelFile.Path);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);            
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_WithTitlePathAndSheet_ShouldSetProperties()
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
            Assert.Equal(DataSourceIds.Excel, excelFile.DataSource.Id);
            Assert.Equal(DataSourceProvider.MicrosoftExcel, excelFile.DataSource.Provider);
            Assert.Equal($"local:{path}", excelFile.Path);
            Assert.Equal(sheet, excelFile.Sheet);
            Assert.Equal(DataSourceProvider.LocalFile, excelFile.ResourceItemDataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItemDataSource.Id);
            Assert.Equal(DataSourceIds.LOCALFILE, excelFile.ResourceItem.DataSourceId);
        }

        [Fact]
        public void Path_GetAndSet_ShouldSetLocalPath()
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
        public void Sheet_GetAndSet_ShouldSetSheet()
        {
            // Arrange
            string sheet = "Sheet1";
            ExcelFileDataSourceItem excelFile = new ExcelFileDataSourceItem("Test Excel File");

            // Act
            excelFile.Sheet = sheet;

            // Assert
            Assert.Equal(sheet, excelFile.Sheet);
        }
    }
}
