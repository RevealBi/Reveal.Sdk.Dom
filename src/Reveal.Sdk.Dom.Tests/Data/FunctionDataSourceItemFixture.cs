using Moq;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class FunctionDataSourceItemFixture
    {
        [Fact]
        public void Constructor_CreateFunctionDataSource_WithTitleAndDataSource()
        {
            // Arrange
            var title = "Title";
            var dataSource = new DataSource();

            // Act
            var mock = new Mock<FunctionDataSourceItem>(title, dataSource) { CallBase = true };
            var functionDataSourceItem = mock.Object;

            // Assert
            Assert.Equal(title, functionDataSourceItem.Title);
            Assert.Equal(dataSource, functionDataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, functionDataSourceItem.DataSourceId);
            Assert.Equal(title, functionDataSourceItem.DataSource.Title);
        }
    }
}
