using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class AmazonRedshiftDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateRedshiftDSItem_WithTitle()
        {
            // Arrange
            var expectedTitle = "DS Item Title";

            // Act
            var dataSourceItem = new AmazonRedshiftDataSourceItem(expectedTitle);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dataSourceItem.SchemaTypeName);
            Assert.Equal(expectedTitle, dataSourceItem.Title);
            Assert.Equal(expectedTitle, dataSourceItem.DataSource.Title);
            Assert.NotNull(dataSourceItem.DataSource);
            Assert.IsType<AmazonRedshiftDataSource>(dataSourceItem.DataSource);
        }

        [Theory]
        [InlineData("DS Title", "DS Title", "DS Item Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateRedshiftDSItem_WithTitleAndDataSource(string dsTitle, string expectedDSTitle, string dsItemTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource() { Title = dsTitle };

            // Act
            var dataSourceItem = new AmazonRedshiftDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dataSourceItem.SchemaTypeName);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.NotSame(dataSource, dataSourceItem.DataSource);
            Assert.IsType<AmazonRedshiftDataSource>(dataSourceItem.DataSource);
        }

        [Theory]
        [InlineData("DS Title", "DS Title", "DS Item Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateRedshiftDSItem_WithTitleAndRedshiftDataSource(string dsTitle, string expectedDSTitle, string dsItemTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new AmazonRedshiftDataSource() { Title = dsTitle };

            // Act
            var dataSourceItem = new AmazonRedshiftDataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dataSourceItem.SchemaTypeName);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Same(dataSource, dataSourceItem.DataSource);
        }

        [Fact]
        public void ToJsonString_CreatesFormattedJson_NoConditions()
        {
            // Arrange
            var expectedJson = """
            {
              "_type": "DataSourceItemType",
              "Id": "redshiftDSItemId",
              "Title": "Redshift DSItem",
              "SubTitle": "Northwind Employees",
              "DataSourceId": "redshiftId",
              "HasTabularData": true,
              "HasAsset": false,
              "Properties": {
                "Table": "employees",
                "Schema": "public",
                "Database": "RedshiftDB"
              },
              "Parameters": {}
            }
            """;
            var dataSource = new AmazonRedshiftDataSource()
            {
                Id = "redshiftId",
            };
            var dataSourceItem = new AmazonRedshiftDataSourceItem("Redshift DSItem", dataSource)
            {
                Id = "redshiftDSItemId",
                Title = "Redshift DSItem",
                Subtitle = "Northwind Employees",
                Database = "RedshiftDB",
                Table = "employees",
                Schema = "public",
                HasTabularData = true,
                HasAsset = false,
            };
            var expectedJObject = JObject.Parse(expectedJson);

            // Act
            var json = dataSourceItem.ToJsonString();
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}