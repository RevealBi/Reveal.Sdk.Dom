using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Amazon.Athena;
using Reveal.Sdk.Data.Amazon.Redshift;
using Reveal.Sdk.Data.Amazon.S3;
using Reveal.Sdk.Data.Box;
using Reveal.Sdk.Data.Dropbox;
using Reveal.Sdk.Data.Excel;
using Reveal.Sdk.Data.Google.Analytics4;
using Reveal.Sdk.Data.Google.BigQuery;
using Reveal.Sdk.Data.Google.Drive;
using Reveal.Sdk.Data.Json;
using Reveal.Sdk.Data.Microsoft.AnalysisServices;
using Reveal.Sdk.Data.Microsoft.OneDrive;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Data.Microsoft.SynapseAnalytics;
using Reveal.Sdk.Data.MongoDB;
using Reveal.Sdk.Data.MySql;
using Reveal.Sdk.Data.OData;
using Reveal.Sdk.Data.Oracle;
using Reveal.Sdk.Data.PostgreSQL;
using Reveal.Sdk.Data.Rest;
using Reveal.Sdk.Data.Snowflake;
using Reveal.Sdk.Dom;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using Sandbox.Factories;
using Sandbox.Helpers;
using Sandbox.RevealSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.Management.Deployment;
using Windows.Storage.Streams;

namespace Sandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string _dashboardFilePath = Path.Combine(Environment.CurrentDirectory, "Dashboards");

        //readonly string _readFilePath = Path.Combine(_dashboardFilePath, DashboardFileNames.Sales);
        readonly string _readFilePath = Path.Combine(_dashboardFilePath, "Healthcare.rdash");

        readonly string _saveJsonToPath = Path.Combine(_dashboardFilePath, "MyDashboard.json");
        readonly string _saveRdashToPath = Path.Combine(_dashboardFilePath, DashboardFileNames.MyDashboard);

        public MainWindow()
        {
            InitializeComponent();

            RevealSdkSettings.DataSourceProvider = new Sandbox.RevealSDK.DataSourceProvider();
            RevealSdkSettings.AuthenticationProvider = new AuthenticationProvider();
            RevealSdkSettings.DataSources.RegisterMicrosoftSqlServer().RegisterMicrosoftAnalysisServices();
            _dashboardTypeSelector.ItemsSource = Enum.GetValues(typeof(DataSourceType)).Cast<DataSourceType>();

            _revealView.LinkedDashboardProvider = (string dashboardId, string linkTitle) =>
            {
                var path = Path.Combine(_dashboardFilePath, $"{dashboardId}.rdash");
                if (File.Exists(path))
                    return new RVDashboard(path);

                return null;
            };

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;

            _revealView.DashboardSelectorRequested += RevealView_DashboardSelectorRequested;
        }

        private void RevealView_DataSourcesRequested(object sender, DataSourcesRequestedEventArgs e)
        {
            var ds = new List<RVDashboardDataSource>();
            var dsi = new List<RVDataSourceItem>();

            //REST
            //var jsonRestDS = new RVRESTDataSource();
            //jsonRestDS.Title = "REST DS JSON";
            //jsonRestDS.Subtitle = "REST DS JSON Subtitle";
            //jsonRestDS.UseAnonymousAuthentication = true;
            //jsonRestDS.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9";
            //ds.Add(jsonRestDS);

            //REST well defined
            //var jsonRestDS = new RVRESTDataSource();
            //jsonRestDS.Title = "REST DS JSON";
            //jsonRestDS.Subtitle = "REST DS JSON Subtitle";
            //jsonRestDS.UseAnonymousAuthentication = true;
            //jsonRestDS.Url = "https://excel2json.io/api/share/6e0f06b3-72d3-4fec-7984-08da43f56bb9";
            //ds.Add(jsonRestDS);

            //var jsonRestDSI = new RVRESTDataSourceItem(jsonRestDS);
            //jsonRestDSI.Title = "REST DSI JSON";
            //jsonRestDSI.Subtitle = "REST JSON DSI Subtitle";

            //var jsonDSI = new RVJsonDataSourceItem(jsonRestDSI);
            //jsonDSI.Title = "JSON DSI";
            //jsonDSI.Subtitle = "JSON DSI Subtitle";
            //jsonDSI.Config = @"
            //{
            //    ""iterationDepth"": 0,
            //    ""columnsConfig"": [
            //        {
            //            ""key"": ""CategoryID"",
            //            ""type"": 1
            //        },
            //        {
            //            ""key"": ""CategoryName"",
            //            ""type"": 0
            //        },
            //        {
            //            ""key"": ""ProductName"",
            //            ""type"": 0
            //        },
            //        {
            //            ""key"": ""ProductSales"",
            //            ""type"": 1
            //        }
            //    ]
            //}";
            //dsi.Add(jsonDSI);

            //REST Excel well defined
            //var restExcelDS = new RVRESTDataSource();
            //restExcelDS.Title = "REST Excel";
            //restExcelDS.Subtitle = "Samples.xlsz";
            //restExcelDS.UseAnonymousAuthentication = true;
            //restExcelDS.Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx";
            //ds.Add(restExcelDS);

            //var restDSI = new RVRESTDataSourceItem(restExcelDS);
            //restDSI.Title = "REST Data Source Item";
            //restDSI.Subtitle = "REST DSI Subtitle";

            //var excelDSI = new RVExcelDataSourceItem(restDSI);
            //excelDSI.Title = "Excel Data Source Item";
            //excelDSI.Subtitle = "Marketing Sheet";
            //excelDSI.Sheet = "Marketing";
            //dsi.Add(excelDSI);

            var sqlDS = new RVSqlServerDataSource();
            sqlDS.Title = "SQL Server Data Source";
            sqlDS.Subtitle = "SQL Server DS Subtitle";
            sqlDS.Host = "Brian-Desktop\\SQLEXPRESS";
            sqlDS.Database = "Northwind"; //this is required
            ds.Add(sqlDS);

            var sqlDSI = new RVSqlServerDataSourceItem(sqlDS);
            sqlDSI.Title = "SQL Server Data Source Item";
            sqlDSI.Subtitle = "SQL Server DSI Subtitle";
            sqlDSI.Table = "Customers";
            dsi.Add(sqlDSI);

            //var webDS = new RVWebResourceDataSource();
            //webDS.UseAnonymousAuthentication = true;
            //webDS.Title = "Web Resource";
            //webDS.Url = "http://dl.infragistics.com/reportplus/reveal/samples/Samples.xlsx";
            //ds.Add(webDS);

            //var http = new RVHttpAnalysisServicesDataSource();
            //http.Title = "HTTP Analysis Services";
            //http.Subtitle = "HTTP Analysis Services Subtitle";
            //http.Url = "http://revealdb01.infragistics.local/OLAP/msmdpump.dll";
            //http.Catalog = "Adventure Works DW 2008R2";
            //ds.Add(http);

            //var httpItem = new RVAnalysisServicesDataSourceItem(http);
            //httpItem.Title = "HTTP Analysis Services Item";
            //httpItem.Subtitle = "HTTP Analysis Services Item Subtitle";
            //httpItem.Cube = "Adventure Works";
            //dsi.Add(httpItem);

            var mongoDS = new RVMongoDBDataSource();
            mongoDS.Id = "MyMongoDatasource";
            mongoDS.Title = "MongoDB Data Source";
            mongoDS.Subtitle = "MongoDB DS Subtitle";
            mongoDS.ConnectionString = "mongodb+srv://user01:DustFillingABlack4Times@cluster0.ta2xrrt.mongodb.net/";
            mongoDS.Database = "test"; //this is required
            ds.Add(mongoDS);

            var mongoDSItem = new RVMongoDBDataSourceItem(mongoDS);
            mongoDSItem.Id = "MyMongoDatasourceItem";
            mongoDSItem.Title = "MongoDB Data Source Item";
            mongoDSItem.Subtitle = "MongoDBDSI Subtitle";
            mongoDSItem.Collection = "data";
            dsi.Add(mongoDSItem);

            e.Callback(new RevealDataSources(ds, dsi, true));
        }

        private void RevealView_DashboardSelectorRequested(object sender, DashboardSelectorRequestedEventArgs e)
        {
            e.Callback("Campaigns");
        }

        private async void RevealView_SaveDashboard(object sender, DashboardSaveEventArgs e)
        {
            //var json = _revealView.Dashboard.ExportToJson();
            var path = Path.Combine(Environment.CurrentDirectory, $"Dashboards/{e.Name}.rdash");
            var data = await e.Serialize();
            var json = _revealView.Dashboard.ExportToJson();
            using (var output = File.Open(path, FileMode.OpenOrCreate))
            {
                output.Write(data, 0, data.Length);
            }

            e.SaveFinished();
        }

        private void Load_Dashboard(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard = new RVDashboard(_readFilePath);
        }

        private void Clear_Dashboard(object sender, RoutedEventArgs e)
        {
            _revealView.Dashboard = new RVDashboard();
        }

        private async void Read_Dashboard(object sender, RoutedEventArgs e)
        {
            var document = RdashDocument.Load(_readFilePath);            

            var json = document.ToJsonString();
            _revealView.Dashboard = await RVDashboard.LoadFromJsonAsync(json);
        }

        private async void CreateDashboardWithTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedDSTypeItem = _dashboardTypeSelector.SelectedItem;
            if (selectedDSTypeItem != null)
            {
                var dataSourceType = _dashboardTypeSelector.SelectedItem;
                RdashDocument document = null;
                switch (dataSourceType)
                {
                    case DataSourceType.REST:
                        document = SalesDashboard.CreateDashboard();
                        break;
                    case DataSourceType.MicrosoftSqlServer:
                        break;
                    default:
                        break;
                }

                if (document != null)
                {
                    var json = document.ToJsonString();
                    _revealView.Dashboard = await RVDashboard.LoadFromJsonAsync(json);
                }
                else
                {
                    _revealView.Dashboard = new RVDashboard();
                }
            }
        }
    }
}
