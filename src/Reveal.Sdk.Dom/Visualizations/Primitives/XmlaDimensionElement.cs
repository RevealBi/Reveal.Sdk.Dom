using Reveal.Sdk.Dom.Filters;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Serialization.Converters;
using Reveal.Sdk.Dom.Core;

namespace Reveal.Sdk.Dom.Visualizations
{
    [JsonConverter(typeof(XmlaDimensionElementConverter))]
    public abstract class XmlaDimensionElement : SchemaType
    {
        public XmlaDimensionElement() { }

        public string UniqueName { get; set; }
        public string Caption { get; set; }
        public string UserCaption { get; set; }
        public string DefaultHierarchy { get; set; }
        public string DimensionUniqueName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public XmlaDimensionType DimensionType { get; set; } = XmlaDimensionType.Regular;
        public List<string> DrillDownElements { get; set; } = new List<string>();
        [JsonConverter(typeof(StringEnumConverter))]
        public SortingType Sorting { get; set; } = SortingType.None;
        public bool FieldSortingByLabel { get; set; }
        public XmlaFilter XmlaFilter { get; set; }
        public int FullyExpandedLevels { get; set; }
        public List<string> ExpandedItems { get; set; } = new List<string>();
        [JsonConverter(typeof(StringEnumConverter))]
        public DateAggregationType DateAggregationType { get; set; } = DateAggregationType.Year;
        public int DateFiscalYearStartMonth { get; set; }
        public List<XmlaMember> DrillDownMembers { get; set; } = new List<XmlaMember>();
    }
}
