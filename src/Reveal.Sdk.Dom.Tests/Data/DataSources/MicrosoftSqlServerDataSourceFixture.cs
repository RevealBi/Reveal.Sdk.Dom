using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftSqlServerDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToMicrosoftSqlServer_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftSqlServerDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftSqlServer, dataSource.Provider);
        }

        [Fact]
        public void Constructor_SetsServerAggregationDefaultToTrue_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftSqlServerDataSource();

            // Assert
            Assert.True(dataSource.Properties.GetValue<bool>("ServerAggregationDefault"));
        }

        [Fact]
        public void Constructor_SetsServerAggregationReadOnlyToFalse_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftSqlServerDataSource();

            // Assert
            Assert.False(dataSource.Properties.GetValue<bool>("ServerAggregationReadOnly"));
        }

        [Theory]
        [InlineData("TestDatabase")]
        [InlineData(null)]
        public void Database_SetsAndGetsValue_WithDifferentInputs(string database)
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            dataSource.Database = database;

            // Assert
            Assert.Equal(database, dataSource.Database);
        }

        [Theory]
        [InlineData("TestHost")]
        [InlineData(null)]
        public void Host_SetsAndGetsValue_WithDifferentInputs(string host)
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            dataSource.Host = host;

            // Assert
            Assert.Equal(host, dataSource.Host);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData(null)]
        public void Port_SetsAndGetsValue_WithDifferentInputs(string port)
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            dataSource.Port = port;

            // Assert
            Assert.Equal(port, dataSource.Port);
        }

        [Theory]
        [InlineData("TestSchema")]
        [InlineData(null)]
        public void Schema_SetsAndGetsValue_WithDifferentInputs(string schema)
        {
            // Arrange
            var dataSource = new MicrosoftSqlServerDataSource();

            // Act
            dataSource.Schema = schema;

            // Assert
            Assert.Equal(schema, dataSource.Schema);
        }

        [Fact]
        public void Constructor_EnsuresServerAggregationDefaultPropertyExists_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftSqlServerDataSource();

            // Assert
            Assert.True(dataSource.Properties.ContainsKey("ServerAggregationDefault"));
            Assert.True(dataSource.Properties.GetValue<bool>("ServerAggregationDefault"));
        }

        [Fact]
        public void Constructor_EnsuresServerAggregationReadOnlyPropertyExists_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftSqlServerDataSource();

            // Assert
            Assert.True(dataSource.Properties.ContainsKey("ServerAggregationReadOnly"));
            Assert.False(dataSource.Properties.GetValue<bool>("ServerAggregationReadOnly"));
        }
    }
}
