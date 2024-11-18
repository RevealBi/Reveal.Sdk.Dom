using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAzureSynapseAnalyticsDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureSqlServer_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftAzureSynapseAnalyticsDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAzureSynapseAnalytics, dataSource.Provider);
        }
    }
}
