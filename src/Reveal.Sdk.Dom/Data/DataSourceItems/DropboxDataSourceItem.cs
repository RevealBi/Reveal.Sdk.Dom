using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    //DO NOT IMPLEMENT
    //Some data sources are used in Slingshot, but not the SDK. We have decided not to implement
    //this data sourec in the SDK which means we do not need to support it in the DOM.
    //Leaving the class here just in case we need to implement it in the future.
    internal class DropboxDataSourceItem : DataSourceItem
    {
        public DropboxDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string Path
        {
            get => Properties.GetValue<string>("Path");
            set => Properties.SetItem("Path", value);
        }
    }
}
