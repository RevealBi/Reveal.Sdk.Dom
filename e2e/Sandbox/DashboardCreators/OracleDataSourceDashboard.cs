using Reveal.Sdk.Data.MySql;
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
using static Antlr4.Runtime.Atn.SemanticContext;

namespace Sandbox.DashboardCreators
{
    public class OracleDataSourceDashboard : IDashboardCreator
    {
        public string Name => "Oracle Data Source";

        public RdashDocument CreateDashboard()
        {
            var oracleDataSource = new OracleDataSource
            {
                Id = "oracleSIDId",
                Title = "Oracle SID DS",
                Subtitle = "Oracle SID Datasource",
                Host = "revealdb01.infragistics.local",
                Database = "HR",
                SID = "orcl",
                Port = 1521
            };

            var oracleDataSourceItem = new OracleDataSourceItem("employees report to ID", oracleDataSource)
            {
                Id = "oracleSIDDSItemId",
                Title = "Oracle SID DSItem",
                Database = "HR",
                Table = "EMPLOYEES",
                Fields = new List<IField>
                {
                    new NumberField("MANAGER_ID"),
                    new NumberField("EMPLOYEE_ID"),
                }
            };

            var document = new RdashDocument()
            {
                Title = "Oracle",
                Description = "Example for Oracle",
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            document.Visualizations.Add(CreateEmployeeReportColumnVisualization(oracleDataSourceItem));

            return document;
        }

        private static Visualization CreateEmployeeReportColumnVisualization(DataSourceItem dsi, params DashboardFilter[] filters)
        {
            return new ColumnChartVisualization("Employees report", dsi)
                .SetLabel("MANAGER_ID")
                .SetValue("EMPLOYEE_ID")
                .ConnectDashboardFilters(filters)
                .SetPosition(20, 11);
        }
    }
}
