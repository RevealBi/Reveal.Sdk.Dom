using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    internal class SplineAreaChartVisualizationDashboard : IDashboardCreator
    {
        public string Name => "Spline Area Chart Visualization Dashboard";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("Spline Area Chart Visualization Dashboard");

            var excelDSItem = DataSourceFactory.GetMarketingDataSourceItem();

            var splineAreaChartVS = new SplineAreaChartVisualization("Spline Area Chart Visualization", excelDSItem)
            {
                ColumnSpan = 3,
                RowSpan = 4,
                Description = "Testing Spline Chart Area visualization",
                IsTitleVisible = true,
                Linker = new VisualizationLinker()
                {
                    Links = new List<IVisualizationLink>()
                    {
                        new UrlLink()
                        {
                            Title = "Custom Link",
                            Url = "https://help.revealbi.io/user/",
                            Type = LinkType.OpenUrl
                        }
                    },
                    Trigger = LinkTriggerType.SelectRow
                },
                Id = "SPLChartID",
                Title = "Excel Spline Chart Visualization",
                Category = new DimensionColumn(),
            };

            splineAreaChartVS.ConfigureSettings((settings) =>
            {
                settings.AutomaticLabelRotation = true;
                settings.ShowLegend = true;
                settings.StartColorIndex = 2;
                settings.SyncAxis = true;
                settings.Trendline = TrendlineType.QuarticFit;
                settings.YAxisIsLogarithmic = true;
                settings.YAxisMinValue = 10;
                settings.YAxisMaxValue = 100000;
                settings.ZoomLevel = 3;
            });

            // Data Specs
            splineAreaChartVS.SetLabel("CampaignID")
                                .SetValues("Spend")
                                .SetCategory("Territory");

            // Filters
            var spendFilter = new DashboardDataFilter(excelDSItem)
            {
                Title = "Spend Filter",
                FieldName = "Spend",
                AllowMultipleSelection = true,
                AllowEmptySelection = true,
                IsDynamic = true,
                SortByLabel = true
            };
            var dateFilter = new DashboardDateFilter("Date Filter", DateFilterRule.Custom(DateTime.Today.AddDays(-15), DateTime.Today.AddDays(-2)));

            document.Filters.Add(spendFilter);
            document.Filters.Add(dateFilter);
            splineAreaChartVS.ConnectDashboardFilter(spendFilter).ConnectDashboardFilter(dateFilter);

            // Add quick filter
            splineAreaChartVS.AddFilters("CampaignID");

            document.Visualizations.Add(splineAreaChartVS);

            return document;
        }
    }
}
