using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    /// <summary>
    /// Specifies where a URL link opens.
    /// </summary>
    public enum UrlLinkTarget
    {
        [EnumMember(Value = "Blank")]
        NewTab,

        [EnumMember(Value = "Self")]
        SameTab
    }
}
