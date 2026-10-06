using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    internal class ScatterVisualizationDashboard : IDashboardCreator
    {
        public string Name => "Scatter Visualization dashboard";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("Scatter Visualization Dashboard");

            var excelDSItem = DataSourceFactory.GetMarketingDataSourceItem();

            var scatterVS = new ScatterVisualization("Scatter Visualization", excelDSItem)
            {
                ColumnSpan = 3,
                RowSpan = 4,
                Description = "Testing Scatter visualization",
                IsTitleVisible = true,
                Linker = new VisualizationLinker()
                {
                    Links = new List<IVisualizationLink>()
                    {
                        new DashboardLink()
                        {
                            Type = LinkType.OpenDashboard,
                            Title = "OpenUr",
                            Dashboard = "http"
                        }
                    },
                    Trigger = LinkTriggerType.Maximize
                },
                Id = "ScatterVS",
                Title = "Excel Scatter Visualization",
            };

            scatterVS.ConfigureSettings((settings) =>
            {
                settings.ShowLegend = true;
                settings.StartColorIndex = 2;
                settings.XAxisIsLogarithmic = true;
                settings.XAxisMaxValue = 100000;
                settings.XAxisMinValue = 100;
                settings.YAxisIsLogarithmic = true;
                settings.YAxisMaxValue = 750000;
                settings.YAxisMinValue = 100;
                settings.AxisDisplayMode = AxisDisplayMode.None;
            });

            // Data Specs
            scatterVS
                .SetLabel("CampaignID")
                .SetXAxes("Spend")
                .SetYAxes("Budget");

            // Filters
            var spendFilter = new DashboardDataFilter(excelDSItem)
            {
                Title = "Spend Filter",
                FieldName = "Spend",
                AllowMultipleSelection = true,
                AllowEmptySelection = true
            };
            var dateFilter = new DashboardDateFilter("Custom range date filter", DateFilterRule.Custom(DateTime.Today.AddDays(-15), DateTime.Today.AddDays(-2)));

            document.Filters.Add(spendFilter);
            document.Filters.Add(dateFilter);
            scatterVS.ConnectDashboardFilter(spendFilter).ConnectDashboardFilter(dateFilter);

            // Add quick filter
            scatterVS.AddFilters("CTR");

            document.Visualizations.Add(scatterVS);

            return document;
        }
    }
}
