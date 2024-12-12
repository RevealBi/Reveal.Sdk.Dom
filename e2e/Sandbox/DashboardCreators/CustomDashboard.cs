using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;

namespace Sandbox.DashboardFactories
{
    internal class CustomDashboard : IDashboardCreator
    {
        public string Name => "Custom";

        public RdashDocument CreateDashboard()
        {
            var factory = new DataSourceItemFactory();

            var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
            {
                Subtitle = "Excel Data Source Item",
                Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetMarketingDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Marketing");

            var csvDataSourceItem = new RestDataSourceItem("Illinois School Info")
            {
                Subtitle = "CSV Data Source Item",
                Url = "https://query.data.world/s/y32gtgblzpemyyvtig47dz7tedgkto",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetCsvDataSourceFields(),
            };
            csvDataSourceItem.UseCsv();

            var financialDataSourceItem = new RestDataSourceItem("OHLC")
            {
                Subtitle = "Financial Data Source Item",
                Url = "https://excel2json.io/api/share/8bb2cd78-1b87-4142-00a2-08da188ec9ab",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetOHLCDataSourceFields(),
            };

            var revenueDataSourceItem = new RestDataSourceItem("Revenue")
            {
                Subtitle = "Choropleth Data Source Item",
                Url = "https://excel2json.io/api/share/818e7b9a-f463-4565-435d-08da496bf5f2",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetRevenueDataSourceFields(),
            };

            var document = new RdashDocument()
            {
                Title = "Custom Dashboard",
                Description = "Playing with the Fluent API",
                Theme = Theme.TropicalIsland
            };

            //grid
            document.Visualizations.Add(new GridVisualization("Grid", excelDataSourceItem)
                .SetColumns("Territory", "Conversions", "Spend"));

            //text view
            document.Visualizations.Add(new TextViewVisualization("TextView", excelDataSourceItem)
                .SetColumns("Territory", "Conversions", "Spend"));

            //pivot
            document.Visualizations.Add(new PivotVisualization("Pivot", excelDataSourceItem)
                .SetRow("Territory").SetValue("New Seats").SetColumn("CampaignID")
                .ConfigureSettings(settings =>
                {
                    settings.ShowGrandTotals = true;
                }));

            //column
            document.Visualizations.Add(new ColumnChartVisualization("Column", excelDataSourceItem)
                .SetLabel("Date").SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //bar
            document.Visualizations.Add(new BarChartVisualization("Bar", excelDataSourceItem)
                .SetLabel("Date").SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //pie
            document.Visualizations.Add(new PieChartVisualization("Pie", excelDataSourceItem)
                .SetLabel("Territory").SetValue("Conversions")
                .ConfigureSettings(settings =>
                {
                    settings.SliceLabelDisplay = LabelDisplayMode.Value;
                }));

            //doughnut
            document.Visualizations.Add(new DoughnutChartVisualization("Doughnut", excelDataSourceItem)
                .SetLabel("Territory").SetValue("Conversions")
                .ConfigureSettings(settings =>
                {
                    settings.SliceLabelDisplay = LabelDisplayMode.ValueAndPercentage;
                    settings.StartPosition = 90;
                }));

            //funnel
            document.Visualizations.Add(new FunnelChartVisualization("Funnel", excelDataSourceItem)
                .SetLabel("Territory")
                .SetValue("Conversions")
                .ConfigureSettings(settings =>
                {
                    settings.SliceLabelDisplay = LabelDisplayMode.Percentage;
                }));

            //combo
            document.Visualizations.Add(new ComboChartVisualization("Combo", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetChart1Value("Spend")
                .SetChart2Value("Budget")
                .ConfigureSettings(settings =>
                {
                    settings.Chart1Type = ComboChartType.Column;
                    settings.Chart2Type = ComboChartType.Line;
                    settings.ShowRightAxis = false;
                    settings.StartColorIndex = 5;
                }));

            //stacked column
            document.Visualizations.Add(new StackedColumnChartVisualization("Stacked Column", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //stacked bar
            document.Visualizations.Add(new StackedBarChartVisualization("Stacked Bar", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //stacked area
            document.Visualizations.Add(new StackedAreaChartVisualization("Stacked Area", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //area
            document.Visualizations.Add(new AreaChartVisualization("Area", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //line
            document.Visualizations.Add(new LineChartVisualization("Line", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValue("Conversions"));

            //step area
            document.Visualizations.Add(new StepAreaChartVisualization("Step Area", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Spend", "Budget"));

            //step line
            document.Visualizations.Add(new StepLineChartVisualization("Step Line", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Spend", "Budget"));

            //spline area
            document.Visualizations.Add(new SplineAreaChartVisualization("Spline Area", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Spend", "Budget"));

            //spline
            document.Visualizations.Add(new SplineChartVisualization("Spline", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Spend", "Budget"));

            //linear gauge
            document.Visualizations.Add(new LinearGaugeVisualization("Linear", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month }).SetValue("Spend")
                .ConfigureSettings(settings =>
                {
                    settings.ValueComparisonType = ValueComparisonType.Number;
                    settings.UpperBand.Value = 10000;
                    settings.MiddleBand.Value = 5000;
                }));

            //circular gauge
            document.Visualizations.Add(new CircularGaugeVisualization("Circular", excelDataSourceItem).SetLabel("Budget").SetValue("Spend")
                .ConfigureSettings(settings =>
                {
                    settings.MiddleBand.Value = 30;
                }));

            //text
            document.Visualizations.Add(new TextVisualization("Text", excelDataSourceItem).SetLabel("Budget").SetValue("Spend")
                .ConfigureSettings(settings =>
                {
                    settings.ConditionalFormattingEnabled = true;
                    settings.UpperBand.Shape = ShapeType.ArrowUp;
                    settings.MiddleBand.Shape = ShapeType.Dash;
                    settings.LowerBand.Shape = ShapeType.ArrowDown;
                }));

            //kpi target
            document.Visualizations.Add(new KpiTargetVisualization("KPI vs Target", excelDataSourceItem).SetDate("Date").SetValue("Spend").SetTarget("Budget")
                .ConfigureSettings(settings =>
                {
                    settings.DifferenceMode = IndicatorDifferenceMode.ValueAndPercentage;
                    settings.GoalPeriod = KpiGoalPeriod.ThisYear;
                }));

            //kpi time
            document.Visualizations.Add(new KpiTimeVisualization("KPI vs Time", excelDataSourceItem).SetDate("Date").SetValue("Traffic")
                .ConfigureSettings(settings =>
                {
                    settings.DifferenceMode = IndicatorDifferenceMode.ValueAndPercentage;
                    settings.TimePeriod = KpiTimePeriod.MonthToDatePreviousMonth;
                }));

            //bullet graph
            document.Visualizations.Add(new BulletGraphVisualization("Bullet Graph", excelDataSourceItem).SetLabel("CampaignID").SetValue("Spend").SetTarget("Budget")
                .ConfigureSettings(setting =>
                {
                    setting.ValueComparisonType = ValueComparisonType.Number;
                    setting.UpperBand.Value = 72000;
                    setting.MiddleBand.Value = 65000;
                }));

            //choropleth map
            document.Visualizations.Add(new ChoroplethVisualization("Choropleth", revenueDataSourceItem)
                .SetMap(Maps.NorthAmerica.UnitedStates.States.AllStates)
                .SetLocation("State")
                .SetValue("Revenue")
                .ConfigureSettings(settings =>
                {
                    settings.ColorIndex = 5;
                    settings.ColorStyle = MapColorStyle.SingleColor;
                    settings.LabelStyle = MapLabelStyle.Values;
                }));

            //scatter map
            document.Visualizations.Add(new ScatterMapVisualization("Scatter Map", csvDataSourceItem)
                .SetMap(Maps.NorthAmerica.UnitedStates.States.Illinois)
                .SetLongitude("X")
                .SetLatitude("Y")
                .SetLabel("School_Nm")
                .SetColorByValue(new NumberDataField("Grades") { AggregationType = AggregationType.Avg })             
                .ConfigureSettings(settings =>
                {
                    settings.ColorIndex = 5;
                    settings.ColorMode = MapColorMode.ConditionalFormatting;
                    
                    settings.ConditionalFormatting.ValueComparisonType = ValueComparisonType.Percentage;
                    settings.ConditionalFormatting.UpperBand.Value = 90;
                    settings.ConditionalFormatting.MiddleBand.Value = 60;

                    settings.Zoom.Longitude = 1.38;
                    settings.Zoom.Latitude = 41.65;
                    settings.Zoom.DegreesLongitude = 1.04;
                    settings.Zoom.DegreesLatitude = 0.39;
                }));

            //tree map
            document.Visualizations.Add(new TreeMapVisualization("Tree Map", excelDataSourceItem)
                .SetLabel("Territory").SetValue("Traffic"));

            //bubble
            document.Visualizations.Add(new BubbleVisualization("Bubble", excelDataSourceItem)
                .SetLabel("CampaignID").SetXAxis("Budget").SetYAxis("Spend").SetRadius("Traffic"));

            //scatter
            document.Visualizations.Add(new ScatterVisualization("Scatter", excelDataSourceItem)
                .SetLabel("CampaignID").SetXAxis("Budget").SetYAxis("Spend"));

            //time series
            document.Visualizations.Add(new TimeSeriesVisualization("Time Series", excelDataSourceItem)
                .SetDate(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Paid Traffic", "Organic Traffic", "Other Traffic"));

            //radial
            document.Visualizations.Add(new RadialVisualization("Radial", excelDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Month })
                .SetValues("Spend", "Budget"));

            //image
            document.Visualizations.Add(new ImageVisualization("Image", excelDataSourceItem).SetUrl("Territory"));

            //sparkline
            document.Visualizations.Add(new SparklineVisualization("Sparkline", excelDataSourceItem)
                .SetDate("Date")
                .SetValue("Spend")
                .SetCategory("Territory")
                .ConfigureSettings(settings =>
                {
                    settings.ShowLastTwoValues = false;
                    settings.NumberOfPeriods = 10;
                    settings.AggregationType = SparklineAggregationType.Months;
                }));

            //textbox
            document.Visualizations.Add(new TextBoxVisualization("TextBox").SetText("This is some text").SetFontSize(FontSize.Large));

            //custom
            document.Visualizations.Add(new CustomVisualization("Custom", excelDataSourceItem)
                .SetUrl("https://dl.infragistics.com/reportplus/diy/HelloWorld-Desktop-EN.html")
                .SetRows("Territory", "CampaignID")
                .SetValues("Spend", "Budget"));

            //OHLC
            document.Visualizations.Add(new OHLCVisualization("OHLC", financialDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Day })
                .SetOpen("Open")
                .SetHigh("High")
                .SetLow("Low")
                .SetClose("Close"));

            //Candle stick
            document.Visualizations.Add(new CandleStickVisualization("Candlestick", financialDataSourceItem)
                .SetLabel(new DateDataField("Date") { AggregationType = DateAggregationType.Day })
                .SetOpen("Open")
                .SetHigh("High")
                .SetLow("Low")
                .SetClose("Close"));

            return document;
        }
    }
}