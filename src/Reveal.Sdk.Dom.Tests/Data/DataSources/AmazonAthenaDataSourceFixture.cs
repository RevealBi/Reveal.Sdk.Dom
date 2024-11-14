using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonAthenaDataSourceFixture
    {
        [Fact]
        public void Constructor_Should_SetProviderToAmazonAthena()
        {
            // Arrange & Act
            var dataSource = new AmazonAthenaDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.AmazonAthena, dataSource.Provider);
        }

        [Fact]
        public void DataCatalog_Should_GetAndSetDataCatalogProperty()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedDataCatalog = "TestCatalog";

            // Act
            dataSource.DataCatalog = expectedDataCatalog;
            var result = dataSource.DataCatalog;

            // Assert
            Assert.Equal(expectedDataCatalog, result);
        }

        [Fact]
        public void OutputLocation_Should_GetAndSetOutputLocationProperty()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedOutputLocation = "s3://test-bucket/output/";

            // Act
            dataSource.OutputLocation = expectedOutputLocation;
            var result = dataSource.OutputLocation;

            // Assert
            Assert.Equal(expectedOutputLocation, result);
        }

        [Fact]
        public void Region_Should_GetAndSetRegionProperty()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedRegion = "us-west-2";

            // Act
            dataSource.Region = expectedRegion;
            var result = dataSource.Region;

            // Assert
            Assert.Equal(expectedRegion, result);
        }

        [Fact]
        public void Workgroup_Should_GetAndSetWorkgroupProperty()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedWorkgroup = "primary";

            // Act
            dataSource.Workgroup = expectedWorkgroup;
            var result = dataSource.Workgroup;

            // Assert
            Assert.Equal(expectedWorkgroup, result);
        }

        [Fact]
        public void Properties_Should_StoreAllValuesCorrectly()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedDataCatalog = "TestCatalog";
            var expectedOutputLocation = "s3://test-bucket/output/";
            var expectedRegion = "us-west-2";
            var expectedWorkgroup = "primary";

            // Act
            dataSource.DataCatalog = expectedDataCatalog;
            dataSource.OutputLocation = expectedOutputLocation;
            dataSource.Region = expectedRegion;
            dataSource.Workgroup = expectedWorkgroup;

            // Assert
            Assert.Equal(expectedDataCatalog, dataSource.Properties.GetValue<string>("DataCatalog"));
            Assert.Equal(expectedOutputLocation, dataSource.Properties.GetValue<string>("OutputLocation"));
            Assert.Equal(expectedRegion, dataSource.Properties.GetValue<string>("Region"));
            Assert.Equal(expectedWorkgroup, dataSource.Properties.GetValue<string>("Workgroup"));
        }

        [Fact]
        public void Properties_Should_UpdateValuesCorrectly()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            var initialDataCatalog = "InitialCatalog";
            var updatedDataCatalog = "UpdatedCatalog";
            var initialOutputLocation = "s3://initial-bucket/output/";
            var updatedOutputLocation = "s3://updated-bucket/output/";

            //Assert
            dataSource.DataCatalog = initialDataCatalog;
            Assert.Equal(initialDataCatalog, dataSource.DataCatalog);

            dataSource.DataCatalog = updatedDataCatalog;
            Assert.Equal(updatedDataCatalog, dataSource.DataCatalog);

            dataSource.OutputLocation = initialOutputLocation;
            Assert.Equal(initialOutputLocation, dataSource.OutputLocation);

            dataSource.OutputLocation = updatedOutputLocation;
            Assert.Equal(updatedOutputLocation, dataSource.OutputLocation);
        }
        
        [Fact]
        public void DataCatalog_SetToNull_Should_HandleNullAssignment()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.DataCatalog = null;

            // Assert
            Assert.Null(dataSource.DataCatalog);
            Assert.Null(dataSource.Properties.GetValue<string>("DataCatalog"));
        }

        [Fact]
        public void OutputLocation_SetToNull_Should_HandleNullAssignment()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.OutputLocation = null;

            // Assert
            Assert.Null(dataSource.OutputLocation);
            Assert.Null(dataSource.Properties.GetValue<string>("OutputLocation"));
        }

        [Fact]
        public void Provider_Should_RemainAmazonAthena_AfterPropertySet()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            dataSource.DataCatalog = "Catalog";
            dataSource.OutputLocation = "s3://test-bucket/output/";

            // Act
            var provider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.AmazonAthena, provider);
        }
    }
}