using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonS3DataSourceFixture
    {
        [Fact]
        public void Constructor_Should_SetProviderToAmazonS3()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Assert
            Assert.Equal(DataSourceProvider.AmazonS3, dataSource.Provider);
        }

        [Fact]
        public void AccountId_Should_GetAndSetAccountIdProperty()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedAccountId = "123456789012";

            // Act
            dataSource.AccountId = expectedAccountId;
            var result = dataSource.AccountId;

            // Assert
            Assert.Equal(expectedAccountId, result);
        }

        [Fact]
        public void Region_Should_GetAndSetRegionProperty()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedRegion = "us-west-2";

            // Act
            dataSource.Region = expectedRegion;
            var result = dataSource.Region;

            // Assert
            Assert.Equal(expectedRegion, result);
        }

        [Fact]
        public void Properties_Should_StoreAllValuesCorrectly()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedAccountId = "123456789012";
            var expectedRegion = "us-west-2";

            // Act
            dataSource.AccountId = expectedAccountId;
            dataSource.Region = expectedRegion;

            // Assert
            Assert.Equal(expectedAccountId, dataSource.Properties.GetValue<string>("AccountId"));
            Assert.Equal(expectedRegion, dataSource.Properties.GetValue<string>("Region"));
        }

        [Fact]
        public void Properties_Should_UpdateValuesCorrectly()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var initialAccountId = "123456789012";
            var updatedAccountId = "098765432109";
            var initialRegion = "us-west-1";
            var updatedRegion = "us-east-1";

            // Assert
            dataSource.AccountId = initialAccountId;
            Assert.Equal(initialAccountId, dataSource.AccountId);

            dataSource.AccountId = updatedAccountId;
            Assert.Equal(updatedAccountId, dataSource.AccountId);
            
            dataSource.Region = initialRegion;
            Assert.Equal(initialRegion, dataSource.Region);

            dataSource.Region = updatedRegion;
            Assert.Equal(updatedRegion, dataSource.Region);
        }

        [Fact]
        public void AccountId_SetToNull_Should_HandleNullAssignment()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Act
            dataSource.AccountId = null;

            // Assert
            Assert.Null(dataSource.AccountId);
            Assert.Null(dataSource.Properties.GetValue<string>("AccountId"));
        }

        [Fact]
        public void Region_SetToNull_Should_HandleNullAssignment()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Act
            dataSource.Region = null;

            // Assert
            Assert.Null(dataSource.Region);
            Assert.Null(dataSource.Properties.GetValue<string>("Region"));
        }

        [Fact]
        public void Provider_Should_RemainAmazonS3_AfterPropertiesSet()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            dataSource.AccountId = "123456789012";
            dataSource.Region = "us-west-2";

            // Act
            var provider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.AmazonS3, provider);
        }
    }
}