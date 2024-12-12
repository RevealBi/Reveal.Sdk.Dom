using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MicrosoftAnalysisServicesDataSourceItem : DataSourceItem
    {
        public MicrosoftAnalysisServicesDataSourceItem(string title) 
            : base(title, new MicrosoftAnalysisServicesDataSource()) 
        { }

        public  MicrosoftAnalysisServicesDataSourceItem(string title, MicrosoftAnalysisServicesDataSource dataSource) :
            base(title, dataSource)
        { }

        public MicrosoftAnalysisServicesDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string Catalog
        {
            get => Properties.GetValue<string>("Catalog");
            set => Properties.SetItem("Catalog", value);
        }

        [JsonIgnore]
        public string Cube
        {
            get => Properties.GetValue<string>("Cube");
            set => Properties.SetItem("Cube", value);
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<MicrosoftAnalysisServicesDataSource>(dataSource);
        }

        protected override void InitializeDataSourceItem(string title)
        {
            base.InitializeDataSourceItem(title);
            HasTabularData = false;
        }
    }
}
