using DocumentFormat.OpenXml.Spreadsheet;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class CampaignsDashboard: IDashboardCreator
    {
        public string Name => "Campaigns";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = DataSourceFactory.GetMarketingDataSourceItem();

            var document = new RdashDocument()
            {
                Title = "Campaigns",
                Description = "I created this in code",
                Theme = Theme.TropicalIsland,
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter(DateFilterRule.Previous(12, PeriodType.Month));
            document.Filters.Add(dateFilter);

            var campaignIdFilter = new DashboardDataFilter(excelDataSourceItem)
            {
                Title = "CampaignID",
                FieldName = "CampaignID",
                AllowMultipleSelection = true,
                AllowEmptySelection = true
            };
            document.Filters.Add(campaignIdFilter);

            document.Visualizations.Add(CreateKpiTargetVisualization(excelDataSourceItem, campaignIdFilter));
            document.Visualizations.Add(CreateIndicatorVisualization("Website Traffic", "Traffic", excelDataSourceItem, campaignIdFilter));
            document.Visualizations.Add(CreateIndicatorVisualization("Conversions", "Conversions", excelDataSourceItem, campaignIdFilter));
            document.Visualizations.Add(CreateIndicatorVisualization("New Seats", "New Seats", excelDataSourceItem, campaignIdFilter));
            document.Visualizations.Add(CreateSplineAreaChartVisualization(excelDataSourceItem, dateFilter, campaignIdFilter));
            document.Visualizations.Add(CreateStackedColumnChartVisualization(excelDataSourceItem, dateFilter, campaignIdFilter));
            document.Visualizations.Add(CreateLineChartVisualization(excelDataSourceItem, dateFilter, campaignIdFilter));
            document.Visualizations.Add(CreateDoughnutChartVisualization(excelDataSourceItem, dateFilter, campaignIdFilter));

            return document;
        }

        private static Visualization CreateKpiTargetVisualization(DataSourceItem excelDataSourceItem, DashboardDataFilter filter)
        {
            var visualization = new KpiTargetVisualization(excelDataSourceItem)
            {
                Title = "Spend vs Budget",
                ColumnSpan = 15,
                RowSpan = 13,
            };

            visualization.ConnectDashboardFilter(filter);

            visualization.Date = new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Year,
                }
            };

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Spend")
            });

            visualization.Targets.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Budget")
            });

            return visualization;
        }

        private static Visualization CreateIndicatorVisualization(string title, string field, DataSourceItem excelDataSourceItem, DashboardDataFilter filter)
        {
            var visualization = new KpiTimeVisualization(excelDataSourceItem)
            {
                Title = title,
                ColumnSpan = 15,
                RowSpan = 13,
            };

            visualization.ConnectDashboardFilter(filter);

            visualization.Date = new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Year,
                }
            };
            
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField(field)
            });

            return visualization;
        }

        private static Visualization CreateSplineAreaChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            var visualization = new SplineAreaChartVisualization(excelDataSourceItem)
            {
                Title = "Actual Spend vs Budget",
                ColumnSpan = 30,
                RowSpan = 28,
            };

            visualization.ConnectDashboardFilters(filters);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Spend")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Budget")
            });

            return visualization;
        }

        private static Visualization CreateStackedColumnChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            var visualization = new StackedColumnChartVisualization(excelDataSourceItem)
            {
                Title = "Website Traffic Breakdown",
                ColumnSpan = 30,
                RowSpan = 28,
            };

            visualization.ConnectDashboardFilters(filters);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Paid Traffic")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Organic Traffic")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Other Traffic")
            });

            return visualization;
        }

        private static Visualization CreateLineChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            var visualization = new LineChartVisualization(excelDataSourceItem)
            {
                Title = "Conversions",
                ColumnSpan = 45,
                RowSpan = 19,
            };

            visualization.ConnectDashboardFilters(filters);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Conversions")
            });

            return visualization;
        }

        private static Visualization CreateDoughnutChartVisualization(DataSourceItem excelDataSourceItem, params DashboardFilter[] filters)
        {
            var visualization = new DoughnutChartVisualization(excelDataSourceItem)
            {
                Title = "Conversions by Territory",
                ColumnSpan = 15,
                RowSpan = 19,
            };

            visualization.ConnectDashboardFilters(filters);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Territory")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Conversions")
            });

            return visualization;
        }
    }
}
