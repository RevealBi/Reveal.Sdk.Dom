using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using Sandbox.Helpers;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class MarketingDashboard : IDashboardCreator
    {
        public string Name => "Marketing";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument()
            {
                Title = "Marketing",
                Description = "I created this in code",
                Theme = Theme.TropicalIsland,
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter(DateFilterRule.Last(1, PeriodType.Year));
            document.Filters.Add(dateFilter);

            var excelDataSourceItem = DataSourceFactory.GetMarketingDataSourceItem();

            document.Visualizations.Add(CreateKpiTargetVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateLineChartVisualization(excelDataSourceItem, dateFilter));
            document.Visualizations.Add(CreatePieChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateColumnChartVisualization(excelDataSourceItem, dateFilter));
            document.Visualizations.Add(CreateFunnelChartVisualization(excelDataSourceItem, dateFilter));
            document.Visualizations.Add(CreatePivotVisualization(excelDataSourceItem));

            return document;
        }

        internal static Visualization CreateKpiTargetVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new KpiTargetVisualization(excelDataSourceItem)
            {
                Title = "Spend vs Budget",
                ColumnSpan = 10,
                RowSpan = 24,
            };

            visualization.Date = new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Year,
                    Formatting = new DateFormatting("dd-MMM-yyyy")
                }
            };

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Spend")
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
                }
            });

            visualization.Targets.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Budget")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Number,
                        ShowGroupingSeparator = true,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                    }
                }
            });

            return visualization;
        }

        internal static Visualization CreateLineChartVisualization(DataSourceItem excelDataSourceItem, DashboardDateFilter dateFilter)
        {
            var visualization = new LineChartVisualization(excelDataSourceItem)
            {
                Title = "Actual Spend vs Budget",
                ColumnSpan = 30,
                RowSpan = 24,
            };

            visualization.ConnectDashboardFilter(dateFilter, "Date");

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

        internal static Visualization CreatePieChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new PieChartVisualization(excelDataSourceItem)
            {
                Title = "Spend by Territory",
                ColumnSpan = 20,
                RowSpan = 24,
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Territory")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Spend")
            });

            return visualization;
        }

        internal static Visualization CreateColumnChartVisualization(DataSourceItem excelDataSourceItem, DashboardDateFilter dateFilter)
        {
            var visualization = new ColumnChartVisualization(excelDataSourceItem)
            {
                Title = "Spend by Territory",
                ColumnSpan = 27,
                RowSpan = 36,
            };

            visualization.ConnectDashboardFilter(dateFilter, "Date");

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

        internal static Visualization CreateFunnelChartVisualization(DataSourceItem excelDataSourceItem, DashboardDateFilter dateFilter)
        {
            var visualization = new FunnelChartVisualization(excelDataSourceItem)
            {
                Title = "Conversions by Campaign",
                ColumnSpan = 13,
                RowSpan = 36,
            };

            visualization.ConnectDashboardFilter(dateFilter, "Date");

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("CampaignID")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Conversions")
            });

            return visualization;
        }

        internal static Visualization CreatePivotVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new PivotVisualization(excelDataSourceItem)
            {
                Title = "New Seats by Campaign ID",
                ColumnSpan = 20,
                RowSpan = 36,
            };

            visualization.Settings.TextFieldAlignment = Alignment.Left;
            visualization.Settings.NumericFieldAlignment = Alignment.Right;
            visualization.Settings.DateFieldAlignment = Alignment.Left;

            visualization.Rows.Add(new DimensionColumn()
            {
                DataField = new TextDataField("CampaignID")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("CTR")
                {
                    AggregationType = AggregationType.Avg,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Percent,
                        DecimalDigits = 2,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                        LargeNumberFormat = LargeNumberFormat.Auto,
                    }
                }
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Avg. CPC")
                {
                    AggregationType = AggregationType.Avg,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                        ShowGroupingSeparator = true,
                    }
                }
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("New Seats")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        CurrencySymbol = "$",
                        NegativeFormat = NegativeFormatType.MinusSign,
                        ShowGroupingSeparator = true,
                    }
                }
            });

            return visualization;
        }
    }
}
