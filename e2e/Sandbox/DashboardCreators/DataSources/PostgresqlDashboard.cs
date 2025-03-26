﻿using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    internal class PostgresqlDashboard : IDashboardCreator
    {
        public string Name => "PostgresQL Data Source";

        public RdashDocument CreateDashboard()
        {
            DataSource _postgresDataSource = new PostgreSQLDataSource()
            {
                Id = "Postgres",
                Title = "Postgres Data Source",
                Subtitle = "The Data Source for Postgres",
                Host = "revealdb01.infragistics.local",
                Database = "northwind",
                Port = 5432
            };

            var postgresDataSourceItem = new PostgreSqlDataSourceItem("Employees", _postgresDataSource)
            {
                Title = "Postgres Employee",
                Subtitle = "Postgres DS Item for Employee",
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
                Title = "PostgreSQL",
                Description = "Example for Postgresql",
                UseAutoLayout = false,
            };

            var dateFilter = new DashboardDateFilter("My Date Filter");
            document.Filters.Add(dateFilter);

            var countryFilter = new DashboardDataFilter("Country", postgresDataSourceItem);
            document.Filters.Add(countryFilter);

            document.Visualizations.Add(new ColumnChartVisualization("Employees report", postgresDataSourceItem)
                .SetLabel("ReportsTo")
                .SetValue("EmployeeID")
                .ConnectDashboardFilters(countryFilter)
                .SetPosition(20, 11));

            return document;
        }
    }
}
