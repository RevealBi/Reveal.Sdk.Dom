using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonS3DataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToAmazonS3_Always()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Act
            var actualProvider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.AmazonS3, actualProvider);
        }

        [Fact]
        public void AccountId_SetsAndGetsValue_ValidAccountId()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedAccountId = "123456789012";

            // Act
            dataSource.AccountId = expectedAccountId;
            var actualAccountId = dataSource.AccountId;

            // Assert
            Assert.Equal(expectedAccountId, actualAccountId);
        }

        [Fact]
        public void AccountId_SetsValue_NullAccountId()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Act
            dataSource.AccountId = null;
            var actualAccountId = dataSource.AccountId;
            var actualPropertyAccountId = dataSource.Properties.GetValue<string>("AccountId");

            // Assert
            Assert.Null(actualAccountId);
            Assert.Null(actualPropertyAccountId);
        }

        [Fact]
        public void Region_SetsAndGetsValue_ValidRegion()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedRegion = "us-west-2";

            // Act
            dataSource.Region = expectedRegion;
            var actualRegion = dataSource.Region;

            // Assert
            Assert.Equal(expectedRegion, actualRegion);
        }

        [Fact]
        public void Region_SetsValue_NullRegion()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();

            // Act
            dataSource.Region = null;
            var actualRegion = dataSource.Region;
            var actualPropertyRegion = dataSource.Properties.GetValue<string>("Region");

            // Assert
            Assert.Null(actualRegion);
            Assert.Null(actualPropertyRegion);
        }

        [Fact]
        public void Properties_StoresAccountIdValueCorrectly_ValidAccountId()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedAccountId = "123456789012";

            // Act
            dataSource.AccountId = expectedAccountId;
            var actualPropertyAccountId = dataSource.Properties.GetValue<string>("AccountId");

            // Assert
            Assert.Equal(expectedAccountId, actualPropertyAccountId);
        }

        [Fact]
        public void Properties_StoresRegionValueCorrectly_ValidRegion()
        {
            // Arrange
            var dataSource = new AmazonS3DataSource();
            var expectedRegion = "us-west-2";

            // Act
            dataSource.Region = expectedRegion;
            var actualPropertyRegion = dataSource.Properties.GetValue<string>("Region");

            // Assert
            Assert.Equal(expectedRegion, actualPropertyRegion);
        }
    }
}