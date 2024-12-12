using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.DashboardFactories;
using Sandbox.Helpers;

namespace Sandbox.DashboardCreators
{
    public class WebServiceDataSourceDashboard : IDashboardCreator
    {
        public string Name => "Web Service Data Source";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("My Dashboard");

            //json - default
            var jsonDataSourceItem = new WebServiceDataSourceItem("Sales by Category", new DataSource { Title = "JSON DS", Subtitle = "JSON DS Subtitle" })
            {
                Subtitle = "JSON Data Source Item",
                Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetSalesByCategoryFields(),
            };

            document.Visualizations.Add(new PieChartVisualization("JSON", jsonDataSourceItem)
                .SetLabel("CategoryName").SetValue("ProductSales"));

            //excel
            var excelDataSourceItem = new WebServiceDataSourceItem("Marketing",
                new DataSource { Title = "Excel DS", Subtitle = "Excel DS Subtitle" })
            {
                Subtitle = "Excel Data Source Item",
                Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx",
                IsAnonymous = true,
                Fields = DataSourceFactory.GetMarketingDataSourceFields(),
            };
            excelDataSourceItem.UseExcel("Marketing");

            document.Visualizations.Add(new PieChartVisualization("Excel", excelDataSourceItem)
                .SetLabel("Territory").SetValue("Conversions"));

            var csvDataSourceItem = new WebServiceDataSourceItem("Illinois School Info", new DataSource() { Title = "CSV DS", Subtitle = "CSV DS Subtitle" })
            {
                Subtitle = "CSV Data Source Item",
                IsAnonymous = true,
                Url = "https://query.data.world/s/y32gtgblzpemyyvtig47dz7tedgkto",
                Fields = DataSourceFactory.GetCsvDataSourceFields(),
            };
            csvDataSourceItem.UseCsv();

            document.Visualizations.Add(new ScatterMapVisualization("Scatter", csvDataSourceItem)
                .SetMap(Maps.NorthAmerica.UnitedStates.States.Illinois)
                .SetLongitude("X")
                .SetLatitude("Y")
                .SetLabel("School_Nm")
                .ConfigureSettings(settings =>
                {
                    settings.Zoom.Longitude = 1.38;
                    settings.Zoom.Latitude = 41.65;
                    settings.Zoom.DegreesLongitude = 1.04;
                    settings.Zoom.DegreesLatitude = 0.39;
                }));

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            return document;
        }
    }
}
