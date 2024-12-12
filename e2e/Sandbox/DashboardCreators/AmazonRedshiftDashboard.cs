using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DashboardFactories
{
    internal class AmazonRedshiftDashboard : IDashboardCreator
    {
        public string Name => "Amazon Redshift Data Source";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument();
            var dataSource = new AmazonRedshiftDataSource() 
            {
                Title = "Redshift DS",
                Host = "reveal-redshift.cmeyu4xjvffl.us-east-1.redshift.amazonaws.com",
                Database = "reveal"
            };
            var dataSourceItem = new AmazonRedshiftDataSourceItem("Redshift DSI", dataSource)
            {
                Id = "redshiftDSItemId",
                Title = "Redshift DS Item",
                Table = "employees",
                Fields = new List<IField>
                {
                    new NumberField("employeeid"),
                    new TextField("firstname"),
                    new TextField("lastname"),
                    new TextField("address"),
                }
            };

            document.Visualizations.Add(new GridVisualization("List employees", dataSourceItem)
                .SetColumns("employeeid", "firstname", "lastname", "address"));

            return document;
        }
    }
}
