using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DashboardFactories
{
    internal class TimeSeriesVisualizationDashboard : IDashboardCreator
    {
        public string Name => "TimeSeries Visualization Dashboard";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("TimeSeries Visualization Dashboard");

            var excelDSItem = DataSourceFactory.GetMarketingDataSourceItem();

            var timeSeriesVS = new TimeSeriesVisualization("TimeSeries Visualization", excelDSItem)
            {
                ColumnSpan = 3,
                RowSpan = 4,
                Description = "Testing Time Series visualization",
                IsTitleVisible = true,
                Linker = new VisualizationLinker()
                {
                    Links = new List<IVisualizationLink>()
                    {
                        new UrlLink()
                        {
                            Title = "Custom link",
                            Type = LinkType.OpenUrl,
                            Url = "https://help.revealbi.io/user/"
                        }
                    },
                    Trigger = LinkTriggerType.SelectRow
                },
                Id = "TimeSeriesVS",
                Title = "Excel TimeSeries Visualization",
                Date = new DimensionColumn(new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Day
                })
            };

            timeSeriesVS.ConfigureSettings((settings) =>
            {
                settings.AutomaticLabelRotation = true;
                settings.ShowLegend = true;
                settings.StartColorIndex = 2;
                settings.SyncAxis = true;
                settings.Trendline = TrendlineType.QuarticFit;
                settings.YAxisIsLogarithmic = true;
                settings.YAxisMaxValue = 100000;
                settings.YAxisMinValue = 10;
                settings.ZoomLevel = 30;
            });

            // Data Specs
            timeSeriesVS.SetValues("Spend", "Budget")
                        .SetCategory("CampaignID");

            // Filters
            var spendFilter = new DashboardDataFilter(excelDSItem)
            {
                Title = "Spend Filter",
                FieldName = "Spend",
                AllowMultipleSelection = true,
                AllowEmptySelection = true
            };
            var dateFilter = new DashboardDateFilter("Custom date filter", DateFilterRule.Custom(DateTime.Today.AddDays(-15), DateTime.Today.AddDays(-2)));

            document.Filters.Add(spendFilter);
            document.Filters.Add(dateFilter);
            timeSeriesVS.ConnectDashboardFilter(spendFilter).ConnectDashboardFilter(dateFilter);

            // Add quick filter
            timeSeriesVS.AddFilters("Territory");

            document.Visualizations.Add(timeSeriesVS);

            return document;
        }
    }
}
