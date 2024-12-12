using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class GoogleAnalytics4DataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToGoogleAnalytics4_WhenConstructed()
        {
            // Act
            var dataSource = new GoogleAnalytics4DataSource();

            // Assert
            Assert.Equal(DataSourceProvider.GoogleAnalytics4, dataSource.Provider);
        }

        [Fact]
        public void Constructor_SetIdNonCanonicalUid_WhenConstructed()
        {
            // Act
            var dataSource = new GoogleAnalytics4DataSource();

            // Assert
            Assert.Equal(DataSourceProvider.GoogleAnalytics4, dataSource.Provider);
            Assert.Equal("provider=GOOGLE_ANALYTICS_4&properties=", dataSource.Id);
        }

        [Fact]
        public void PropertyId_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var propertyId = "TestPropertyId";

            // Act
            dataSource.PropertyId = propertyId;

            // Assert
            Assert.Equal(propertyId, dataSource.PropertyId);
            Assert.Equal(propertyId, dataSource.Properties.GetValue<string>("PropertyId"));
        }

        [Fact]
        public void AccountId_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var accountId = "TestAccountId";

            // Act
            dataSource.AccountId = accountId;

            // Assert
            Assert.Equal(accountId, dataSource.AccountId);
            Assert.Equal(accountId, dataSource.Properties.GetValue<string>("ga4AccountId"));
        }

        [Fact]
        public void PropertyName_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new GoogleAnalytics4DataSource();
            var propertyName = "TestPropertyName";

            // Act
            dataSource.PropertyName = propertyName;

            // Assert
            Assert.Equal(propertyName, dataSource.PropertyName);
            Assert.Equal(propertyName, dataSource.Properties.GetValue<string>("PropertyName"));
        }
    }
}
