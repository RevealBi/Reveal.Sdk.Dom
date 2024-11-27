using System;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MySqlDataSourceItemFixture
    {
        [Theory]
        [InlineData("Test Item")]
        [InlineData(null)]
        public void Constructor_SetsTitleAndDataSource_WhenCalled(string title)
        {
            // Arrange
            var dataSource = new MySQLDataSource();

            // Act
            var item = new MySqlDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, item.Title);
            Assert.Equal(dataSource, item.DataSource);
        }


        [Fact]
        public void ProcessDataOnServer_SetsAndGetsValue_WithInputs()
        {
            // Arrange
            var item = new MySqlDataSourceItem("Test Item", new MySQLDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Fact]
        public void ProcessDataOnServer_SetsAndGetsValue_WhenCalled()
        {
            // Arrange
            var item = new MySqlDataSourceItem("Test Item", new MySQLDataSource());

            // Act
            item.ProcessDataOnServer = true;

            // Assert
            Assert.True(item.ProcessDataOnServer);
            Assert.True(item.Properties.GetValue<bool>("ServerAggregation"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServer_TogglesValueCorrectly_WhenCalled(bool initialValue)
        {
            // Arrange
            var item = new MySqlDataSourceItem("Toggle Test", new MySQLDataSource());

            // Act
            item.ProcessDataOnServer = initialValue;

            // Assert
            Assert.Equal(initialValue, item.ProcessDataOnServer);
        }

        [Fact]
        public void Title_UpdatesCorrectly_WhenSet()
        {
            // Arrange
            var item = new MySqlDataSourceItem("Initial Title", new MySQLDataSource());

            // Act
            item.Title = "Updated Title";

            // Assert
            Assert.Equal("Updated Title", item.Title);
        }

        [Fact]
        public void DataSource_RemainsUnchanged_WhenAccessed()
        {
            // Arrange
            var dataSource = new MySQLDataSource();
            var item = new MySqlDataSourceItem("Item", dataSource);

            // Act & Assert
            Assert.Equal(dataSource, item.DataSource);
        }
    }
}