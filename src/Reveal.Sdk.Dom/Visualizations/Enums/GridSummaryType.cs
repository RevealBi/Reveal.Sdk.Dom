using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// The calculation used to summarize a grid column.
    /// </summary>
    public enum GridSummaryType
    {
        [EnumMember(Value = "Min")]
        Minimum,

        [EnumMember(Value = "Max")]
        Maximum,

        Sum,
        Average,
        Count,
        Custom
    }
}
