using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class CsvDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToCSV_WhenCalled()
        {
            // Act
            var dataSource = new CsvDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.CSV, dataSource.Provider);
            Assert.Equal(DataSourceIds.CSV, dataSource.Id);
        }
    }
}
