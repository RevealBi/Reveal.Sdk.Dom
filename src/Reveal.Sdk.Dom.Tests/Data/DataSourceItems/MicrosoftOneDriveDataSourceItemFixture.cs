using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class MicrosoftOneDriveDataSourceItemFixture
    {

        [Theory]
        [InlineData("DS Title", "DS Item Title", "DS Title", "DS Item Title")]
        [InlineData(null, "DS Item Title", "DS Item Title", "DS Item Title")]
        public void Constructor_SetsTitleAndDatasource_AsProvided(string dSTitle, string dSItemTitle, string expectedDSTitle, string expectedDSItemTitle)
        {
            // Arrange
            var dataSource = new MicrosoftOneDriveDataSource() { Title = dSTitle };

            // Act
            var dataSourceItem = new MicrosoftOneDriveDataSourceItem(dSItemTitle, dataSource);

            // Assert
            Assert.Equal(expectedDSItemTitle, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDSTitle, dataSourceItem.DataSource.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
        }

        [Fact]
        public void Identifier_SaveValueAndProperties_WhenSet()
        {
            // Arrange
            var dataSource = new MicrosoftOneDriveDataSource();
            var dataSourceItem = new MicrosoftOneDriveDataSourceItem("Test", dataSource);
            var identifier = "Identifier";

            // Act
            dataSourceItem.Identifier = identifier;

            // Assert
            Assert.Equal(identifier, dataSourceItem.Identifier);
            Assert.Equal(identifier, dataSourceItem.Properties.GetValue<string>("Identifier"));
        }
    }
}
