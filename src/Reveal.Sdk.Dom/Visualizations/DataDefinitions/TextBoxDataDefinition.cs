﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class TextBoxDataDefinition : DataDefinitionBase
    {
        public TextBoxDataDefinition()
        {
            SchemaTypeName = SchemaTypeNames.TextBoxDataSpecType;
        }

        public string Text { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FontSize FontSize { get; set; } = FontSize.Medium;

        [JsonConverter(typeof(StringEnumConverter))]
        public Alignment Alignment { get; set; } = Alignment.Left;
    }
}
