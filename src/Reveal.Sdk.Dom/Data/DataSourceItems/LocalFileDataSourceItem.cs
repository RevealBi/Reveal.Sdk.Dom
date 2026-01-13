using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data.DataSourceItems
{
    //I'm not sure if we should have ExcelFileDataSourceItem separately or just have methods here to configure as Excel
    internal class LocalFileDataSourceItem : DataSourceItem
    {
        public LocalFileDataSourceItem(string title) : this(title, new DataSource())
        { }

        public LocalFileDataSourceItem(string title, string path) : this(title, new DataSource())
        {
            Path = path;
        }

        public LocalFileDataSourceItem(string title, DataSource dataSource) : base(title, dataSource)
        {
            InitializeResourceItem(title);
        }

        [JsonIgnore]
        public string Path
        {
            get { return ResourceItem.Properties.GetValue<string>("URI"); }
            set { ResourceItem.Properties.SetItem("URI", $"local:{value}"); }
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            if (dataSource is LocalFileDataSource)
                return dataSource;

            return new LocalFileDataSource();
        }

        protected void InitializeResourceItem(string title)
        {
            ResourceItemDataSource = new LocalFileDataSource();
            ResourceItem = new DataSourceItem
            {
                DataSource = ResourceItemDataSource,
                DataSourceId = ResourceItemDataSource.Id,
                Title = title
            };
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

        public void UseJson(string configStr = "")
        {
            ClearJsonConfig();
            Parameters.SetItem("config", configStr);
            DataSource = new JsonDataSource();
        }

        public void UseCsv()
        {
            ClearJsonConfig();
            DataSource = new CsvDataSource();
        }

        void ClearJsonConfig()
        {
            if (Parameters.ContainsKey("config"))
                Parameters.Remove("config");
        }
    }
}
