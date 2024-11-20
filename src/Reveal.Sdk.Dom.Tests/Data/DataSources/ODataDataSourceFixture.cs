using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class ODataDataSourceFixture
    {
        [Fact]
        public void Constructor_SetsProviderToOData_WhenConstructed()
        {
            // Act
            var dataSource = new ODataDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.OData, dataSource.Provider);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UsePreemptiveAuthentication_SetsAndGetsValue_WithDifferentInputs(bool usePreemptiveAuthentication)
        {
            // Arrange
            var dataSource = new ODataDataSource();

            // Act
            dataSource.UsePreemptiveAuthentication = usePreemptiveAuthentication;

            // Assert
            Assert.Equal(usePreemptiveAuthentication, dataSource.UsePreemptiveAuthentication);
            Assert.Equal(usePreemptiveAuthentication, dataSource.Properties.GetValue<bool>("UsePreemptiveAuthentication"));
        }
    }
}