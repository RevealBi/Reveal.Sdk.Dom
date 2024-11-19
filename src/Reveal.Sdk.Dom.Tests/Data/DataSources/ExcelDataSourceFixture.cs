using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class ExcelDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftExcel_WhenConstructed()
        {
            // Act
            var dataSource = new ExcelDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftExcel, dataSource.Provider);
            Assert.Equal(DataSourceIds.Excel, dataSource.Id);
        }
    }
}
