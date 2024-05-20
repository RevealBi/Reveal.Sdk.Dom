using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Data
{
    public class DataSource : SchemaType, IEquatable<DataSource>
    {
        private string _id = Guid.NewGuid().ToString();

        public DataSource()
        {
            SchemaTypeName = SchemaTypeNames.DataSourceType;
            Properties = new Dictionary<string, object>();
        }

        public string Id
        {
            get => _id;
            set => _id = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value; //do not allow a null Id
        }

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataSourceProvider Provider { get; set; } //needs to be public to enable serialization of custom datasources

        [JsonProperty("Description")]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string DefaultRefreshRate
        {
            get => Properties.GetValue<string>("DefaultRefreshRate");
            set => Properties.SetItem("DefaultRefreshRate", value);
        }

        [JsonProperty]
        public Dictionary<string, object> Properties { get; set; } //needs to be public to enable serialization of custom datasources

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
            hashCode = hashCode * -1521134295 + EqualityComparer<DataSourceProvider>.Default.GetHashCode(Provider);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subtitle);
            return hashCode;
        }
    }
}
