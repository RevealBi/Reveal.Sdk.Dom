using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reveal.Sdk.Data.MongoDB;

namespace Sandbox.Factories
{
    internal class MongoDbDataSourceDashboard
    {
        internal static RdashDocument CreateDashboard()
        {
            var mongoDbDs = new MongoDBDataSource()
            {
                ConnectionString = "mongodb://root:Abc123@localhost:27017"
            };

            var salesCollection = new MongoDbDataSourceItem("Sales Collection", mongoDbDs)
            {
                Collection = "sales",
                Fields = new List<IField>
                {
                    new TextField("storeLocation") { FieldLabel = "Store Location" },
                    new TextField("purchaseMethod") { FieldLabel = "Purchase Method" },
                    new TextField("saleDate") { FieldLabel = "Sale Date" }
                }
            };

            var document = new RdashDocument("My Dashboard");

            document.Visualizations.Add(new GridVisualization("Sales List", salesCollection).SetColumns("Store Location", "Purchase Method", "Sale Date"));

            return document;
        }
    }
}
