using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class BoxDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToBox_Always()
        {
            // Arrange
            var dataSource = new BoxDataSource();

            // Act
            var actualProvider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.Box, actualProvider);
        }
    }
}