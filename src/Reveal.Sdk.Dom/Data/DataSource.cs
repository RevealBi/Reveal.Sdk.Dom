﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using System;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Data
{
    public sealed class DataSource : SchemaType, IEquatable<DataSource>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public DataSourceProvider Provider { get; internal set; }

        [JsonProperty("Description")]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        
        public Dictionary<string, object> Properties { get; internal set; }

        public DataSource()
        {
            SchemaTypeName = SchemaTypeNames.DataSourceType;
            Properties = new Dictionary<string, object>();
        }

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
