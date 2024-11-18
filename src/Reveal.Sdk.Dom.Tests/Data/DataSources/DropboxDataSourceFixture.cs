using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class DropboxDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToDropbox_Always()
        {
            // Arrange
            var dataSource = new DropboxDataSource();

            // Act
            var actualProvider = dataSource.Provider;

            // Assert
            Assert.Equal(DataSourceProvider.Dropbox, actualProvider);
        }
    }
}