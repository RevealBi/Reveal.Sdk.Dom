using Reveal.Sdk.Dom.Core.Constants;
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
    public class GoogleSheetsDataSourceFixture
    {

        [Fact]
        public void Constructor_SetProviderToGoogleSheets_WhenConstructed()
        {
            // Act
            var dataSource = new GoogleSheetsDataSource();

            // Assert
            Assert.Equal(DataSourceProvider.GoogleSheets, dataSource.Provider);
        }

        [Fact]
        public void Constructor_SetIdToGSheet_WhenConstructed()
        {
            // Act
            var dataSource = new GoogleSheetsDataSource();

            // Assert
            Assert.Equal(DataSourceIds.GSHEET, dataSource.Id);
        }
    }
}
