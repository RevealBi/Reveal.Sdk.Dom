using Infragistics.ReportPlus.DashboardModel;
using Reveal.Sdk.Data.Excel;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class GridVisualizationDashboard : IDashboardCreator
    {
        public string Name => "Grid Visualization Dashboard";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("My Grid Visualization Dashboard");

            var excelDSItem = DataSourceFactory.GetMarketingDataSourceItem();

            var gridVS = new GridVisualization("Grid Visualization", excelDSItem)
            {
                ColumnSpan = 3,
                RowSpan = 4,
                Description = "Testing Grid visualization",
                IsTitleVisible = true,
                Linker = new VisualizationLinker()
                {
                    Links = new List<IVisualizationLink>()
                    {

                    },
                    Trigger = LinkTriggerType.SelectRow
                },
                Id = "GridVS",
                Title = "Excel Grid Visualization",
            };

            gridVS.ConfigureSettings((settings) =>
            {
                settings.FontSize = FontSize.Large;
                settings.PageSize = 5;
                settings.DateFieldAlignment = Alignment.Right;
                settings.NumericFieldAlignment = Alignment.Center;
                settings.TextFieldAlignment = Alignment.Right;
                settings.IsFirstColumnFixed = false;
                settings.IsPagingEnabled = false;
            });

            // Data Specs
            gridVS.SetColumns("Date", "Spend", "Territory");

            // Filters
            var spendFilter = new DashboardDataFilter(excelDSItem)
            {
                Title = "Spend Filter",
                FieldName = "Spend",
                AllowMultipleSelection = true,
                AllowEmptySelection = true
            };
            var dateFilter = new DashboardDateFilter(DateFilterRule.Custom(DateTime.Today.AddDays(-15), DateTime.Today.AddDays(-2)));

            document.Filters.Add(spendFilter);
            document.Filters.Add(dateFilter);
            gridVS.ConnectDashboardFilter(spendFilter).ConnectDashboardFilter(dateFilter);

            // Add quick filter
            gridVS.AddFilters("Territory");

            document.Visualizations.Add(gridVS);

            return document;
        }
    }
}
