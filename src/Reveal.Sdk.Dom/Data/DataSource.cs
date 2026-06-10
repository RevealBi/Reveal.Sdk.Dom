using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Data
{
    /// <summary>
    /// Represents a data source in Reveal. A data source contains the necessary information for Reveal to connect to and retrieve data from a specific provider, such as a database or an API.
    /// </summary>
    public class DataSource : SchemaType, IEquatable<DataSource>
    {
        private string _id = Guid.NewGuid().ToString();

        
        public DataSource()
        {
            SchemaTypeName = SchemaTypeNames.DataSourceType;
            Properties = new Dictionary<string, object>();
            Settings = new Dictionary<string, object>();
        }

        /// <summary>
        /// The unique identifier for the data source.
        /// </summary>
        public string Id
        {
            get => _id;
            set => _id = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value; //do not allow a null Id
        }


        /// <summary>
        /// The provider used by this data source.
        /// </summary>
        [JsonProperty]
        public string Provider { get; set; }

        /// <summary>
        /// The title of the data source. Used as the display name for the data source in the UI.
        /// </summary>
        [JsonProperty("Description")]
        public string Title { get; set; }

        /// <summary>
        /// The subtitle of the data source.
        /// </summary>
        public string Subtitle { get; set; }

        [JsonIgnore]
        /// <summary>
        /// The default refresh rate for the cached data accessed by this data source.
        /// </summary>
        public string DefaultRefreshRate
        {
            get => Settings.GetValue<string>("DefaultRefreshRate");
            set => Settings.SetItem("DefaultRefreshRate", value);
        }

        /// <summary>
        /// A dictionary of additional properties specific to the data source provider.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, object> Properties { get; set; }

        /// <summary> 
        /// A dictionary of additional settings for the data source.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, object> Settings { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DataSource);
        }

        public bool Equals(DataSource other)
        {
            return other != null && other.Id == Id;
        }

        public override int GetHashCode()
        {
            int hashCode = -258580624;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SchemaTypeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Provider);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subtitle);
            return hashCode;
        }
    }
}
