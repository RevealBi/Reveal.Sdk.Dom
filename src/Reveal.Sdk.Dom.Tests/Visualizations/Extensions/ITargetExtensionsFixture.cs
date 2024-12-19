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
    public class ITargetExtensionsFixture
    {
        [Fact]
        public void SetTarget_UpdateTargets_WithFieldName()
        {
            // Arrange
            var mockTarget = new Mock<ITargets>();
            var targets = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } };
            mockTarget.Setup(s => s.Targets).Returns(targets);
            var targetObj = mockTarget.Object;

            var fieldName = "TestField";
            var numberField = new NumberDataField(fieldName);
            var expectedTargetList = new List<MeasureColumn>() { new MeasureColumn() { DataField = numberField } };

            // Act
            targetObj.SetTarget(fieldName);

            // Assert
            Assert.Equivalent(expectedTargetList, targetObj.Targets);
        }

        [Fact]
        public void SetTarget_UpdateTargets_WithNumberDataField()
        {
            // Arrange
            var mockTarget = new Mock<ITargets>();
            var targets = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } };
            mockTarget.Setup(s => s.Targets).Returns(targets);
            var targetObj = mockTarget.Object;

            var fieldName = "TestField";
            var numberField = new NumberDataField(fieldName);
            var expectedTargetList = new List<MeasureColumn>() { new MeasureColumn() { DataField = numberField } };

            // Act
            targetObj.SetTarget(numberField);

            // Assert
            Assert.Equivalent(expectedTargetList, targetObj.Targets);
        }

        [Fact]
        public void SetTargets_UpdateTargets_WithListFieldNames()
        {
            // Arrange
            var mockTarget = new Mock<ITargets>();
            var targets = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } };
            mockTarget.Setup(s => s.Targets).Returns(targets);
            var targetObj = mockTarget.Object;

            var fieldNames = new List<string>() { "FirstField", "SecondField" };
            var expectedTargetList = fieldNames.Select(fieldName => new MeasureColumn()
            {
                DataField = new NumberDataField(fieldName)
            });

            // Act
            targetObj.SetTargets(fieldNames.ToArray());

            // Assert
            Assert.Equivalent(expectedTargetList, targetObj.Targets);
        }

        [Fact]
        public void SetTargets_UpdateTargets_WithListNumberDataField()
        {
            // Arrange
            var mockTarget = new Mock<ITargets>();
            var targets = new List<MeasureColumn>() { new MeasureColumn() { DataField = new NumberDataField("InitialField") } };
            mockTarget.Setup(s => s.Targets).Returns(targets);
            var targetObj = mockTarget.Object;

            var numberFields = new List<NumberDataField>()
            {
                new NumberDataField("FirstField"),
                new NumberDataField("SecondField"),
            };
            var expectedTargetList = numberFields.Select(field => new MeasureColumn()
            {
                DataField = field
            });

            // Act
            targetObj.SetTargets(numberFields.ToArray());

            // Assert
            Assert.Equivalent(expectedTargetList, targetObj.Targets);
        }
    }
}
