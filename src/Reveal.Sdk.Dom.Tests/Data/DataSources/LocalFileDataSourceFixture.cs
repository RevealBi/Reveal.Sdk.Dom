using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class LocalFileDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToLocalFile_WhenConstructed()
        {
            // Act
            var dataSource = new LocalFileDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.LocalFile, dataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, dataSource.Id);
        }
    }
}
