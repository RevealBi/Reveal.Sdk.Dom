using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class HttpAnalysisServicesDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsModeToHttp_WhenConstructed()
        {
            // Act
            var dataSource = new HttpAnalysisServicesDataSource();

            // Assert
            Assert.Equal("HTTP", dataSource.Properties.GetValue<string>("Mode"));
        }

        [Theory]
        [InlineData("https://example.com/analysis")]
        [InlineData(null)]
        public void Url_SetsAndGetsValue_WithDifferentInputs(string url)
        {
            // Arrange
            var dataSource = new HttpAnalysisServicesDataSource();

            // Act
            dataSource.Url = url;

            // Assert
            Assert.Equal(url, dataSource.Url);
            Assert.Equal(url, dataSource.Properties.GetValue<string>("Url"));
        }
    }
}