using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;

namespace Sandbox.DashboardFactories
{
    internal class MSAzureSynapseAnalyticsDashboard : IDashboardCreator
    {
        public string Name => "MS Azure Synapse Analytics Data Source";

        public RdashDocument CreateDashboard()
        {
            var document = new RdashDocument("Azure Synapse Analytics Dashboard");

            var dataSource = new MicrosoftAzureSynapseAnalyticsDataSource()
            {
                Id = "azureSynapseDSId",
                Title = "Synapse data source title",
                Host = "revealdb01.infragistics.local",
                Database = "Northwind",
                Port = 1433,
                TrustServerCertificate = false
            };
            var dataSourceItem = new MicrosoftAzureSynapseAnalyticsDataSourceItem("MS Azure Synapse DS Item", dataSource)
            {
                Id = "azureSynapseDSItemId",
                Title = "Synapse DS item title",
                Database = dataSource.Database,
                Table = "Categories",
                Fields = new List<IField>
                {
                    new NumberField("CategoryID"),
                    new TextField("CategoryName"),
                    new TextField("Description")
                }
            };

            document.Visualizations.Add(new GridVisualization("List Categories", dataSourceItem)
                .SetColumns("CategoryID", "CategoryName", "Description"));

            return document;
        }
    }
}
