using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;

namespace Sandbox.Factories
{
    internal class MongoDbDataSourceDashboard
    {
        internal static RdashDocument CreateDashboard()
        {
            var mongoDbDs = new MongoDBDataSource()
            {
                Id = "MyMongoDatasource",
                Title = "MyMongoDatasource",
                Subtitle = "My MongoDB",
                ProcessDataOnServerDefaultValue = true,
                ProcessDataOnServerReadOnly = false,
                ConnectionString = "mongodb+srv://cluster0.ta2xrrt.mongodb.net",
                Database = "test"
            };

            var testCollection = new MongoDbDataSourceItem("DB Test", mongoDbDs)
            {
                Id = "MyMongoDatasourceItem",
                Title = "MyMongoDatasourceItem",
                Subtitle = "Test Collection",
                Collection = "data",
                Fields = new List<IField>
                {
                    new TextField("_id"),
                    new TextField("name"),
                    new NumberField("price"),
                    new DateTimeField("available_since"),
                    new TextField("category"),
                    new NumberField("year_value"),
                    new NumberField("month_value"),
                    new NumberField("day_value"),
                    new NumberField("hour_value"),
                    new NumberField("minutes_value"),
                    new NumberField("seconds_value"),
                    new NumberField("milliseconds_value"),
                    new NumberField("numeric_value1"),
                    new NumberField("numeric_value2"),
                    new NumberField("numeric_value3"),
                    new TextField("image_url")

                }
            };

            var document = new RdashDocument("My Dashboard");

            document.Visualizations.Add(new GridVisualization("Test List", testCollection).SetColumns("name", "category", "price"));

            var jsonData = document.ToJsonString();

            // var filePath = "test.rdash";

            // try
            // {

            //     if (File.Exists(filePath))
            //         File.Delete(filePath);

            //     document.Save(filePath);
            // }
            // catch
            // {
            //     throw;
            // }
            

            return document;
        }
    }
}
