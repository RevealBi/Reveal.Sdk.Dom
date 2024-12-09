using Reveal.Sdk.Data.MySql;
using Reveal.Sdk.Data.Rest;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.DashboardFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DashboardCreators
{
    public class WebServiceDataSourceDashboard : IDashboardCreator
    {
        public string Name => "Web Service Data Source";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument()
            {
                Title = "Webservice DS",
                Description = "Example for Webservice",
                UseAutoLayout = false,
            };

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", new DataSource { Title = "JSON DS", Subtitle = "JSON DS Subtitle" })
            {
                Id = "webServiceItemId",
                Title = "Sales by Category",
                Subtitle = "Excel2Json",
                Fields = new List<IField>
                {
                    new NumberField("CategoryID"),
                    new TextField("CategoryName"),
                    new TextField("ProductName"),
                    new NumberField("ProductSales"),
                }
            };

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            var countryFilter = new DashboardDataFilter("Country", dataSourceItems);
            document.Filters.Add(countryFilter);

            document.Visualizations.Add(
                            new ColumnChartVisualization("Test List", dataSourceItems)
                                .SetLabel("CategoryName")
                                .SetValue("ProductName")
                                );
            return document;
        }
    }
}
