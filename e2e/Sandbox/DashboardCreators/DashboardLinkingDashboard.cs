using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sandbox.DashboardFactories
{
    internal class DashboardLinkingDashboard : IDashboardCreator
    {
        public string Name => "Dashboard Linking";

        public RdashDocument CreateDashboard()
        {
            var excelDataSourceItem = DataSourceFactory.GetMarketingDataSourceItem();

            var document = new RdashDocument()
            {
                Title = "Marketing",
                Description = "I created this in code",
                Theme = Theme.Mountain,
                UseAutoLayout = true,
            };

            document.Filters.Add(new DashboardDateFilter(DateFilterRule.Last(1, PeriodType.Year)));

            //create the funnel chart
            var funnelViz = new FunnelChartVisualization("Conversions by Campaign", excelDataSourceItem).SetLabel("CampaignID").SetValue("Conversions");

            //get the filter from the target dashboard
            var linkedDoc = RdashDocument.Load(Path.Combine(Environment.CurrentDirectory, "Dashboards/Campaigns.rdash"));
            var filter = linkedDoc.Filters.Where(x => x.Title == "CampaignID").First();

            //create links the hard way
            funnelViz.Linker = new VisualizationLinker()
            {
                Links = new List<IVisualizationLink>()
                {
                    new UrlLink("Open URL", "https://www.brianlagunas.com/[CampaignID]"),
                    new DashboardLink("Open Dashboard", "Campaigns")
                    {
                        Filters = new List<LinkFilter>()
                        {
                            new LinkFilter("Campaigns Filter", filter.Id, filter.Title),
                            new DateLinkFilter(),
                        }
                    },
                }
            };


            //add the viz to the document
            document.Visualizations.Add(funnelViz);


            //create the pivot vizualization
            var pivotViz = new PivotVisualization("New Seats by Campaign ID", excelDataSourceItem).SetRow("CampaignID").SetValues("CTR", "Avg. CPC", "New Seats");

            //create links the easy way
            pivotViz.Linker = new VisualizationLinker()
                .AddUrl("Open URL", "https://www.brianlagunas.com/[CampaignID]")
                .AddDashboard("Open Dashboard", "Campaigns", new LinkFilter("Campaigns Filter", filter.Id, filter.Title), new DateLinkFilter());

            document.Visualizations.Add(pivotViz);

            return document;
        }
    }
}
