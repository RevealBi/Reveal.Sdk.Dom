using Microsoft.AnalysisServices.AdomdClient;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using System.IO;

namespace Sandbox.DashboardFactories
{
    internal class GoogleAnalytic4Dashboard : IDashboardCreator
    {
        public string Name => "Google Analytic4 Data Source";

        public RdashDocument CreateDashboard()
        {
            var ga4DS = new GoogleAnalytics4DataSource()
            {
                Title = "Google Analytics 4",
                Subtitle = "Google Analytics 4 Subtitle",
                AccountId = "392932",
                PropertyId = "338246105",
                PropertyName = "RevealBI.io - GA4"
            };

            var gs4DSI = new GoogleAnalytics4DataSourceItem("GA4 DataSource Item", ga4DS)
            {
                Fields = new List<IField>
                {
                    new DateTimeField("Date")
                }
            };

            var visualization = new ColumnChartVisualization("Column Chart", gs4DSI)
            {
                Title = "View per page"
            };

            visualization.DataDefinition.AsXmla().DataFilters.Add(new XmlaHierarchy()
            {
                XmlaFilter = new XmlaFilter()
                {
                    Filter = new XmlaDateFilter() {
                        RuleType = DateRuleType.LastMonth,
                        FilterType = FilterType.FilterByRule,
                    }
                }
            });

            visualization.Labels.Add(new DimensionColumn()
            {
                XmlaElement = new XmlaHierarchy()
                {
                    UniqueName = "unifiedScreenName",
                    Caption = "Page title and screen name",
                    DimensionUniqueName = "Page / Screen",
                    DimensionType = XmlaDimensionType.Regular,
                    DateAggregationType = DateAggregationType.Year,
                    DateFiscalYearStartMonth = 0,
                    Description = "The page title (web) or screen name (app) on which the event was logged."
                }
            });

            visualization.Values.Add(new MeasureColumn()
            {
                XmlaMeasure = new XmlaMeasure()
                {
                    UniqueName = "screenPageViewsPerSession",
                    Caption = "Views per session",
                    MeasureGroupName = "Page / Screen",
                    Formatting = new NumberFormatting()
                    {
                        FormatType = NumberFormattingType.Number,
                        DecimalDigits = 0,
                        ShowGroupingSeparator = true,
                        NegativeFormat = NegativeFormatType.MinusSign
                    }
                }
            });

            var document = new RdashDocument("My Dashboard");

            document.Visualizations.Add(visualization);

            var jsonData = document.ToJsonString();

            var filePath = "D:\\Dropbox\\Ohio\\infragistic\\my_test\\nodejs-js\\myDashboards\\testga4.rdash";

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                document.Save(filePath);
            }
            catch
            {
                throw;
            }
            return document;
        }
    }
}
