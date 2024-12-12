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
    public class MySqlDataSourceDashboards : IDashboardCreator
    {
        public string Name => "MySql Data Source";

        public RdashDocument CreateDashboard()
        {
            var mysqlDS = new MySqlDataSource
            {
                Id = "mysqlDS",
                Title = "MySQL DS",
                Subtitle = "My SQL Datasource",
                Host = "revealdb01.infragistics.local",
                Database = "northwind",
                Port = 3306,
            };

            var mysqlDSItem = new MySqlDataSourceItem("employees report to ID", mysqlDS)
            {
                Id = "mysqlDSItem",
                Title = "MySQL DSItem",
                Subtitle = "My SQL Datasource order table",
                Database = "northwind",
                Table = "employees",
                Fields = new List<IField>
                {
                    new NumberField("ReportsTo"),
                    new NumberField("EmployeeID"),
                    new TextField("Country"),
                }
            };

            var document = new RdashDocument()
            {
                Title = "MySql",
                Description = "Example for MySql",
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            var countryFilter = new DashboardDataFilter("Country", mysqlDSItem);
            document.Filters.Add(countryFilter);

            document.Visualizations.Add(CreateEmployeeReportColumnVisualization(mysqlDSItem, countryFilter));

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
