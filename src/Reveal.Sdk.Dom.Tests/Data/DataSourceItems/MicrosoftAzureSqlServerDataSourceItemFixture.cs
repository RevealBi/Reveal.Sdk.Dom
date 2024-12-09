using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftAzureSqlServerDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateDSItemWithExpectedFields_WithTitleAndMSAzureSqlServerDataSource(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new MicrosoftAzureSqlServerDataSource() { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Same(dataSource, dataSourceItem.DataSource);
        }

        [Fact]
        public void Constructor_CreateDSItemWithExpectedFields_WithTitle()
        {
            // Arrange
            var expectedDSItemTitle = "Azure SQL DS Item title";

            // Act
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem(expectedDSItemTitle);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.NotNull(dataSourceItem.DataSource);
            Assert.IsType<MicrosoftAzureSqlServerDataSource>(dataSourceItem.DataSource);
            Assert.Equal(expectedDSItemTitle, dataSourceItem.DataSource.Title);
        }

        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_CreateDSItemWithExpectedFields_WithTitleAndDataSource(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new DataSource () { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSource.Id);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.NotSame(dataSource, dataSourceItem.DataSource);
            Assert.IsType<MicrosoftAzureSqlServerDataSource>(dataSourceItem.DataSource);
        }

        [Fact]
        public void ToJsonString_CreateExpectedRevealJson_NoConditions()
        {
            // Arrange
            var expectedJson = @"{
              ""_type"": ""DataSourceItemType"",
              ""Id"": ""azureSqlDsItemId"",
              ""Title"": ""Azure SQL DSItem"",
              ""Subtitle"": ""Azure SQL DS Item SubTitle"",
              ""DataSourceId"": ""azureSqlId"",
              ""HasTabularData"": true,
              ""HasAsset"": false,
              ""Properties"": {
                ""ServerAggregation"": true,
                ""Database"": ""reveal"",
                ""Table"": ""Categories""
              },
              ""Parameters"": {}
            }";
            var dataSource = new MicrosoftAzureSqlServerDataSource()
            {
                Id = "azureSqlId"
            };
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem("Azure SQL DSItem", dataSource)
            {
                Id = "azureSqlDsItemId",
                Subtitle = "Azure SQL DS Item SubTitle",
                HasTabularData = true,
                HasAsset = false,
                ProcessDataOnServer = true,
                Database = "reveal",
                Table = "Categories"
                //DefaultRefreshRate = "161" // TODO: Check the DefaultRefreshRate when get Json, after DefaultRefreshRate is moved to Settings field
            };

            // Act
            var json = dataSourceItem.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(json);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
