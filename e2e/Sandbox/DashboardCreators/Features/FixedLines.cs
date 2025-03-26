﻿using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.DashboardFactories;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DashboardCreators
{
    internal class FixedLines : IDashboardCreator
    {
        public string Name { get; } = "Fixed Lines";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = new RestDataSourceItem("Marketing Sheet")
            {
                Subtitle = "Excel Data Source Item",
                Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetMarketingDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Marketing");

            var document = new RdashDocument()
            {
                Title = "Fixed Lines",
                Description = "Playing with the Fixed Lines API"
            };

            document.Visualizations.Add(new ColumnChartVisualization("Column", excelDataSourceItem)
                .SetLabel("Date").SetValues("Paid Traffic")
                .AddFixedLine(new FixedLineAverage("My Title"))
                .AddFixedLine(new FixedLineMaximum())
                .AddFixedLine(new FixedLineData("Spend")
                {
                    LineStyle = ChartLineStyle.Dashed,
                })
                .AddFixedLine(new FixedLineMinimum()));

            return document;
        }
    }
}
