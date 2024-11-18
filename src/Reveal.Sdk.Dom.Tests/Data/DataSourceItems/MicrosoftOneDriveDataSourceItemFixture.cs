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

        [Fact]
        public void Constructor_SetsTitleAndDatasource_AsProvided()
        {
            // Arrange
            var dataSource = new MicrosoftOneDriveDataSource();
            var title = "Test title";

            // Act
            var dataSourceItem = new MicrosoftOneDriveDataSourceItem(title, dataSource);

            // Assert
            Assert.Equal(title, dataSourceItem.Title);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
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
