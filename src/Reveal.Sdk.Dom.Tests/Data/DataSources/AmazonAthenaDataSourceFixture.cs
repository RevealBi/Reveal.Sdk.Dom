using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class AmazonAthenaDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToAmazonAthena_Always()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            var actualProvider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.AmazonAthena, actualProvider);
        }

        [Fact]
        public void DataCatalog_SetsAndGetsValue_ValidDataCatalog()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedDataCatalog = "TestCatalog";

            // Act
            dataSource.DataCatalog = expectedDataCatalog;
            var actualDataCatalog = dataSource.DataCatalog;

            // Assert
            Assert.Equal(expectedDataCatalog, actualDataCatalog);
        }

        [Fact]
        public void DataCatalog_SetsValue_NullDataCatalog()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.DataCatalog = null;
            var actualDataCatalog = dataSource.DataCatalog;
            var actualPropertyDataCatalog = dataSource.Properties.GetValue<string>("DataCatalog");

            // Assert
            Assert.Null(actualDataCatalog);
            Assert.Null(actualPropertyDataCatalog);
        }

        [Fact]
        public void OutputLocation_SetsAndGetsValue_ValidOutputLocation()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedOutputLocation = "s3://test-bucket/output/";

            // Act
            dataSource.OutputLocation = expectedOutputLocation;
            var actualOutputLocation = dataSource.OutputLocation;

            // Assert
            Assert.Equal(expectedOutputLocation, actualOutputLocation);
        }

        [Fact]
        public void OutputLocation_SetsValue_NullOutputLocation()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.OutputLocation = null;
            var actualOutputLocation = dataSource.OutputLocation;
            var actualPropertyOutputLocation = dataSource.Properties.GetValue<string>("OutputLocation");

            // Assert
            Assert.Null(actualOutputLocation);
            Assert.Null(actualPropertyOutputLocation);
        }

        [Fact]
        public void Region_SetsAndGetsValue_ValidRegion()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
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
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.Region = null;
            var actualRegion = dataSource.Region;
            var actualPropertyRegion = dataSource.Properties.GetValue<string>("Region");

            // Assert
            Assert.Null(actualRegion);
            Assert.Null(actualPropertyRegion);
        }

        [Fact]
        public void Workgroup_SetsAndGetsValue_ValidWorkgroup()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedWorkgroup = "primary";

            // Act
            dataSource.Workgroup = expectedWorkgroup;
            var actualWorkgroup = dataSource.Workgroup;

            // Assert
            Assert.Equal(expectedWorkgroup, actualWorkgroup);
        }

        [Fact]
        public void Workgroup_SetsValue_NullWorkgroup()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();

            // Act
            dataSource.Workgroup = null;
            var actualWorkgroup = dataSource.Workgroup;
            var actualPropertyWorkgroup = dataSource.Properties.GetValue<string>("Workgroup");

            // Assert
            Assert.Null(actualWorkgroup);
            Assert.Null(actualPropertyWorkgroup);
        }

        [Fact]
        public void Properties_StoresDataCatalogValueCorrectly_ValidDataCatalog()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedDataCatalog = "TestCatalog";

            // Act
            dataSource.DataCatalog = expectedDataCatalog;
            var actualPropertyDataCatalog = dataSource.Properties.GetValue<string>("DataCatalog");

            // Assert
            Assert.Equal(expectedDataCatalog, actualPropertyDataCatalog);
        }

        [Fact]
        public void Properties_StoresOutputLocationValueCorrectly_ValidOutputLocation()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedOutputLocation = "s3://test-bucket/output/";

            // Act
            dataSource.OutputLocation = expectedOutputLocation;
            var actualPropertyOutputLocation = dataSource.Properties.GetValue<string>("OutputLocation");

            // Assert
            Assert.Equal(expectedOutputLocation, actualPropertyOutputLocation);
        }

        [Fact]
        public void Properties_StoresRegionValueCorrectly_ValidRegion()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedRegion = "us-west-2";

            // Act
            dataSource.Region = expectedRegion;
            var actualPropertyRegion = dataSource.Properties.GetValue<string>("Region");

            // Assert
            Assert.Equal(expectedRegion, actualPropertyRegion);
        }

        [Fact]
        public void Properties_StoresWorkgroupValueCorrectly_ValidWorkgroup()
        {
            // Arrange
            var dataSource = new AmazonAthenaDataSource();
            var expectedWorkgroup = "primary";

            // Act
            dataSource.Workgroup = expectedWorkgroup;
            var actualPropertyWorkgroup = dataSource.Properties.GetValue<string>("Workgroup");

            // Assert
            Assert.Equal(expectedWorkgroup, actualPropertyWorkgroup);
        }
    }
}