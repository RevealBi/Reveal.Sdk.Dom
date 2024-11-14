using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class LocalFileDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToGoogleAnalytics4_WhenConstructed()
        {
            // Act
            var dataSource = new LocalFileDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.LocalFile, dataSource.Provider);
            Assert.Equal(DataSourceIds.LOCALFILE, dataSource.Id);
        }
    }
}
