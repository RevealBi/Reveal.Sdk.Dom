using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Moq;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Xunit;

public class VisualizationTests
{
    private class TestSettings : VisualizationSettings
    {
        public string CustomSetting { get; set; }
    }

    private class TestVisualization : Visualization<TestSettings>
    {
        public TestVisualization(string title, DataSourceItem dataSourceItem)
            : base(title, dataSourceItem)
        {
        }

        protected override void InitializeDataDefinition(DataSourceItem dataSourceItem)
        {
            DataDefinition = new TabularDataDefinition();
        }
    }

    private class StubDataSourceItem : DataSourceItem
    {
        public bool HasTabularData { get; set; } = false;
    }

    private DataSourceItem CreateStubDataSourceItem(bool hasTabularData = false)
    {
        return new StubDataSourceItem
        {
            HasTabularData = hasTabularData
        };
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties_Correctly()
    {
        // Arrange
        var dataSourceItem = CreateStubDataSourceItem();

        // Act
        var visualization = new TestVisualization("Test Title", dataSourceItem);

        // Assert
        Assert.NotNull(visualization.Id);
        Assert.Equal("Test Title", visualization.Title);
        Assert.True(visualization.IsTitleVisible);
        Assert.Equal(0, visualization.ColumnSpan);
        Assert.Equal(0, visualization.RowSpan);
        Assert.NotNull(visualization.Settings);
        Assert.IsType<TestSettings>(visualization.Settings);
    }

    [Fact]
    public void GetDataSourceItem_ShouldThrowException_WhenFieldsAreEmpty()
    {
        // Arrange
        var dataSourceItem = CreateStubDataSourceItem();
        var document = new RdashDocument
        {
            DataSources = new List<DataSource>
            {
                new DataSource { Id = "mockSourceId" }
            }
        };

        // Create a DataDefinition with an empty Fields list
        var dataDefinition = new TabularDataDefinition
        {
            DataSourceItem = dataSourceItem,
            Fields = new List<IField>()  // Empty list
        };

        // Now assign the initialized DataDefinition to your visualization
        var visualization = new TestVisualization("Test Title", dataSourceItem)
        {
            DataDefinition = dataDefinition
        };

        ((IParentDocument)visualization).Document = document;

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => visualization.GetDataSourceItem());
    }

    [Fact]
    public void Linker_ShouldAllowAddingUrls_WhenValidUrlIsProvided()
    {
        // Arrange
        var linker = new VisualizationLinker();

        // Act
        linker.AddUrl("Google", "https://google.com");

        // Assert
        Assert.Single(linker.Links);
        Assert.Equal("Google", linker.Links[0].Title);
        Assert.Equal(LinkType.OpenUrl, linker.Links[0].Type);
    }

    [Fact]
    public void Linker_ShouldAllowAddingDashboards_WhenValidDashboardIsProvided()
    {
        // Arrange
        var linker = new VisualizationLinker();

        // Act
        linker.AddDashboard("Dashboard1", "dash123", new LinkFilter { Name = "Filter1" });

        // Assert
        Assert.Single(linker.Links);
        Assert.Equal("Dashboard1", linker.Links[0].Title);
        Assert.Equal(LinkType.OpenDashboard, linker.Links[0].Type);
    }

    [Fact]
    public void Settings_ShouldBeSerializable_WhenVisualizationIsSerialized()
    {
        // Arrange
        var visualization = new TestVisualization("Test Title", CreateStubDataSourceItem());

        // Act
        var json = JsonConvert.SerializeObject(visualization.Settings);

        // Assert
        Assert.NotNull(json);
        Assert.Contains("VisualizationType", json);
    }

    [Fact]
    public void Title_ShouldBeSettable_WhenNewTitleIsAssigned()
    {
        // Arrange
        var visualization = new TestVisualization("Old Title", CreateStubDataSourceItem());

        // Act
        visualization.Title = "New Title";

        // Assert
        Assert.Equal("New Title", visualization.Title);
    }
}