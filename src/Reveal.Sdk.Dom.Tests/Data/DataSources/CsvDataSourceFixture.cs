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
    public class CsvDataSourceFixture
    {
        [Fact]
        public void Constructor_SetProviderToCSV_WhenConstructed()
        {
            // Act
            var dataSource = new CsvDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.CSV, dataSource.Provider);
            Assert.Equal(DataSourceIds.CSV, dataSource.Id);
        }
    }
}
