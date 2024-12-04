using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class GoogleSheetsDataSourceItem : DataSourceItem
    {
        public GoogleSheetsDataSourceItem(string title, string identifier) :
            base(title, new GoogleSheetsDataSource())
        {
            InitializeResourceItem(title, identifier);
        }

        public GoogleSheetsDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        {
            InitializeResourceItem(title, null);
        }

        [JsonIgnore]
        public bool FirstRowContainsLabels
        {
            get => Parameters.GetValue<bool>("TITLES_IN_FIRST_ROW");
            set => Parameters.SetItem("TITLES_IN_FIRST_ROW", value);
        }

        [JsonIgnore]
        public string NamedRange
        {
            get => Properties.GetValue<string>("NamedRange");
            set => Properties.SetItem("NamedRange", value);
        }

        [JsonIgnore]
        public string PivotTable
        {
            get => Properties.GetValue<string>("PivotTable");
            set => Properties.SetItem("PivotTable", value);
        }

        //todo: this is a nested object
        //[JsonIgnore]
        //public object Range { get; set; }

        [JsonIgnore]
        public string Sheet
        {
            get => Properties.GetValue<string>("Sheet");
            set => Properties.SetItem("Sheet", value);
        }

        [JsonIgnore]
        public string Identifier
        {
            get => ResourceItem.Properties.GetValue<string>("Identifier");
            set => ResourceItem.Properties.SetItem("Identifier", value);
        }

        private void InitializeResourceItem(string title, string identifier)
        {
            ResourceItemDataSource = new GoogleDriveDataSource();
            ResourceItem = new GoogleDriveDataSourceItem(title, ResourceItemDataSource)
            {
                Identifier = identifier
            };
        }
    }
}
