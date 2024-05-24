﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class TabularColumn : ColumnBase
    {
        internal TabularColumn() : this(string.Empty) { }
        public TabularColumn(string fieldName)
        {
            SchemaTypeName = SchemaTypeNames.TabularColumnSpecType;
            FieldName = fieldName;
        }
        
        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the sorting type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SortingType Sorting { get; set; } = SortingType.None;
    }
}
