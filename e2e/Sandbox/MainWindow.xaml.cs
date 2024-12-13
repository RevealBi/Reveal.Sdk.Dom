using Reveal.Sdk;
using Reveal.Sdk.Data;
using Reveal.Sdk.Data.Google.Analytics4;
using Reveal.Sdk.Data.Microsoft.SqlServer;
using Reveal.Sdk.Dom;
using Sandbox.DashboardFactories;
using Sandbox.RevealSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Sandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly string _dashboardFilePath = Path.Combine(Environment.CurrentDirectory, "Dashboards");
        static readonly string _readFilePath = Path.Combine(_dashboardFilePath, "Healthcare.rdash");

        List<IDashboardCreator> _dashboardCreators = new List<IDashboardCreator>
        {
            new AmazonAthenaDashboard(),
            new AmazonS3Dashboard(),
            new AmazonRedshiftDashboard(),
            new CampaignsDashboard(),
            new CustomDashboard(),
            new DashboardLinkingDashboard(),
            new GoogleAnalytic4Dashboard(),
            new GoogleBigQueryDashboard(),
            new GoogleDriveDashboard(),
            new GoogleSheetDashboard(),
            new HealthcareDashboard(),
            new HttpAnalysisDashboard(),
            new ManufacturingDashboard(),
            new MarketingDashboard(),
            new MongoDashboard(),
            new MSAnalysisServiceDashboard(),
            new MSAzureAnalysisServiceDashboard(),
            new MSAzureSqlDashboard(),
            new MySqlDashboard(),
            new MSAzureSqlServerDSDashboard(),
            new ODataDashboard(),
            new OracleDashboard(),
            new PostgresqlDashboard(),
            new RestDataSourceDashboard(),
            new SalesDashboard(),
            new SnowflakeDashboard(),
            new SqlServerDataSourceDashboards(),
            new WebServiceDataSourceDashboard(),
        };

        public MainWindow()
        {
            InitializeComponent();

            RevealSdkSettings.DataSourceProvider = new DataSourceProvider();
            RevealSdkSettings.AuthenticationProvider = new AuthenticationProvider();
            RevealSdkSettings.DataSources
                .RegisterMicrosoftSqlServer()
                .RegisterMicrosoftAnalysisServices()
                .RegisterPostgreSQL()
                .RegisterAmazonAthena()
                .RegisterMicrosoftSynapseAnalytics()
                .RegisterSnowflake()
                .RegisterMySql()
                .RegisterMongoDB()
                .RegisterGoogleDrive()
                .RegisterGoogleBigQuery()
                .RegisterGoogleAnalytics4()
                .RegisterAmazonRedshift()
                .RegisterAmazonS3()
                .RegisterGoogleDrive()
                .RegisterOracle()
                .RegisterGoogleAnalytics4();

            LoadDashboards();

            _revealView.LinkedDashboardProvider = (string dashboardId, string linkTitle) =>
            {
                var path = Path.Combine(_dashboardFilePath, $"{dashboardId}.rdash");
                if (File.Exists(path))
                    return new RVDashboard(path);

                return null;
            };

            _revealView.DataSourcesRequested += RevealView_DataSourcesRequested;
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


            var ga4Ds = new RVGoogleAnalytics4DataSource()
            {
                Id = "ga4Ds",
                Title = "Google Analytics 4",
                Subtitle = "Query your data",
                AccountId = "392932",
                PropertyId = "325972604",
            };

            ds.Add(ga4Ds);

            var ga4Dsi = new RVGoogleAnalytics4DataSourceItem(ga4Ds)
            {
                Id = "ga4Dsi",
                Title = "z[STAGING] Roll-up: Infragistics Web Properties - GA4",
                Subtitle = "Infragistics Inc.",
                AccountId = "392932",
                PropertyId = "325972604",
            };
            dsi.Add(ga4Dsi);



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

            e.Callback(new RevealDataSources(ds, dsi, true));
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
            var creator = _dashboardTypeSelector.SelectedItem as IDashboardCreator;
            if (creator != null)
            {
                RdashDocument document = creator.CreateDashboard();

                var json = document.ToJsonString();
                _revealView.Dashboard = await RVDashboard.LoadFromJsonAsync(json);

                // Save the last selected item to application settings
                Properties.Settings.Default.LastSelectedDashboard = creator.Name;
                Properties.Settings.Default.Save();
            }
        }

        private void LoadDashboards()
        {
            _dashboardTypeSelector.ItemsSource = _dashboardCreators;

            // Load the last selected item from application settings
            var lastSelectedName = Properties.Settings.Default.LastSelectedDashboard;
            if (!string.IsNullOrEmpty(lastSelectedName))
            {
                var selectedDashboard = _dashboardCreators.FirstOrDefault(x => x.Name == lastSelectedName);
                _dashboardTypeSelector.SelectedItem = selectedDashboard;
            }
        }
    }
}
