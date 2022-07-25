﻿using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class FieldBindingSource : BindingSource
    {
        public string FieldName { get; set; }

        public FieldBindingSource()
        {
            SchemaTypeName = SchemaTypeNames.FieldBindingSourceType;
        }
    }
}
