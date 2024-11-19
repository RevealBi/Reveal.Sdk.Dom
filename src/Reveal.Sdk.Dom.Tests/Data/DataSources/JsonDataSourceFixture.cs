using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class JsonDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToJSON_WhenConstructed()
        {
            // Act
            var dataSource = new JsonDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.JSON, dataSource.Provider);
            Assert.Equal(DataSourceIds.JSON, dataSource.Id);
        }
    }
}
