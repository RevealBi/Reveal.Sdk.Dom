using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSources
{
    public class MicrosoftAzureSqlServerDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToMicrosoftAzureSqlServer_WhenConstructed()
        {
            // Act
            var dataSource = new MicrosoftAzureSqlServerDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.MicrosoftAzureSqlServer, dataSource.Provider);
        }

        [Fact]
        public void TrustServerCertificate_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new MicrosoftAzureSqlServerDataSource();
            var trustServerCertificate = false;

            // Act
            dataSource.TrustServerCertificate = trustServerCertificate;

            // Assert
            Assert.Equal(trustServerCertificate, dataSource.TrustServerCertificate);
            Assert.Equal(trustServerCertificate, dataSource.Properties.GetValue<bool>("TrustServerCertificate"));
        }
    }
}
