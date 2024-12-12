using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Data
{
    public class WebServiceDataSourceItem : DataSourceItem
    {
        DataSource _dataSource;
        public WebServiceDataSourceItem(string title) :
            this(title, new DataSource())
        { }

        public WebServiceDataSourceItem(string title, string url) :
            this(title, url, new DataSource())
        { }

        public WebServiceDataSourceItem(string title, string url, DataSource dataSource) :
            this(title, dataSource)
        {
            Url = url;
        }

        public WebServiceDataSourceItem(string title, DataSource dataSource) :
            base(title, new JsonDataSource())
        {
            _dataSource = dataSource ?? new DataSource();
            InitializeResourceItem(title);
            UpdateResourceItemDataSource(_dataSource);
        }

        [JsonIgnore]
        public string Url
        {
            get { return ResourceItem.Properties.GetValue<string>("Url"); }
            set
            {
                ResourceItem.Properties.SetItem("Url", value);
                ResourceItemDataSource.Properties.SetItem("Url", value);
            }
        }

        [JsonIgnore]
        public bool IsAnonymous
        {
            get { return ResourceItemDataSource.Properties.GetValue<bool>("_rpUseAnonymousAuthentication"); }
            set { ResourceItemDataSource.Properties.SetItem("_rpUseAnonymousAuthentication", value); }
        }

        public void UseCsv()
        {
            ClearJsonConfig();
            DataSource = new CsvDataSource();
            ResourceItemDataSource.Properties.SetItem("Result-Type", ".csv");
        }

        public void UseExcel(string sheet = null, ExcelFileType fileType = ExcelFileType.Xlsx)
        {
            ClearJsonConfig();
            DataSource = new ExcelDataSource();

            var fileExt = fileType == ExcelFileType.Xlsx ? ".xlsx" : ".xls";
            ResourceItemDataSource.Properties.SetItem("Result-Type", fileExt);

            if (sheet != null)
                Properties.SetItem("Sheet", sheet);
        }

        protected void InitializeResourceItem(string title)
        {
            ResourceItemDataSource = new DataSource { Provider = DataSourceProvider.WebService };
            ResourceItem = new DataSourceItem
            {
                DataSource = ResourceItemDataSource,
                DataSourceId = ResourceItemDataSource.Id,
                Title = title
            };

            ResourceItemDataSource = ResourceItemDataSource;
            ResourceItem = ResourceItem;
        }



        protected override void OnFieldsPropertyChanged(List<IField> fields)
        {
            Parameters.Add("config", BuildConfig(fields));
        }

        private Dictionary<string, object> BuildConfig(IEnumerable<IField> fields)
        {
            Dictionary<string, object> config = new Dictionary<string, object>();
            List<ColumnConfig> columnConfigs = new List<ColumnConfig>();
            foreach (var field in fields)
            {
                if (field == null)
                    continue;

                var columnConfig = new ColumnConfig
                {
                    Key = field.FieldName
                };


                int type = ((IFieldDataType)field).DataType switch
                {
                    DataType.Number => 1,
                    DataType.Date => 2,
                    DataType.DateTime => 3,
                    DataType.Time => 4,
                    _ => 0
                };

                columnConfig.Type = type;
                columnConfigs.Add(columnConfig);
            }

            config.Add("iterationDepth", 0);
            config.Add("columnsConfig", columnConfigs);
            return config;
        }

        void ClearJsonConfig()
        {
            if (Parameters.ContainsKey("config"))
                Parameters.Remove("config");
        }
    }
}
