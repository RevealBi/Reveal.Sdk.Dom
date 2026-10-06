using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using System.Linq;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class ManufacturingDashboard : IDashboardCreator
    {
        public string Name => "Manufacturing";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = DataSourceFactory.GetManufacturingDataSourceItem();

            var document = new RdashDocument("Manufacturing")
            {
                Title = "Manufacturing",
                Theme = Theme.RockyMountain,
                Description = "I created this in code",
                UseAutoLayout = false,
            };

            document.Visualizations.Add(CreateIndicatorVisualization("Productivity", "Overall Plant Productivity ", excelDataSourceItem, true));
            document.Visualizations.Add(CreateIndicatorVisualization("Units Lost", "Units Lost", excelDataSourceItem));
            document.Visualizations.Add(CreateLineChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateColumnChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateDoughnutChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateCircularGaugeVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateCircularGaugeVisualization2(excelDataSourceItem));
            document.Visualizations.Add(CreateColumnChartVisualization2(excelDataSourceItem));

            return document;
        }

        private static Visualization CreateIndicatorVisualization(string title, string field, DataSourceItem excelDataSourceItem, bool avg = false)
        {
            var visualization = new KpiTimeVisualization(excelDataSourceItem)
            {
                Title = title,
                ColumnSpan = 10,
                RowSpan = 22,
            };

            visualization.Date = new DimensionColumn()
            {
                DataField = new DateDataField("Date")
            };

            NumberDataField dataField = new NumberDataField(field);
            if (avg)
            {
                dataField.AggregationType = AggregationType.Avg;
                dataField.Formatting = new NumberFormatting()
                {
                    FormatType = NumberFormattingType.Percent,
                    DecimalDigits = 0,
                    ShowGroupingSeparator = true,
                    LargeNumberFormat = LargeNumberFormat.Auto
                };
            }

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = dataField
            });

            return visualization;
        }

        private static Visualization CreateLineChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new LineChartVisualization(excelDataSourceItem)
            {
                Title = "Cost of Labor vs Revenue",
                ColumnSpan = 40,
                RowSpan = 22,
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Date").First() as DateField;
            field.DataFilter = new DateTimeFilter(DateFilterRule.Last(1, PeriodType.Year))
            {
                FilterType = FilterType.FilterByRule,
                DateFiscalYearStartMonth = 0,
                DisplayInLocalTimeZone = false
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Cost of Labor ")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                    }
                }
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Revenue")
                {
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Currency,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                    }
                }
            });

            return visualization;
        }

        private static Visualization CreateColumnChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new ColumnChartVisualization(excelDataSourceItem)
            {
                Title = "Units Produced By Line",
                ColumnSpan = 19,
                RowSpan = 38
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Date").First() as DateField;
            field.DataFilter = new DateTimeFilter(DateFilterRule.Last(1, PeriodType.Month))
            {
                FilterType = FilterType.FilterByRule,
                DateFiscalYearStartMonth = 0,
                DisplayInLocalTimeZone = false
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Line")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Units Produced")
                {
                    Sorting = SortingType.Desc,
                    Formatting = new NumberFormatting()
                    {
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                    }
                }
            });

            return visualization;
        }

        private static Visualization CreateDoughnutChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new DoughnutChartVisualization(excelDataSourceItem)
            {
                Title = "Operators Available by Function",
                ColumnSpan = 21,
                RowSpan = 38,
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Date").First() as DateField;
            field.DataFilter = new DateTimeFilter(DateFilterRule.Last(1, PeriodType.Month))
            {
                FilterType = FilterType.FilterByRule,
                DateFiscalYearStartMonth = 0,
                DisplayInLocalTimeZone = false
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Operators by Function")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Operators Available ")
                {
                    Sorting = SortingType.Asc,
                    Formatting = new NumberFormatting()
                    {
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true
                    }
                }
            });

            return visualization;
        }

        private static Visualization CreateCircularGaugeVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new CircularGaugeVisualization(excelDataSourceItem)
            {
                Title = "Line 2 Efficiency",
                ColumnSpan = 20,
                RowSpan = 19
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Line").First() as TextField;
            field.DataFilter = new TextFilter()
            {
                FilterType = FilterType.SelectedValues,
                SelectedValues = new System.Collections.Generic.List<FilterValue>()
                {
                    new FilterValue() { Name = "Line 5", Value = "Line 5"},
                    new FilterValue() { Name = "Line 2", Value = "Line 2"}
                }
            };

            visualization.Settings.Minimum = new Bound() { Value = 0.0, ValueType = BoundValueType.NumberValue };
            visualization.Settings.Maximum = new Bound() { Value = 1.0, ValueType = BoundValueType.NumberValue };
            visualization.Settings.UpperBand.Value = 100.0;
            visualization.Settings.MiddleBand.Value = 0.0;

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Efficiency")
                {
                    AggregationType = AggregationType.Avg,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Percent,
                        DecimalDigits = 2,
                        ShowGroupingSeparator = false,
                        LargeNumberFormat = LargeNumberFormat.Auto
                    }
                }
            });

            visualization.Label = new DimensionColumn()
            {
                DataField = new TextDataField("Line")
            };

            return visualization;
        }

        private static Visualization CreateCircularGaugeVisualization2(DataSourceItem excelDataSourceItem)
        {
            var visualization = new CircularGaugeVisualization(excelDataSourceItem)
            {
                Title = "Line 1 Efficiency",
                ColumnSpan = 20,
                RowSpan = 19
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Line").First() as TextField;
            field.DataFilter = new TextFilter()
            {
                FilterType = FilterType.SelectedValues,
                SelectedValues = new System.Collections.Generic.List<FilterValue>()
                {
                    new FilterValue() { Name = "Line 1", Value = "Line 1"}
                }
            };

            visualization.Settings.Minimum = new Bound() { Value = 0.0, ValueType = BoundValueType.NumberValue };
            visualization.Settings.Maximum = new Bound() { Value = 1.0, ValueType = BoundValueType.NumberValue };
            visualization.Settings.UpperBand.Value = 100.0;
            visualization.Settings.MiddleBand.Value = 0.0;

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Efficiency")
                {
                    AggregationType = AggregationType.Max,
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Percent,
                        DecimalDigits = 2,
                        ShowGroupingSeparator = false,
                        LargeNumberFormat = LargeNumberFormat.Auto
                    }
                }
            });

            visualization.Label = new DimensionColumn()
            {
                DataField = new TextDataField("Line")
            };

            return visualization;
        }

        private static Visualization CreateColumnChartVisualization2(DataSourceItem excelDataSourceItem)
        {
            var visualization = new ColumnChartVisualization(excelDataSourceItem)
            {
                Title = "Orders In vs Orders Shipped",
                ColumnSpan = 60,
                RowSpan = 32
            };

            var field = visualization.DataDefinition.AsTabular().Fields.Where(x => x.FieldName == "Date").First() as DateField;
            field.DataFilter = new DateTimeFilter(DateFilterRule.Last(1, PeriodType.Year))
            {
                FilterType = FilterType.FilterByRule,
                DateFiscalYearStartMonth = 0,
                DisplayInLocalTimeZone = false
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Orders In")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Orders Shipped ")
            });

            return visualization;
        }
    }
}
