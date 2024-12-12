using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class GoogleAnalytics4DataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDatasource_AsProvided()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var title = "Test title";

            // Act
            var dataSourceItem = new GoogleAnalytics4DataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
        }

        [Fact]
        public void AccountId_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var dataSourceItem = new GoogleAnalytics4DataSourceItem("Test", dataSource);
            var accountId = "TestAccountId";

            // Act
            dataSourceItem.AccountId = accountId;

            // Assert
            Assert.Equal(accountId, dataSourceItem.AccountId);
            Assert.Equal(accountId, dataSourceItem.Properties.GetValue<string>("ga4AccountId"));
        }

        [Fact]
        public void PropertyId_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var dataSourceItem = new GoogleAnalytics4DataSourceItem("Test", dataSource);
            var propertyId = "TestPropertyId";

            // Act
            dataSourceItem.PropertyId = propertyId;

            // Assert
            Assert.Equal(propertyId, dataSourceItem.PropertyId);
            Assert.Equal(propertyId, dataSourceItem.Properties.GetValue<string>("PropertyId"));
        }

        [Fact]
        public void Id_SameAsPropertyId_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var dataSourceItem = new GoogleAnalytics4DataSourceItem("Test", dataSource);
            var propertyId = "TestPropertyId";

            // Act
            dataSourceItem.PropertyId = propertyId;

            // Assert
            Assert.Equal(propertyId, dataSourceItem.Id);
        }

        [Fact]
        public void PropertyName_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var dataSourceItem = new GoogleAnalytics4DataSourceItem("Test", dataSource);
            var propertyName = "TestPropertyName";

            // Act
            dataSourceItem.PropertyName = propertyName;

            // Assert
            Assert.Equal(propertyName, dataSourceItem.PropertyName);
            Assert.Equal(propertyName, dataSourceItem.Properties.GetValue<string>("PropertyName"));
        }
    }
}
