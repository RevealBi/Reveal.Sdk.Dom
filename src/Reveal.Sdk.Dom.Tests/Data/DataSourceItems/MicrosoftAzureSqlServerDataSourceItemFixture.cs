using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftAzureSqlServerDataSourceItemFixture
    {
        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_SetsTitleAndDatasource_AsProvided(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new MicrosoftAzureSqlServerDataSource() { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Equal(dSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
        }
    }
}
