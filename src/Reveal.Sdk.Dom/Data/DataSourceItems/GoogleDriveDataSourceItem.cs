using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    /// <summary>
    /// DO NOT MAKE PUBLIC. WE ARE NOT SUPPORTING THIS YET.
    /// </summary>
    internal class GoogleDriveDataSourceItem : DataSourceItem
    {
        public GoogleDriveDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public string Identifier
        {
            get => Properties.GetValue<string>("Identifier");
            set => Properties.SetItem("Identifier", value);
        }
    }
}
