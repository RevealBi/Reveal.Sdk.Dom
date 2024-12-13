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
                Id = "ga4Ds1",
                Title = "Google Analytics 4",
                Subtitle = "Query your data",
                AccountId = "392932",
                PropertyId = "325972604",
                PropertyName = "z[STAGING] Roll-up: Infragistics Web Properties - GA4"
            };

            var gs4DSI = new GoogleAnalytics4DataSourceItem("GA4 DataSource Item", ga4DS)
            {
                Id = "ga4Dsi1",
                Title = "z[STAGING] Roll-up: Infragistics Web Properties - GA4",
                Subtitle = "Infragistics Inc.",
                AccountId = "392932",
                PropertyId = "325972604",
                Fields = new List<IField>
                {
                    new TextField("Campaign ID")
                }
            };


            var visualization = new PivotVisualization("ABC grid", gs4DSI);

            var document = new RdashDocument("My Dashboard");
            document.DataSources.Add(ga4DS);
            //document.Visualizations.Add(grid);
            //return document;


            visualization.DataDefinition.AsXmla().DataFilters.Add(new XmlaHierarchy()
            {
                UniqueName = "date",
                Caption = "Date",
                XmlaFilter = new XmlaFilter()
                {
                    Filter = new XmlaDateFilter()
                    {
                        RuleType = DateRuleType.LastMonth,
                        FilterType = FilterType.FilterByRule,
                        SelectedValues = new List<FilterValue>(),
                        IncludeToday = true,
                    },
                    DataType = DataType.String,
                    ElementType = XmlaElementType.Dimension,
                    IsDynamic = false
                },
                DimensionType = XmlaDimensionType.Date,
                DrillDownElements = new List<string>(),
                Sorting = SortingType.None,
                FieldSortingByLabel = false,
                FullyExpandedLevels = 0,
                ExpandedItems = new List<string>(),
                DateAggregationType = DateAggregationType.Year,
                DateFiscalYearStartMonth = 0,

            });

            visualization.Rows.Add(new DimensionColumn()
            {
                XmlaElement = new XmlaHierarchy()
                {
                    UniqueName = "campaignId",
                    Caption = "Campaign ID",
                    DimensionUniqueName = "Attribution",
                    DimensionType = XmlaDimensionType.Regular,
                    DateAggregationType = DateAggregationType.Year,
                    DateFiscalYearStartMonth = 0,
                    Description = "The page title (web) or screen name (app) on which the event was logged."
                }
            });

            //visualization.Values.Add(new MeasureColumn()
            //{
            //    XmlaMeasure = new XmlaMeasure()
            //    {
            //        UniqueName = "screenPageViewsPerSession",
            //        Caption = "Views per session",
            //        MeasureGroupName = "Page / Screen",
            //        Formatting = new NumberFormatting()
            //        {
            //            FormatType = NumberFormattingType.Number,
            //            DecimalDigits = 0,
            //            ShowGroupingSeparator = true,
            //            NegativeFormat = NegativeFormatType.MinusSign
            //        }
            //    }
            //});



            document.Visualizations.Add(visualization);
            document.DataSources.Add(ga4DS);

            var jsonData = document.ToJsonString();

            var filePath = "E:\\testga4.rdash";

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
