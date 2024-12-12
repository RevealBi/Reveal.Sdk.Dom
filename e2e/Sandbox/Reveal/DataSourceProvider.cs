using Reveal.Sdk.Data.Json;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.Rest;
using Reveal.Sdk.Data;
using System.Threading.Tasks;
using Reveal.Sdk.Data.Excel;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.MongoDB;

namespace Sandbox.RevealSDK
{
    public class DataSourceProvider : IRVDataSourceProvider
    {
        public Task<RVDataSourceItem> ChangeDataSourceItemAsync(RVDataSourceItem dataSourceItem)
        {
            //if (dataSourceItem is RVJsonDataSourceItem jsonDataSourceItem)
            //{
            //    var restDataSourceItem = jsonDataSourceItem.ResourceItem as RVRESTDataSourceItem;
            //    var ds = restDataSourceItem.DataSource as RVRESTDataSource;
            //    ds.UseAnonymousAuthentication = true;
            //    ds.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9";
            //}

            //if (dataSourceItem is RVExcelDataSourceItem excelDataSourceItem)
            //{
            //    if (excelDataSourceItem.Id == "MarketingID")
            //    {
            //        excelDataSourceItem.Sheet = "Marketing";
            //    }

            //    var restDataSourceItem = excelDataSourceItem.ResourceItem as RVRESTDataSourceItem;
            //    if (restDataSourceItem != null)
            //    {
            //        if (restDataSourceItem.Id == "MarketingID")
            //        {
            //            excelDataSourceItem.Sheet = "Marketing";
            //        }
            //        var ds = restDataSourceItem.DataSource as RVRESTDataSource;
            //        ds.UseAnonymousAuthentication = true;

            //        if (ds.Id == "RestExcel")
            //        {
            //            ds.Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx";
            //        }
            //    }

            //    var webResourceItem = excelDataSourceItem.ResourceItem as RVWebResourceDataSourceItem;
            //    if (webResourceItem != null)
            //    {
            //        webResourceItem.Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx";
            //    }
            //}

            //if (dataSourceItem is RVSqlServerDataSourceItem sqlServerDataSourceItem)
            //{
            //    sqlServerDataSourceItem.Table = "Customers";
            //    sqlServerDataSourceItem.Database = "Northwind";

            //    var ds = sqlServerDataSourceItem.DataSource as RVSqlServerDataSource;
            //    ds.Host = "Brian-Desktop\\SQLEXPRESS";
            //    ds.Database = "Northwind";
            //}

            //if (dataSourceItem is RVMongoDBDataSourceItem mongoDbDataSourceItem)
            //{
            //    mongoDbDataSourceItem.Collection = "data";

            //    var ds = mongoDbDataSourceItem.DataSource as RVMongoDBDataSource;

            //    ds.ConnectionString = "mongodb+srv://user01:*******@cluster0.ta2xrrt.mongodb.net/";
            //    ds.Database = "test";
            //}

            return Task.FromResult(dataSourceItem);
        }
    }
}
