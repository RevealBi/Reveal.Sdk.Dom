using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    public class MSAzureSqlServerDSDashboard : IDashboardCreator
    {
        public string Name => "MS Azure Sql Server";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("MS Azure Sql Dashboard");

            var msAzureSqlDS = new MicrosoftAzureSqlServerDataSource()
            {
                Id = "MSAzureSqlId",
                Title = "Microsoft Azure Sql Server Data Source",
                Subtitle = "MS Azure Sql Server DS Subtitle",
                Database = "reveal",
                DefaultRefreshRate = "180",
                Encrypt = false,
                Host = "revealtesting.database.windows.net",
                Port = 1433,
                TrustServerCertificate = false
            };

            var msAzureSqlDSItem = new MicrosoftAzureSqlServerDataSourceItem("Microsoft Azure Sql Data Source Item", msAzureSqlDS)
            {
                Id = "MSAzureSqlItemId",
                Title = "Microsoft Azure Sql Server Data Source Item",
                Subtitle = "MS Azure Sql Server DSI Subtitle",
                Table = "Customers",
                Fields = new List<IField>()
                {
                    new TextField("City"),
                    new TextField("CustomerID"),
                }
            };

            document.Visualizations.Add(new GridVisualization("Customers in France", msAzureSqlDSItem)
                .SetColumns("CustomerID", "City"));

            return document;
        }
    }
}
