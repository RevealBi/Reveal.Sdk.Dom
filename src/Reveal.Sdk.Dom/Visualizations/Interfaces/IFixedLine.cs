﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Serialization.Converters;

namespace Reveal.Sdk.Dom.Visualizations
{
    [JsonConverter(typeof(FixedLineConverter))]
    public interface IFixedLine
    {
        string Color { get; set; }
        NumberFormatting Formatting { get; set; }
        LineStyle LineStyle { get; set; }
        double Thickness { get; set; }
        string Title { get; set; }
    }
}
