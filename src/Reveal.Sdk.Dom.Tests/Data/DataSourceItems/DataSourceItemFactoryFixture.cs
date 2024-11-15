using Reveal.Sdk.Dom.Data;
using System;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data.DataSourceItems
{
    public class DataSourceItemFactoryFixture
    {
        [Fact]
        public void Create_ReturnDataSourceItem_WithDefaultDataSource()
        {
            // Arrange
            var factory = new DataSourceItemFactory();
            var type = DataSourceType.MicrosoftSqlServer;
            var id = "1";
            var title = "Test Title";

            // Act
            var result = factory.Create(type, id, title);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MicrosoftSqlServerDataSourceItem>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Null(result.Subtitle);
            Assert.NotNull(result.DataSource);
            Assert.IsType<MicrosoftSqlServerDataSource>(result.DataSource);
        }

        [Fact]
        public void Create_ReturnDataSourceItem_WithCustomDataSource()
        {
            // Arrange
            var factory = new DataSourceItemFactory();
            var type = DataSourceType.MicrosoftSqlServer;
            var id = "1";
            var title = "Test Title";
            var dataSource = new DataSource();

            // Act
            var result = factory.Create(type, id, title, dataSource);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MicrosoftSqlServerDataSourceItem>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Null(result.Subtitle);
            Assert.NotNull(result.DataSource);
            Assert.NotSame(result.DataSource, dataSource);
            Assert.IsType<MicrosoftSqlServerDataSource>(result.DataSource);
        }

        [Fact]
        public void Create_ReturnDataSourceItem_WithCustomSubtitle()
        {
            // Arrange
            var factory = new DataSourceItemFactory();
            var type = DataSourceType.MicrosoftSqlServer;
            var id = "1";
            var title = "Test Title";
            var subTitle = "Test Subtitle";

            // Act
            var result = factory.Create(type, id, title, subTitle);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MicrosoftSqlServerDataSourceItem>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(subTitle, result.Subtitle);
            Assert.NotNull(result.DataSource);
            Assert.IsType<MicrosoftSqlServerDataSource>(result.DataSource);
        }

        [Fact]
        public void Create_ReturnDataSourceItem_WithCustomDataSourceAndSubTitle()
        {
            // Arrange
            var factory = new DataSourceItemFactory();
            var type = DataSourceType.MicrosoftSqlServer;
            var id = "2";
            var title = "Test Title";
            var subtitle = "Test Subtitle";
            var dataSource = new DataSource() 
            { 
                Id = "CustomId",
                Title = "Custom Title",
                Subtitle = "Custom Subtitle",
            };

            // Act
            var result = factory.Create(type, id, title, subtitle, dataSource);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<MicrosoftSqlServerDataSourceItem>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
            Assert.Equal(subtitle, result.Subtitle);
            Assert.NotNull(result.DataSource);
            Assert.NotSame(dataSource, result.DataSource);
        }

        [Fact]
        public void Create_ThrowNotImplementedException_WhenUnknownType()
        {
            // Arrange
            var factory = new DataSourceItemFactory();
            var type = (DataSourceType)99;
            var id = "3";
            var title = "Test Title";
            var subtitle = "Test Subtitle";
            var dataSource = new DataSource();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => factory.Create(type, id, title));
            Assert.Throws<NotImplementedException>(() => factory.Create(type, id, title, dataSource));
            Assert.Throws<NotImplementedException>(() => factory.Create(type, id, title, subtitle));
            Assert.Throws<NotImplementedException>(() => factory.Create(type, id, title, subtitle, dataSource));
        }
    }
}
