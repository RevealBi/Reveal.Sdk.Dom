using Moq;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Primitives
{
    public class FieldBaseFixture
    {
        [Fact]
        public void Constructor_FieldsHaveDefaultValue_WithFieldNameParameter()
        {
            // Act
            var fieldBase = new MockFieldBase("TestFieldName");

            // Assert
            Assert.Equal(DataType.String, ((IFieldDataType)fieldBase).DataType);
            Assert.Empty(fieldBase.Properties);
            Assert.Equal(SortingType.None, fieldBase.Sorting);
        }

        [Fact]
        public void Constructor_UpdateFieldNameAndFieldLabel_WithFieldNameParameter()
        {
            // Arrange
            var testFieldName = "Field Base Field Name";

            // Act
            var fieldBase = new MockFieldBase(testFieldName);

            // Assert
            Assert.Equal(testFieldName, fieldBase.FieldName);
            Assert.Equal(testFieldName, fieldBase.FieldLabel);
        }

        [Fact]
        public void ToJsonString_CreateCorrectJson_WithoutCondition()
        {
            // Arrange
            var expectedJson = """
                {
                  "FieldName": "TestFieldName",
                  "FieldLabel": "TestFieldLabel",
                  "UserCaption": "TestUserCaption",
                  "IsCalculated": false,
                  "Expression": "Test Expression",
                  "Filter": {},
                  "Properties": {
                    "TestKey": "TestValue"
                  },
                  "Sorting": "Asc",
                  "TableAlias": "TestTableAlias",
                  "FieldType": "String"
                }
                """;
            var fieldBase = new MockFieldBase("TestFieldName")
            {
                DataFilter = new MockIFilter(),
                Expression = "Test Expression",
                FieldLabel = "TestFieldLabel",
                FieldName = "TestFieldName",
                IsCalculated = false,
                Properties = new Dictionary<string, object>
                {
                    { "TestKey", "TestValue" }
                },
                Sorting = SortingType.Asc,
                TableAlias = "TestTableAlias",
                UserCaption = "TestUserCaption"
            };

            // Act
            var actualJson = fieldBase.ToJsonString();
            var expectedJObject = JObject.Parse(expectedJson);
            var actualJObject = JObject.Parse(actualJson);

            // Assert
            Assert.Equal(expectedJObject, actualJObject);
        }

        private class MockFieldBase : FieldBase<MockIFilter>
        {
            public MockFieldBase(string fieldName) : base(fieldName)
            {
            }
        }

        private class MockIFilter : IFilter
        { }

        private class MockField : FieldBase<MockIFilter>
        {
            public MockField(string fieldName) : base(fieldName)
            { }
        }
    }
}
