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
            var dataSource = new WebServiceDataSource()
            {
                Id = "webServiceId",
                Title = "Web Data Source",
                Subtitle = "Web Data Source Subtitle",
                Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9",
                UseAnonymousAuthentication = true,
            };

            var dataSourceItems = new WebServiceDataSourceItem("DB Test", dataSource)
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

            var document = new RdashDocument()
            {
                Title = "Webservice DS",
                Description = "Example for Webservice",
                UseAutoLayout = false,
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
