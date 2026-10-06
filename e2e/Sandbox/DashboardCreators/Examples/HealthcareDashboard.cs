using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using DataSourceFactory = Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.DashboardFactories
{
    internal class HealthcareDashboard : IDashboardCreator
    {
        public string Name => "Healthcare";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = DataSourceFactory.GetHealthcareDataSourceItem();

            var document = new RdashDocument()
            {
                Title = "Healthcare",
                Theme = Theme.Circus,
                Description = "I created this in code",
                UseAutoLayout = false,
            };

            document.Filters.Add(new DashboardDateFilter(DateFilterRule.Previous(12, PeriodType.Month)));

            var globalDateFilterBinding = new DashboardDateFilterBinding("Date");

            document.Visualizations.Add(CreateIndicatorVisualization("Number of Inpatients", "Number of Inpatients", excelDataSourceItem));
            document.Visualizations.Add(CreateIndicatorVisualization("Number of Outpatients", "Number of Outpatients", excelDataSourceItem));
            document.Visualizations.Add(CreateIndicatorVisualization("Average ER Wait Time (Min)", "ER Wait Time", excelDataSourceItem, true));
            document.Visualizations.Add(CreateIndicatorVisualization("Average Days Stayed", "Length of Stay ", excelDataSourceItem, true));
            document.Visualizations.Add(CreateDoughnutChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateSplineAreaChartVisualization(excelDataSourceItem, globalDateFilterBinding));
            document.Visualizations.Add(CreateFunnelChartVisualization(excelDataSourceItem, globalDateFilterBinding));
            document.Visualizations.Add(CreateStackedColumnChartVisualization(excelDataSourceItem));
            document.Visualizations.Add(CreateLineChartVisualization(excelDataSourceItem, globalDateFilterBinding));

            return document;
        }

        private static Visualization CreateIndicatorVisualization(string title, string field, DataSourceItem excelDataSourceItem, bool avg = false)
        {
            var visualization = new KpiTimeVisualization(excelDataSourceItem)
            {
                Title = title,
                ColumnSpan = 15,
                RowSpan = 13,
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
                    DecimalDigits = 0,
                    ShowGroupingSeparator = true,
                };
            }

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = dataField
            });

            return visualization;
        }

        private static Visualization CreateDoughnutChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new DoughnutChartVisualization(excelDataSourceItem)
            {
                Title = "Current Patients by Division",
                ColumnSpan = 15,
                RowSpan = 23,
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Divison")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Current Paitents")
            });

            return visualization;
        }

        private static Visualization CreateSplineAreaChartVisualization(DataSourceItem excelDataSourceItem, params Binding[] filterBindings)
        {
            var visualization = new SplineAreaChartVisualization(excelDataSourceItem)
            {
                Title = "Treatment Costs vs Charges per MD",
                ColumnSpan = 30,
                RowSpan = 23,
            };

            visualization.FilterBindings.AddRange(filterBindings);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Charges per MD")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Treatment Costs ")
            });

            return visualization;
        }

        private static Visualization CreateFunnelChartVisualization(DataSourceItem excelDataSourceItem, params Binding[] filterBindings)
        {
            var visualization = new FunnelChartVisualization(excelDataSourceItem)
            {
                Title = "Wait Time by Division",
                ColumnSpan = 15,
                RowSpan = 23,
            };

            visualization.FilterBindings.AddRange(filterBindings);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Divison")
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("ER Wait Time")
            });

            return visualization;
        }

        private static Visualization CreateStackedColumnChartVisualization(DataSourceItem excelDataSourceItem)
        {
            var visualization = new StackedColumnChartVisualization(excelDataSourceItem)
            {
                Title = "Satisfaction by Division",
                ColumnSpan = 30,
                RowSpan = 23,
            };

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new TextDataField("Divison")
            });

            visualization.Category = new DimensionColumn()
            {
                DataField = new TextDataField("Satisfaction ")
            };

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Satisfaction ")
                {
                    AggregationType = AggregationType.CountRows,
                    Formatting = new NumberFormatting()
                    {
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                    }
                }
            });

            return visualization;
        }

        private static Visualization CreateLineChartVisualization(DataSourceItem excelDataSourceItem, params Binding[] filterBindings)
        {
            var visualization = new LineChartVisualization(excelDataSourceItem)
            {
                Title = "Inpatients vs Outpatients",
                ColumnSpan = 30,
                RowSpan = 23,
            };

            visualization.FilterBindings.AddRange(filterBindings);

            visualization.Labels.Add(new DimensionColumn()
            {
                DataField = new DateDataField("Date")
                {
                    AggregationType = DateAggregationType.Month
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Number of Inpatients")
            });
            visualization.Values.Add(new MeasureColumn()
            {
                DataField = new NumberDataField("Number of Outpatients")
            });

            return visualization;
        }
    }
}
