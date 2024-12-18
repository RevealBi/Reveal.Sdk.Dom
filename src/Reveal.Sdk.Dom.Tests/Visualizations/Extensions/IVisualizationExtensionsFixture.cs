using Moq;
using Newtonsoft.Json;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Reflection;

namespace Reveal.Sdk.Dom.Tests.Visualizations.Extensions
{
    public class IVisualizationExtensionsFixture
    {
        [Fact]
        public void SetPosition_UpdateRowAndColumnSpan_WithoutCondition()
        {
            // Arrange
            var mockVisualization = new Mock<IVisualization>();
            mockVisualization.SetupProperty(v => v.RowSpan);
            mockVisualization.SetupProperty(v => v.ColumnSpan);
            var visualization = mockVisualization.Object;

            var rowSpan = 10;
            var columnSpan = 20;

            // Act
            var updatedVisualization = visualization.SetPosition(rowSpan, columnSpan);

            // Assert
            Assert.Equal(rowSpan, visualization.RowSpan);
            Assert.Equal(columnSpan, visualization.ColumnSpan);
            Assert.Same(visualization, updatedVisualization);
        }

        [Theory]
        [InlineData(typeof(TextFilter), typeof(TextField))]
        public void AddDataFilter_UpdateDataDefinition_ForTabularDataDefinition(Type filterType, Type fieldType)
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) } );
            var fieldObj = fieldConstructor.Invoke(new object[] { "testField" });
            var field = (fieldType.IsAssignableFrom(fieldObj.GetType())) ? (dynamic)fieldObj : null;

            dataDefinition.Fields.Add((IField)field);

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filterConstructor = filterType.GetConstructor( Type.EmptyTypes );
            var filter = filterConstructor.Invoke(new object[] {});

            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;

            foreach (var f in vsDataDefinitionFields)
            {
                // Act
                var addDataFilterMethod = typeof(IVisualizationExtensions).GetMethod(nameof(IVisualizationExtensions.AddDataFilter));
                var addDataFilterGenericMethod = addDataFilterMethod.MakeGenericMethod(typeof(IVisualization), filterType);
                addDataFilterGenericMethod.Invoke(visualization, new object[] { visualization, f.FieldName, filter });

                Type genericFilterType = typeof(FieldBase<>).MakeGenericType(filterType);
                var propertyInfo = genericFilterType.GetProperty("DataFilter");
                var actualFilter = propertyInfo.GetValue(f);


                // Assert
                Assert.Same(filter, actualFilter);
            }
        }
    }
}
