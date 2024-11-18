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
        [Fact]
        public void Constructor_SetsTitleAndDatasource_AsProvided()
        {
            // Arrange
            var dataSource = new MicrosoftAzureSqlServerDataSource();
            var title = "Test title";

            // Act
            var dataSourceItem = new MicrosoftAzureSqlServerDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
        }
    }
}
