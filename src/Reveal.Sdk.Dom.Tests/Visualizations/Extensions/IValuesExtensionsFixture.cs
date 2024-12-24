using Moq;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions
{
    public class IValuesExtensionsFixture
    {
        [Fact]
        public void SetValue_AddValuesItem_WithFieldName()
        {
            // Arrange
            var mockVS = new Mock<IValues>();
            mockVS.SetupGet(x => x.Values).Returns(new List<MeasureColumn>() { new MeasureColumn(new NumberDataField("InitialField")) });
            var visualization = mockVS.Object;

            var fieldName = "TestField";
            var numberField = new NumberDataField(fieldName);
            var expectedVSValues = new List<MeasureColumn>()
            {
                new MeasureColumn() {
                    DataField = numberField
                }
            };

            // Act
            visualization.SetValue(fieldName);

            // Assert
            Assert.Equivalent(expectedVSValues, visualization.Values);
        }

        [Fact]
        public void SetValue_AddValuesItem_WithNumberDataField()
        {
            // Arrange
            var mockVS = new Mock<IValues>();
            mockVS.SetupGet(x => x.Values).Returns(new List<MeasureColumn>() { new MeasureColumn(new NumberDataField("InitialField")) });
            var visualization = mockVS.Object;

            var fieldName = "TestField";
            var numberField = new NumberDataField(fieldName);
            var expectedVSValues = new List<MeasureColumn>()
            {
                new MeasureColumn() {
                    DataField = numberField
                }
            };

            // Act
            visualization.SetValue(numberField);

            // Assert
            Assert.Equivalent(expectedVSValues, visualization.Values);
        }

        [Fact]
        public void SetValues_AddValuesItem_WithListFieldNames()
        {
            // Arrange
            var mockVS = new Mock<IValues>();
            mockVS.SetupGet(x => x.Values).Returns(new List<MeasureColumn>() { new MeasureColumn(new NumberDataField("InitialField")) });
            var visualization = mockVS.Object;

            var fieldNames = new List<string>()
            {
                "FirstField",
                "SecondField"
            };

            var expectedVSValues = fieldNames.Select(fieldName =>
            {
                return new MeasureColumn()
                {
                    DataField = new NumberDataField(fieldName)
                };
            });

            // Act
            visualization.SetValues(fieldNames.ToArray());

            // Assert
            Assert.Equivalent(expectedVSValues, visualization.Values);
        }

        [Fact]
        public void SetValues_AddValuesItem_WithListNumberFields()
        {
            // Arrange
            var mockVS = new Mock<IValues>();
            mockVS.SetupGet(x => x.Values).Returns(new List<MeasureColumn>() { new MeasureColumn(new NumberDataField("InitialField")) });
            var visualization = mockVS.Object;

            var numberFields = new List<NumberDataField>()
            {
                new NumberDataField("FirstField"),
                new NumberDataField("SecondField")
            };
            
            var expectedVSValues = numberFields.Select(field =>
            {
                return new MeasureColumn()
                {
                    DataField = field
                };
            });

            // Act
            visualization.SetValues(numberFields.ToArray());

            // Assert
            Assert.Equivalent(expectedVSValues, visualization.Values);
        }
    }
}
