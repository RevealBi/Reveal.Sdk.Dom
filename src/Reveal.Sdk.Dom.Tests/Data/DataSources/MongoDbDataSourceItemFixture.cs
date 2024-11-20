using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MongoDbDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsProviderToMongoDB_WhenConstructed()
        {
            // Act
            var dataSource = new MongoDBDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MongoDB, dataSource.Provider);
        }

        [Theory]
        [InlineData("mongodb://localhost:27017")]
        [InlineData(null)]
        public void ConnectionString_SetsAndGetsValue_WithDifferentInputs(string connectionString)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ConnectionString = connectionString;

            // Assert
            Assert.Equal(connectionString, dataSource.ConnectionString);
            Assert.Equal(connectionString, dataSource.Properties.GetValue<string>("ConnectionString"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServerDefaultValue_SetsAndGetsValue_WithDifferentInputs(bool defaultValue)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ProcessDataOnServerDefaultValue = defaultValue;

            // Assert
            Assert.Equal(defaultValue, dataSource.ProcessDataOnServerDefaultValue);
            Assert.Equal(defaultValue, dataSource.Properties.GetValue<bool>("ServerAggregationDefault"));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessDataOnServerReadOnly_SetsAndGetsValue_WithDifferentInputs(bool readOnlyValue)
        {
            // Arrange
            var dataSource = new MongoDBDataSource();

            // Act
            dataSource.ProcessDataOnServerReadOnly = readOnlyValue;

            // Assert
            Assert.Equal(readOnlyValue, dataSource.ProcessDataOnServerReadOnly);
            Assert.Equal(readOnlyValue, dataSource.Properties.GetValue<bool>("ServerAggregationReadOnly"));
        }
    }
}