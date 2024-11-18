using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftSharePointDataSourceItemFixture
    {
        [Fact]
        public void Constructor_SetsTitleAndDatasource_AsProvided()
        {
            // Arrange
            var dataSource = new MicrosoftSharePointDataSource();
            var title = "Test title";

            // Act 
            var dataSourceItem = new MicrosoftSharePointDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
        }
    }
}
