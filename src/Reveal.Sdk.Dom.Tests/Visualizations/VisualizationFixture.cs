using Xunit;
using Moq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Xunit.Sdk;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Utilities;
using System;
using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Tests.Visualizations
{
    public class VisualizationFixture
    {
        [Fact]
        public void Constructor_SetTitleAndChartType_WhenConstructedWithoutDatasource()
        {
            // Arrange
            var testTitle = "testTitle";

            // Act
            var visualization = new Mock<Visualization>(testTitle, null) { CallBase = true }.Object;

            // Assert
            Assert.Equal(testTitle, visualization.Title);
            Assert.Equal(ChartType.Unknown, visualization.ChartType);
        }


        [Fact]
        public void Constructor_UseTabularDataDefinition_WhenUseTabularDataSource()
        {
            // Arrange
            var dataSource = new DataSource();
            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = true,
                Fields = new List<IField>
                {
                    new TextField("A"),
                    new TextField("B"),
                    new TextField("C"),
                    new TextField("D"),
                }
            };

            // Mock join tables
            var leftJoinFieldName = "left";
            var rightJoinFieldName = "right";
            var aliasName = "Alias";
            var joinConditions = new List<JoinCondition> { new JoinCondition(leftJoinFieldName, rightJoinFieldName) };
            var dataSourceItemToJoin = new DataSourceItem();

            dataSourceItem.Join(aliasName, leftJoinFieldName, rightJoinFieldName, dataSourceItemToJoin);

            // Act
            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            // Assert
            Assert.IsType<TabularDataDefinition>(visualization.DataDefinition);
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.Equal(dataSourceItem.Fields.Count, (visualization.DataDefinition as TabularDataDefinition).Fields.Count);
            Assert.Equal(dataSourceItem.JoinTables.Count, (visualization.DataDefinition as TabularDataDefinition).JoinTables.Count);
        }

        [Fact]
        public void Constructor_UseXmlaDataDefinition_WhenNotUseTabularDataSource()
        {
            // Arrange
            var dataSource = new DataSource();
            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = false,
            };

            // Act
            var visualization = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true }.Object;

            // Assert
            Assert.IsType<XmlaDataDefinition>(visualization.DataDefinition);
        }

        [Fact]
        public void Id_IsUnique_WhenConstructed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();

            // Act
            var visualization = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true }.Object;
            var visualization2 = new Mock<Visualization>("testTitle2", dataSourceItem) { CallBase = true }.Object;

            // Assert
            Assert.NotEmpty(visualization.Id);
            Assert.NotEmpty(visualization2.Id);
            Assert.NotEqual(visualization.Id, visualization2.Id);
        }

        [Fact]
        public void FilterBindings_EqualDataDefinitionBindings_WhenUsed()
        {
            // Arrange
            var dataSourceItem = new DataSourceItem();
            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;
            var testBindings = new DataSpecBindings();
            ((DataDefinitionBase)visualization.DataDefinition).Bindings = testBindings;

            // Act
            var filterBindings = visualization.FilterBindings;

            // Assert
            Assert.Equal(testBindings.Bindings, filterBindings);
        }

        [Fact]
        public void GetDataSourceItem_ReturnsCorrectDataSourceItem_Always()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };
            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                Fields = new List<IField>
                {
                    new TextField("A"),
                    new TextField("B"),
                    new TextField("C"),
                    new TextField("D"),
                }
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            var document = new RdashDocument();

            document.Visualizations.Add(visualization);

            RdashDocumentValidator.Validate(document);

            // Act
            var dataSourceItemResult = visualization.GetDataSourceItem();

            // Assert
            Assert.Equal(dataSourceItem, dataSourceItemResult);
            Assert.Equal(dataSourceItemResult.DataSource.Id, dataSource.Id);
        }

        [Fact]
        public void GetDataSourceItem_ReturnsCorrectResourceItemDataSource_WhenSetResourceItem()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };
            var dataSourceResourceItem = new DataSource()
            {
                Id = "dataSourceResourceItemId",
            };
            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                Fields = new List<IField>
                {
                    new TextField("A"),
                    new TextField("B"),
                    new TextField("C"),
                    new TextField("D"),
                }
            };
            dataSourceItem.ResourceItem = new DataSourceItem()
            {
                DataSource = dataSourceResourceItem,
                Id = "ResourceItem",
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            var document = new RdashDocument();
            document.DataSources.Add(dataSourceResourceItem);
            document.Visualizations.Add(visualization);

            RdashDocumentValidator.Validate(document);

            // Act
            var dataSourceItemResult = visualization.GetDataSourceItem();

            // Assert
            Assert.Equal(dataSourceItemResult.ResourceItemDataSource.Id, dataSourceResourceItem.Id);
        }

        [Fact]
        public void GetDataSourceItem_ReturnsCorrectFields_WhenUseTabularData()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };
            var dataSourceResourceItem = new DataSource()
            {
                Id = "dataSourceResourceItemId",
            };
            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = true,
                Fields = new List<IField>
                {
                    new TextField("A"),
                    new TextField("B"),
                    new TextField("C"),
                    new TextField("D"),
                }
            };
            dataSourceItem.ResourceItem = new DataSourceItem()
            {
                DataSource = dataSourceResourceItem,
                Id = "ResourceItem",
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            var document = new RdashDocument();
            document.DataSources.Add(dataSourceResourceItem);
            document.Visualizations.Add(visualization);

            RdashDocumentValidator.Validate(document);

            // Act
            var dataSourceItemResult = visualization.GetDataSourceItem();

            // Assert
            Assert.Equal(dataSourceItem, dataSourceItemResult);
            Assert.Equal((visualization.DataDefinition as TabularDataDefinition).Fields.Count, dataSourceItemResult.Fields.Count);
            Assert.Equal(dataSourceItemResult.DataSource.Id, dataSource.Id);
            Assert.Equal(dataSourceItemResult.ResourceItemDataSource.Id, dataSourceResourceItem.Id);
        }

        [Fact]
        public void UpdateDataSourceItem_UpdateNewDataSourceItem_WithTabularDSItem()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };

            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = true,
                Fields = new List<IField>
                {
                    new TextField("A"),
                    new TextField("B"),
                }
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            var newDataSource = new DataSource();
            var newDataSourceItem = new DataSourceItem
            {
                DataSource = newDataSource,
                HasTabularData = true,
                Fields = new List<IField>
                {
                    new TextField("A1"),
                    new TextField("B2"),
                    new TextField("C3"),
                    new TextField("D4"),
                }
            };

            // Mock join tables
            var leftJoinFieldName = "left";
            var rightJoinFieldName = "right";
            var aliasName = "Alias";
            var joinConditions = new List<JoinCondition> { new JoinCondition(leftJoinFieldName, rightJoinFieldName) };
            var dataSourceItemToJoin = new DataSourceItem();

            newDataSourceItem.Join(aliasName, leftJoinFieldName, rightJoinFieldName, dataSourceItemToJoin);

            // Act
            visualization.UpdateDataSourceItem(newDataSourceItem);

            // Assert
            Assert.Equal(newDataSourceItem, visualization.DataDefinition.DataSourceItem);
            Assert.Equal(newDataSourceItem.Fields.Count, (visualization.DataDefinition as TabularDataDefinition).Fields.Count);
            Assert.Equal(newDataSourceItem.JoinTables.Count, (visualization.DataDefinition as TabularDataDefinition).JoinTables.Count);
        }

        [Fact]
        public void UpdateDataSourceItem_UpdateNewDataSourceItem_WithNonTabularDsItem()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };

            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = false,
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            var newDataSource = new DataSource();
            var newDataSourceItem = new DataSourceItem
            {
                DataSource = newDataSource,
                HasTabularData = false,
            };

            // Act
            visualization.UpdateDataSourceItem(newDataSourceItem);

            // Assert
            Assert.Equal(newDataSourceItem, visualization.DataDefinition.DataSourceItem);
        }

        [Fact]
        public void UpdateDataSourceItem_ChangeNothing_IfDataSourceItemIsNull()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };

            var dataSourceItem = new DataSourceItem
            {
                DataSource = dataSource,
                HasTabularData = false,
            };

            var mock = new Mock<Visualization>("testTitle", dataSourceItem) { CallBase = true };
            var visualization = mock.Object;

            // Act
            visualization.UpdateDataSourceItem(null);

            // Assert
            Assert.Equal(dataSourceItem, visualization.DataDefinition.DataSourceItem);
        }

        [Fact]
        public void ExportJson_ReturnsExpectedJsonFields_WhenUsed()
        {
            // Arrange
            var dataSource = new DataSource()
            {
                Id = "DataSourceId",
            };
            var dataSourceItem = new DataSourceItem
            {
                Id = "DatasourceItemId",
                DataSource = dataSource,
                HasTabularData = true,
                Fields = new List<IField>
                {
                    new TextField("A"),
                }
            };

            var visualization = new ConcreteVisualization("testTitle", dataSourceItem)
            {
                Id = "VisualizationId",
                ColumnSpan = 10,
                RowSpan = 20,
                Description = "Description",
                IsTitleVisible = true,
            };

            var document = new RdashDocument();

            document.Visualizations.Add(visualization);

            RdashDocumentValidator.Validate(document);

            var expectedJson = JObject.Parse("""
                {
                  "Description": "Description",
                  "Id": "VisualizationId",
                  "Title": "testTitle",
                  "IsTitleVisible": true,
                  "ColumnSpan": 10,
                  "RowSpan": 20,
                  "DataSpec": {
                    "_type": "TabularDataSpecType",
                    "IsTransposed": false,
                    "Fields": [
                      {
                        "FieldName": "A",
                        "FieldLabel": "A",
                        "UserCaption": "A",
                        "IsCalculated": false,
                        "Properties": {},
                        "Sorting": "None",
                        "FieldType": "String"
                      }
                    ],
                    "TransposedFields": [],
                    "QuickFilters": [],
                    "AdditionalTables": [],
                    "ServiceAdditionalTables": [],
                    "DataSourceItem": {
                      "_type": "DataSourceItemType",
                      "Id": "DatasourceItemId",
                      "DataSourceId": "DataSourceId",
                      "HasTabularData": true,
                      "HasAsset": false,
                      "Properties": {},
                      "Parameters": {}
                    },
                    "Expiration": 1440,
                    "Bindings": {
                      "Bindings": []
                    }
                  }
                }
            """);

            // Act
            var outputJson = JsonConvert.SerializeObject(visualization, Formatting.Indented, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            // Assert
            Assert.Equal(expectedJson.ToString(), outputJson);

        }

        private class ConcreteVisualization : Visualization
        {
            public ConcreteVisualization(string title, DataSourceItem dataSourceItem) : base(title, dataSourceItem)
            {
            }
        }
    }
}
