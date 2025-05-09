using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    /// <summary>
    /// DO NOT MAKE PUBLIC. WE ARE NOT SUPPORTING THIS YET.
    /// </summary>
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
