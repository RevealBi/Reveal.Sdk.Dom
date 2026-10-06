using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using System.Linq;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class SalesDashboard : IDashboardCreator
    {
        public string Name => "Sales";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = DataSourceFactory.GetSalesDataSourceItem();

            var document = new RdashDocument()
            {
                Title = "Sales",
                Description = "I created this in code",
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter("My Date Filter", DateFilterRule.Last(1, PeriodType.Year));
            document.Filters.Add(dateFilter);

            var territoryFilter = new DashboardDataFilter("Territory", excelDataSourceItem);
            document.Filters.Add(territoryFilter);

            document.Visualizations.Add(CreateKpiTargetVisualization(excelDataSourceItem, territoryFilter));
            document.Visualizations.Add(CreateSplineAreaChartVisualization(excelDataSourceItem, dateFilter, territoryFilter));
            document.Visualizations.Add(CreateStackedColumnChartVisualization(excelDataSourceItem, dateFilter, territoryFilter));
            document.Visualizations.Add(CreateIndicatorVisualization(excelDataSourceItem, territoryFilter));
            document.Visualizations.Add(CreateSparklineVisualization(excelDataSourceItem, dateFilter, territoryFilter));
            document.Visualizations.Add(CreateBarChartVisualization(excelDataSourceItem, dateFilter, territoryFilter));
            document.Visualizations.Add(CreateColumnChartVisualization(excelDataSourceItem, dateFilter, territoryFilter));
            document.Visualizations.Add(CreateGaugeVisualization(excelDataSourceItem, dateFilter, territoryFilter));

            return document;
        }

        private static Visualization CreateKpiTargetVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new KpiTargetVisualization("Sales", excelDataSourceItem)
                .SetDate("Date")
                .SetValue(new NumberDataField("Pipepline")
                {
                    FieldLabel = "Actual Sales",
                    AggregationType = AggregationType.Sum,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                        LargeNumberFormat = LargeNumberFormat.Auto,
                    }
                })
                .SetTarget("Forecasted")
                .ConnectDashboardFilters(filters)
                .SetPosition(20, 11);
        }

        private static Visualization CreateSplineAreaChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new SplineAreaChartVisualization("New vs Renewal Sales", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("New Sales", "Renewal Sales ")
                .ConnectDashboardFilters(filters)
                .SetPosition(39, 31);
        }

        private static Visualization CreateStackedColumnChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new StackedColumnChartVisualization("Sales by Product", excelDataSourceItem)
                .SetLabel("Product")
                .SetValues("New Sales", "Renewal Sales ")
                .ConnectDashboardFilters(filters)
                .SetPosition(39, 18);
        }

        private static Visualization CreateIndicatorVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new KpiTimeVisualization("Total Opportunities", excelDataSourceItem)
                .SetDate(new DateDataField("Date") { AggregationType = DateAggregationType.Year })
                .SetValue("Total Opportunites")
                .ConnectDashboardFilters(filters)
                .SetPosition(19, 11);
        }

        private static Visualization CreateSparklineVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new SparklineVisualization("New Seats by Product", excelDataSourceItem)
                .SetDate(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValue(new NumberDataField("New Seats")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Number,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                        LargeNumberFormat = LargeNumberFormat.Auto,
                    }
                })
                .SetCategory("Product")
                .ConfigureSettings(settings =>
                {
                    settings.TextFieldAlignment = Alignment.Left;
                    settings.NumericFieldAlignment = Alignment.Left;
                    settings.DateFieldAlignment = Alignment.Left;
                    settings.AggregationType = SparklineAggregationType.Months;
                })
                .ConnectDashboardFilters(filters)
                .SetPosition(30, 31);
        }

        private static Visualization CreateBarChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new BarChartVisualization("Sales", excelDataSourceItem)
                .SetLabel("Employee")
                .SetValue(new NumberDataField("Pipepline")
                {
                    Sorting = SortingType.Asc,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                        LargeNumberFormat = LargeNumberFormat.Auto,
                    }
                })
                .ConnectDashboardFilters(filters)
                .SetPosition(43, 29);
        }

        private static Visualization CreateColumnChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            return new ColumnChartVisualization("", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Leads", "Hot Leads")
                .ConnectDashboardFilters(filters)
                .SetPosition(46, 31);
        }

        private static Visualization CreateGaugeVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            var visualization = new BulletGraphVisualization("Quotas by Sales Rep", excelDataSourceItem)
                .SetLabel("Employee")
                .SetValue(new NumberDataField("Quota")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Percent,
                        DecimalDigits = 2,
                        ShowGroupingSeparator = false,
                        LargeNumberFormat = LargeNumberFormat.Auto,
                    }
                })
                .ConfigureSettings(settings => 
                {
                    settings.Minimum = new Bound() { Value = 0.8 };
                    settings.Maximum = new Bound() { Value = 2.0 };

                    settings.ValueComparisonType = ValueComparisonType.Number;
                    settings.UpperBand.Value = 100.0;
                    settings.MiddleBand.Value = 80.0;
                })
                .AddDataFilter("Quota", new NumberFilter()
                {
                    FilterType = FilterType.FilterByRule,
                    RuleType = NumberRuleType.TopItems,
                    Value = 10.0
                })
                .ConnectDashboardFilters(filters)
                .SetPosition(33, 29);

            return visualization;
        }
    }
}