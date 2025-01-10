using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class JoinTableFixture
    {
        [Fact]
        public void Constructor_SetDefaultValues_WithoutParams()
        {
            // Act
            var instance = new JoinTable();

            // Assert
            Assert.NotNull(instance.DataDefinition);
            Assert.Empty(instance.JoinConditions);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithAliasDSItem()
        {
            // Arrange
            var alis = "Test Alis";
            var dsItem = new DataSourceItem()
            {
                Title = "Test Title",
                Fields = new List<IField> { new TextField("Test Field") }
            };

            // Act
            var instance = new JoinTable(alis, dsItem);

            // Assert
            Assert.NotNull(instance.DataDefinition);
            Assert.Empty(instance.JoinConditions);
            Assert.Equal(alis, instance.Alias);
            Assert.Equal(dsItem, instance.DataDefinition.DataSourceItem);
            Assert.Equivalent(dsItem.Fields, instance.DataDefinition.Fields);
        }

        [Fact]
        public void Constructor_SetDefaultValues_WithAliasDSItemJoinConditions()
        {
            // Arrange
            var alis = "Test Alis";
            var dsItem = new DataSourceItem()
            {
                Title = "Test Title",
                Fields = new List<IField> { new TextField("Test Field") }
            };
            var joinConditions = new List<JoinCondition>
            {
                new("Left Field", "Right Field")
            };

            // Act
            var instance = new JoinTable(alis, joinConditions, dsItem);

            // Assert
            Assert.NotNull(instance.DataDefinition);
            Assert.Equal(alis, instance.Alias);
            Assert.Equal(dsItem, instance.DataDefinition.DataSourceItem);
            Assert.Equivalent(dsItem.Fields, instance.DataDefinition.Fields);
            Assert.Equivalent(joinConditions, instance.JoinConditions);
        }

        [Fact]
        public void Constructor_ThrowError_WithNullDsItem()
        {
            // Arrange
            var alis = "Test Alis";
            DataSourceItem dsItem = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => new JoinTable(alis, dsItem));
            var msg = $"JoinTable: {nameof(dsItem)}";
            Assert.Contains("Value cannot be null.", ex.Message);
            Assert.Equal("JoinTable: dataSourceItem", ex.ParamName);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJsonString_WithoutCondition()
        {
            // Arrange
            var alis = "Test Alis";
            var dsItem = new DataSourceItem()
            {
                Id = "TestID",
                Title = "Test Title",
                Fields = new List<IField> { new TextField("Test Field") }
            };
            var joinConditions = new List<JoinCondition>
            {
                new("Left Field", "Right Field")
            };

            var instance = new JoinTable(alis, joinConditions, dsItem);

            var expectedJson = """
            {
              "Alias": "Test Alis",
              "DataSpec": {
                "_type": "TabularDataSpecType",
                "IsTransposed": false,
                "Fields": [
                  {
                    "FieldName": "Test Field",
                    "FieldLabel": "Test Field",
                    "UserCaption": "Test Field",
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
                  "Id": "TestID",
                  "Title": "Test Title",
                  "HasTabularData": true,
                  "HasAsset": false,
                  "Properties": {},
                  "Parameters": {}
                },
                "Expiration": 1440,
                "Bindings": {
                  "Bindings": []
                }
              },
              "JoinConditions": [
                {
                  "LeftFieldName": "Left Field",
                  "RightFieldName": "Right Field"
                }
              ]
            }
            """;

            // Act
            var actualJson = instance.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }
    }
}
