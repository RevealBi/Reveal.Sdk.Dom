using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sandbox.Helpers.DataSourceFactory;

namespace Sandbox.Factories
{
    public class MySqlDataSourceDashboard
    {
        internal static RdashDocument CreateDashboard()
        {
            var mysqlDataSourceItem = MySQLDataSourceFactory.GetEmployeeDSItem();

            var document = new RdashDocument()
            {
                Title = "MySql",
                Description = "Example for MySql",
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            var countryFilter = new DashboardDataFilter("Country", mysqlDataSourceItem);
            document.Filters.Add(countryFilter);

            document.Visualizations.Add(CreateEmployeeReportColumnVisualization(mysqlDataSourceItem, countryFilter));

            return document;
        }

        private static Visualization CreateEmployeeReportColumnVisualization(DataSourceItem mysql, params DashboardFilter[] filters)
        {
            return new ColumnChartVisualization("Employees report", mysql)
                .SetLabel("ReportsTo")
                .SetValue("EmployeeID")
                .ConnectDashboardFilters(filters)
                .SetPosition(20, 11);
        }
    }
}
