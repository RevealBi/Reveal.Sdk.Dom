using Moq;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
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
            Assert.Equal(rowSpan, updatedVisualization.RowSpan);
            Assert.Equal(columnSpan, updatedVisualization.ColumnSpan);
            Assert.Same(visualization, updatedVisualization);
        }

        [Fact]
        public void AddDataFilter_UpdateDataDefinition_ForTabularDataDefinition()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };
            var dataDefinition = new TabularDataDefinition()
            {
                Fields = new List<IField>()
                {
                    new Mock_Field("testField")
                }
            };
            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filter = new Mock_IFilter();
            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;

            // Act
            visualization.AddDataFilter("testField", filter);

            // Assert
            Assert.Equal(filter, ((FieldBase<Mock_IFilter>)(vsDataDefinitionFields.First())).DataFilter);
        }

        [Fact]
        public void AddDataFilter_ThrowException_WithNotFoundField()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };
            var dataDefinition = new TabularDataDefinition();
            var field = new Mock_Field("testField");
            dataDefinition.Fields.Add(field);
            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filter = new Mock_IFilter();
            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;

            // Act
            var action = () => visualization.AddDataFilter("NotFoundField", filter);

            // Assert
            var ex = Assert.Throws<Exception>(action);
            Assert.Equal("AddDataFilter: Field NotFoundField cannot be found.", ex.Message);
        }

        [Fact]
        public void AddDataFilter_ThrowException_WithGeneralException()
        {
            // Arrange
            var mockVS = new Mock<IVisualization>() { };
            var dataDefinition = new TabularDataDefinition();
            var field = new Mock_Field("testField");
            dataDefinition.Fields.Add(field);
            mockVS.SetupGet(p => p.DataDefinition).Returns(dataDefinition);
            var visualization = mockVS.Object;

            var filter = new TextFilter();
            var vsDataDefinitionFields = (visualization.DataDefinition as TabularDataDefinition).Fields;

            // Act
            var action = () => visualization.AddDataFilter("testField", filter);

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
            foreach (var fieldName in fieldNames)
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

        private class Mock_IFilter : IFilter { }

        private class Mock_Field : FieldBase<Mock_IFilter>
        {
            public Mock_Field(string fieldName) : base(fieldName)
            { }
        }
    }
}
