﻿using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class FilterItem
    {
        public Dictionary<string, object> FieldValues { get; set; }
        public List<string> ExpansionPath { get; set; } = new List<string>();
    }
}
