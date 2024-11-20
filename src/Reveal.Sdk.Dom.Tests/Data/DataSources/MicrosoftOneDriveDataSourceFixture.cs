using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftOneDriveDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureSqlServer_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftOneDriveDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftOneDrive, dataSource.Provider);
        }
    }
}
