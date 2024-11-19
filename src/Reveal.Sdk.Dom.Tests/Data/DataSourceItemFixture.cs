using NuGet.Frameworks;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Data
{
    public class DataSourceItemFixture
    {
        [Fact]
        public void Constructor_GeneratesNotNullObject_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.NotNull(dataSourceItem);
        }

        [Fact]
        public void Constructor_CreatesDefaultSchemaTypeName_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.Equal(SchemaTypeNames.DataSourceItemType, dataSourceItem.SchemaTypeName);
        }

        [Fact]
        public void Constructor_GeneratesUniqueNotEmptyId_WithoutArguments()
        {
            // Arrange

            // Act
            var dataSourceItem1 = new DataSourceItem();
            var dataSourceItem2 = new DataSourceItem();

            // Assert
            Assert.NotEmpty(dataSourceItem1.Id);
            Assert.NotEmpty(dataSourceItem2.Id);
            Assert.NotEqual(dataSourceItem1.Id, dataSourceItem2.Id);
        }

        [Fact]
        public void Constructor_GeneratesUniqueNotEmptyId_WithDataSourceAndTitle()
        {
            // Arrange
            var dataSource1 = new DataSource();
            var dataSource2 = new DataSource();

            // Act
            var dataSourceItem1 = new DataSourceItem("Title 1", dataSource1);
            var dataSourceItem2 = new DataSourceItem("Title 2", dataSource2);

            // Assert
            Assert.NotEmpty(dataSourceItem1.Id);
            Assert.NotEmpty(dataSourceItem2.Id);
            Assert.NotEqual(dataSourceItem1.Id, dataSourceItem2.Id);
        }

        [Theory]
        [InlineData("DS Title", "DS Title")]
        [InlineData(null, "DS Item Title")]
        public void Constructor_CreatesDataSourceItem_WithDataSourceAndTitle(string dsTitle, string expectedDsTitle)
        {
            // Arrange
            var dsItemTitle = "DS Item Title";
            var dataSource = new DataSource { Title = dsTitle };

            // Act
            var dataSourceItem = new DataSourceItem(dsItemTitle, dataSource);

            // Assert
            Assert.NotNull(dataSourceItem);
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
            Assert.Equal(expectedDsTitle, dataSourceItem.DataSource.Title);
            Assert.Equal(dsItemTitle, dataSourceItem.Title);
        }

        [Fact]
        public void SetId_GeneratesUniqueNotEmptyId_WithNullValue()
        {
            // Arrange
            var dataSourceItem1 = new DataSourceItem();
            var dataSourceItem2 = new DataSourceItem();

            // Act
            dataSourceItem1.Id = null;
            dataSourceItem2.Id = null;

            // Assert
            Assert.NotEmpty(dataSourceItem1.Id);
            Assert.NotEmpty(dataSourceItem2.Id);
            Assert.NotEqual(dataSourceItem1.Id, dataSourceItem2.Id);
        }

        [Fact]
        public void SetId_UpdatesResourceId_WhenResourceNotNull()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            dataSourceItem.ResourceItem = new DataSourceItem();
            var newId = "updated-id";

            // Act
            dataSourceItem.Id = newId;

            // Assert
            Assert.Equal(dataSourceItem.Id, dataSourceItem.ResourceItem.Id);
        }

        [Fact]
        public void GetId_ReturnsSameValue_AfterSetId()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var newId = "updated-id";

            // Act
            dataSourceItem.Id = newId;

            // Assert
            Assert.Equal(newId, dataSourceItem.Id);
        }

        [Fact]
        public void GetSubtitle_ReturnsSameValue_AfterSetSubtitle()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var subTitle = "subtitle";

            // Act
            dataSourceItem.Subtitle = subTitle;

            // Assert
            Assert.Equal(subTitle, dataSourceItem.Subtitle);
        }

        [Fact]
        public void SetSubtitle_UpdateResourceItemSubTitle_ResourceItemNotnull()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            dataSourceItem.ResourceItem = new DataSourceItem();
            var subTitle = "subtitle";

            // Act
            dataSourceItem.Subtitle = subTitle;

            // Assert
            Assert.Equal(subTitle, dataSourceItem.ResourceItem.Subtitle);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForHasTabularData()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.True(dataSourceItem.HasTabularData);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForHasAsset()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.False(dataSourceItem.HasAsset);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForProperties()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.NotNull(dataSourceItem.Properties);
            Assert.Empty(dataSourceItem.Properties);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForParameters()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.NotNull(dataSourceItem.Parameters);
            Assert.Empty(dataSourceItem.Parameters);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForFields()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.NotNull(dataSourceItem.Fields);
            Assert.Empty(dataSourceItem.Fields);
        }

        [Fact]
        public void GetFields_ReturnSameValue_AfterSetFields()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var fields = new List<IField> { new TextField() };

            // Act
            dataSourceItem.Fields = fields;

            // Assert
            Assert.Equal(fields, dataSourceItem.Fields);
        }

        [Fact]
        public void GetDataSource_ReturnSameValue_AfterSetDataSource()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var dataSource = new DataSource();

            // Act
            dataSourceItem.DataSource = dataSource;

            // Assert
            Assert.Equal(dataSource, dataSourceItem.DataSource);
            Assert.Equal(dataSource.Id, dataSourceItem.DataSourceId);
        }

        [Fact]
        public void Constructor_SetDefaultValue_ForJoinTables()
        {
            // Arrange

            // Act
            var dataSourceItem = new DataSourceItem();

            // Assert
            Assert.NotNull(dataSourceItem.JoinTables);
            Assert.Empty(dataSourceItem.JoinTables);
        }

        [Fact]
        public void Join_AddsJoinTable_UsingJoinConditions()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var joinConditions = new List<JoinCondition> { new JoinCondition("left", "right") };
            var dataSourceItemToJoin = new DataSourceItem();

            // Act
            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);

            // Assert
            Assert.Single(dataSourceItem.JoinTables);
            Assert.Equal("Alias", dataSourceItem.JoinTables[0].Alias);
            Assert.Equal(dataSourceItemToJoin, dataSourceItem.JoinTables[0].DataDefinition.DataSourceItem);
            Assert.Single(dataSourceItem.JoinTables[0].JoinConditions);
            Assert.Equal("[left]", dataSourceItem.JoinTables[0].JoinConditions[0].LeftFieldName);
            Assert.Equal("Alias.[right]", dataSourceItem.JoinTables[0].JoinConditions[0].RightFieldName);
        }

        [Fact]
        public void Join_AddsJoinTable_UsingLeftAndRightFieldNames()
        {
            // Arrange
            var leftJoinFieldName = "left";
            var rightJoinFieldName = "right";
            var aliasName = "Alias";
            var dataSourceItem = new DataSourceItem();
            var joinConditions = new List<JoinCondition> { new JoinCondition(leftJoinFieldName, rightJoinFieldName) };
            var dataSourceItemToJoin = new DataSourceItem();

            // Act
            dataSourceItem.Join(aliasName, leftJoinFieldName, rightJoinFieldName, dataSourceItemToJoin);

            // Assert
            Assert.Single(dataSourceItem.JoinTables);
            Assert.Equal(aliasName, dataSourceItem.JoinTables[0].Alias);
            Assert.Equal(dataSourceItemToJoin, dataSourceItem.JoinTables[0].DataDefinition.DataSourceItem);
            Assert.Single(dataSourceItem.JoinTables[0].JoinConditions);
            Assert.Equal($"[{leftJoinFieldName}]", dataSourceItem.JoinTables[0].JoinConditions[0].LeftFieldName);
            Assert.Equal($"{aliasName}.[{rightJoinFieldName}]", dataSourceItem.JoinTables[0].JoinConditions[0].RightFieldName);
        }

        [Theory]
        [InlineData("FieldName", "[FieldName]")]
        [InlineData("[FieldName]", "[FieldName]")]
        [InlineData("A.FieldName", "A.[FieldName]")]
        [InlineData("A.[FieldName]", "A.[FieldName]")]
        public void ValidateLeftJoinFieldName_ReturnsFieldName_WithLeftFieldName(string fieldName, string expected)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var joinConditions = new List<JoinCondition> { new JoinCondition(fieldName, "") };
            var dataSourceItemToJoin = new DataSourceItem();

            // Act
            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);

            // Assert
            Assert.Equal(expected, dataSourceItem.JoinTables[0].JoinConditions[0].LeftFieldName);
        }

        [Theory]
        [InlineData("FieldName")]
        [InlineData("[FieldName]")]
        [InlineData("Alias.FieldName")]
        [InlineData("Alias.[FieldName]")]
        [InlineData("[Alias.FieldName]")]
        public void ValidateRightFieldName_ReturnsFieldName_WithValidRightFieldName(string fieldName)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var joinConditions = new List<JoinCondition> { new JoinCondition("", fieldName) };
            var dataSourceItemToJoin = new DataSourceItem();

            // Act
            dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);

            // Assert
            Assert.Equal("Alias.[FieldName]", dataSourceItem.JoinTables[0].JoinConditions[0].RightFieldName);
        }

        [Theory]
        [InlineData("FieldName.Alias")]
        [InlineData("[FieldName.Alias]")]
        [InlineData("Alias.FieldName.FieldName1")]
        public void ValidateRightFieldName_ThrowsException_WithInvalidRightFieldName(string fieldName)
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var joinConditions = new List<JoinCondition> { new JoinCondition("", fieldName) };
            var dataSourceItemToJoin = new DataSourceItem();

            // Act
            Action act = () => dataSourceItem.Join("Alias", joinConditions, dataSourceItemToJoin);
            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            // Assert
            Assert.Equal($"Invalid right field name format: {fieldName}", exception.Message);
        }

        [Theory]
        [InlineData(typeof(AmazonAthenaDataSourceItem), false)]
        [InlineData(typeof(AmazonRedshiftDataSourceItem), false)]
        [InlineData(typeof(AmazonS3DataSourceItem), false)]
        [InlineData(typeof(BoxDataSourceItem), false)]
        [InlineData(typeof(DropboxDataSourceItem), false)]
        [InlineData(typeof(ExcelFileDataSourceItem), false)]
        [InlineData(typeof(GoogleAnalytics4DataSourceItem), false)]
        [InlineData(typeof(GoogleBigQueryDataSourceItem), false)]
        [InlineData(typeof(GoogleDriveDataSourceItem), false)]
        [InlineData(typeof(GoogleSheetsDataSourceItem), false)]
        [InlineData(typeof(MicrosoftAnalysisServicesDataSourceItem), true)]
        [InlineData(typeof(MicrosoftAzureAnalysisServicesDataSourceItem), true)]
        [InlineData(typeof(MicrosoftAzureSqlServerDataSourceItem), false)]
        [InlineData(typeof(MicrosoftAzureSynapseAnalyticsDataSourceItem), false)]
        [InlineData(typeof(MicrosoftOneDriveDataSourceItem), false)]
        [InlineData(typeof(MicrosoftSharePointDataSourceItem), false)]
        [InlineData(typeof(MicrosoftSqlServerDataSourceItem), false)]
        [InlineData(typeof(MongoDbDataSourceItem), false)]
        [InlineData(typeof(MySqlDataSourceItem), false)]
        [InlineData(typeof(ODataDataSourceItem), false)]
        [InlineData(typeof(OracleDataSourceItem), false)]
        [InlineData(typeof(PostgreSqlDataSourceItem), false)]
        [InlineData(typeof(RestDataSourceItem), false)]
        [InlineData(typeof(SnowflakeDataSourceItem), false)]
        [InlineData(typeof(WebServiceDataSourceItem), false)]
        public void IsXmla_ReturnsExpectedBoolean_ForGivenDataSourceItemType(Type dataSourceItemType, bool expectedIsXmla)
        {
            // Arrange
            var constructor = dataSourceItemType.GetConstructor(new[] { typeof(string), typeof(DataSource) });
            var dataSourceItem = (DataSourceItem)constructor.Invoke(new object[] { null, null });

            // Act
            var isXmla = dataSourceItem.IsXmla;

            // Assert
            Assert.Equal(expectedIsXmla, isXmla);
        }
    }
}
