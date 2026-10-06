using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    internal class SparklineVisualizationDashboard : IDashboardCreator
    {
        public string Name => "Sparkline Visualization";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("Sparkline Visualization Dashboard");

            var excelDSItem = DataSourceFactory.GetMarketingDataSourceItem();

            var sparklineVS = new SparklineVisualization("Sparkline Visualization", excelDSItem)
            {
                ColumnSpan = 3,
                RowSpan = 4,
                Description = "Testing Sparkline visualization",
                IsTitleVisible = true,
                Linker = new VisualizationLinker()
                {
                    Links = new List<IVisualizationLink>()
                    {
                        new UrlLink()
                        {
                            Title = "URL Link",
                            Filters = new List<LinkFilter>(),
                            Type = LinkType.OpenUrl,
                            Url = "https://help.revealbi.io/web/datasources/"
                        }
                    },
                    Trigger = LinkTriggerType.Maximize
                },
                Id = "SparklineVSId",
                Title = "Excel Sparkline Visualization",
                Date = new DimensionColumn()
                {
                    DataField = new DateDataField("Date")
                    {
                        Description = "Date Data Field Description",
                        AggregationType = DateAggregationType.Quarter,
                        Formatting = new DateFormatting("dd-MMM-yyyy")
                    }
                },
            };

            sparklineVS.ConfigureSettings((settings) =>
            {
                settings.FontSize = FontSize.Large;
                settings.DateFieldAlignment = Alignment.Center;
                settings.NumericFieldAlignment = Alignment.Center;
                settings.TextFieldAlignment = Alignment.Center;
                settings.AggregationType = SparklineAggregationType.Years;
                settings.NumberOfPeriods = 5;
                settings.PositiveIsRed = true;
                settings.ShowDifference = true;
                settings.ShowLastTwoValues = true;
                settings.ChartType = SparklineChartType.Area;
            });

            // Data Specs
            sparklineVS.SetDate("Date")
                        .SetValue("Spend")
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
            var dateFilter = new DashboardDateFilter(DateFilterRule.Custom(DateTime.Today.AddYears(-3), DateTime.Today.AddDays(-2)));

            document.Filters.Add(spendFilter);
            document.Filters.Add(dateFilter);
            sparklineVS.ConnectDashboardFilter(spendFilter).ConnectDashboardFilter(dateFilter);

            // Add quick filter
            sparklineVS.AddFilters("Territory");

            document.Visualizations.Add(sparklineVS);

            return document;
        }
    }
}
