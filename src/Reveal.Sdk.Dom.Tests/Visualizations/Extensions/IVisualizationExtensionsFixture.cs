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

        // TODO: We should find a faster way to create Generic constructor / method 
        [Theory]
        [InlineData(typeof(TextFilter), typeof(TextField))]
        public void AddDataFilter_UpdateDataDefinition_ForTabularDataDefinition(Type filterType, Type fieldType)
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) });
            var fieldObj = fieldConstructor.Invoke(new object[] { "testField" });
            var field = (fieldType.IsAssignableFrom(fieldObj.GetType())) ? (dynamic)fieldObj : null;

            dataDefinition.Fields.Add((IField)field);

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filterConstructor = filterType.GetConstructor(Type.EmptyTypes);
            var filter = filterConstructor.Invoke(new object[] { });

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

        // TODO: We should find a faster way to create Generic constructor / method
        // TODO: Need to check how to write unit test to cover all the cases of the catch block
        [Theory]
        [InlineData(typeof(TextFilter), typeof(TextField))]
        public void AddDataFilter_ThrowException_WithNotFoundField(Type filterType, Type fieldType)
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) });
            var fieldObj = fieldConstructor.Invoke(new object[] { "testField" });
            var field = (fieldType.IsAssignableFrom(fieldObj.GetType())) ? (dynamic)fieldObj : null;

            dataDefinition.Fields.Add((IField)field);

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filterConstructor = filterType.GetConstructor(Type.EmptyTypes);
            var filter = filterConstructor.Invoke(new object[] { });

            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;

            // Act
            var addDataFilterMethod = typeof(IVisualizationExtensions).GetMethod(nameof(IVisualizationExtensions.AddDataFilter));
            var addDataFilterGenericMethod = addDataFilterMethod.MakeGenericMethod(typeof(IVisualization), filterType);
            var action = () => addDataFilterGenericMethod.Invoke(visualization, new object[] { visualization, "NotFoundField", filter });

            // Assert
            var ex = Assert.Throws<TargetInvocationException>(action);
            Assert.IsType<Exception>(ex.InnerException);
            Assert.Equal("AddDataFilter: Field NotFoundField cannot be found.", ex.InnerException.Message);
        }

        // TODO: We should find a faster way to create Generic constructor / method
        // TODO: Need to check how to write unit test to cover all the cases of the catch block
        [Theory]
        [InlineData(typeof(TextFilter), typeof(TextField))]
        public void AddDataFilter_ThrowException_WithGeneralException(Type filterType, Type fieldType)
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldConstructor = fieldType.GetConstructor(new[] { typeof(string) });
            var fieldObj = fieldConstructor.Invoke(new object[] { "testField" });
            var field = (fieldType.IsAssignableFrom(fieldObj.GetType())) ? (dynamic)fieldObj : null;

            dataDefinition.Fields.Add((IField)field);

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filterConstructor = filterType.GetConstructor(Type.EmptyTypes);
            var filter = filterConstructor.Invoke(new object[] { });

            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;


            // Act
            var action = () => visualization.AddDataFilter("testField", (IFilter)filter);

            // Assert
            var ex = Assert.Throws<Exception>(action);
            Assert.Equal($"AddDataFilter: Field testField is not compatible with {filter.GetType()}.", ex.Message);
        }

        [Fact]
        public void AddFilter_AddQuickFilter_DataDefinitionIsTabular()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldName = "testField";
            var field = new TextField(fieldName);
            dataDefinition.Fields.Add(field);

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;
            var expectedQuickFilter = new VisualizationFilter(fieldName);

            // Act
            visualization.AddFilter(fieldName);

            // Assert
            Assert.Equivalent(expectedQuickFilter, (visualization.DataDefinition as TabularDataDefinition).QuickFilters.FirstOrDefault());
        }

        [Fact]
        public void AddFilter_AddQuickFilters_DataDefinitionIsTabular()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            var fieldNames = new List<string>() { "Field1", "Field2" };
            foreach (var fieldName in fieldNames)
            {
                dataDefinition.Fields.Add(new TextField(fieldName));
            }

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;
            var expectedQuickFilters = new List<VisualizationFilter>();
            foreach(var fieldName in fieldNames)
            {
                expectedQuickFilters.Add(new VisualizationFilter(fieldName));
            }

            // Act
            visualization.AddFilters(fieldNames.ToArray());

            // Assert
            Assert.Equivalent(expectedQuickFilters, (visualization.DataDefinition as TabularDataDefinition).QuickFilters);
        }

        [Fact]
        public void ConnectDashboardFilter_BindDateFilter_ConnectDateFilterAndWithoutFieldName()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            mockVS.SetupGet(p => p.FilterBindings).Returns(new List<Binding>());
            var visualization = mockVS.Object;

            var dateFilter = new DashboardDateFilter();
            var expectedBinding = new DashboardDateFilterBinding("Date");

            // Act
            visualization.ConnectDashboardFilter(dateFilter);

            // Assert
            Assert.Equivalent(expectedBinding, visualization.FilterBindings.FirstOrDefault());
        }

        [Fact]
        public void ConnectDashboardFilter_BindDataFilter_ConnectDataFilterAndWithoutFieldName()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            mockVS.SetupGet(p => p.FilterBindings).Returns(new List<Binding>());
            var visualization = mockVS.Object;

            var dataFilter = new DashboardDataFilter();
            var expectedBinding = new DashboardDataFilterBinding(dataFilter);

            // Act
            visualization.ConnectDashboardFilter(dataFilter);

            // Assert
            Assert.Equivalent(expectedBinding, visualization.FilterBindings.FirstOrDefault());
        }

        [Fact]
        public void ConnectDashboardFilter_BindDateFilter_ConnectDateFilterAndWithFieldName()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            mockVS.SetupGet(p => p.FilterBindings).Returns(new List<Binding>());
            var visualization = mockVS.Object;

            var dateFieldName = "TestDate";
            var dateFilter = new DashboardDateFilter();
            var expectedBinding = new DashboardDateFilterBinding(dateFieldName);

            // Act
            visualization.ConnectDashboardFilter(dateFilter, dateFieldName);

            // Assert
            Assert.Equivalent(expectedBinding, visualization.FilterBindings.FirstOrDefault());
        }

        [Fact]
        public void ConnectDashboardFilter_BindDataFilter_ConnectDataFilterAndWithFieldName()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            mockVS.SetupGet(p => p.FilterBindings).Returns(new List<Binding>());
            var visualization = mockVS.Object;

            var dataFieldName = "TestDate";
            var dataFilter = new DashboardDataFilter();
            var expectedBinding = new DashboardDataFilterBinding(dataFilter, dataFieldName);

            // Act
            visualization.ConnectDashboardFilter(dataFilter, dataFieldName);

            // Assert
            Assert.Equivalent(expectedBinding, visualization.FilterBindings.FirstOrDefault());
        }

        [Fact]
        public void ConnectDashboardFilters_BindDataFilters_WithDifferentFilterTypes()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };

            var dataDefinition = new TabularDataDefinition();

            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            mockVS.SetupGet(p => p.FilterBindings).Returns(new List<Binding>());
            var visualization = mockVS.Object;

            var dateFilter = new DashboardDateFilter();
            var dataFilter = new DashboardDataFilter();
            var dashboardFilters = new List<DashboardFilter>()
            {
                dateFilter,
                dataFilter
            };
            var expectedDashboardFilterBindings = new List<Binding>()
            {
                new DashboardDateFilterBinding("Date"),
                new DashboardDataFilterBinding(dataFilter)
            };

            // Act
            visualization.ConnectDashboardFilters(dashboardFilters.ToArray());

            // Assert
            Assert.Equivalent(expectedDashboardFilterBindings, visualization.FilterBindings);
        }
    }
}
