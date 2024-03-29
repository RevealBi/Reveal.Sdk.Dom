﻿using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class DataField : SchemaType, IDataField
    {
        protected DataField(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <inheritdoc/>
        public string FieldName { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }
    }
}
