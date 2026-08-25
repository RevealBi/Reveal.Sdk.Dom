using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public class UrlLink : VisualizationLinkBase
    {
        public UrlLink()
        {
            Type = LinkType.OpenUrl;
        }

        public UrlLink(string title, string url) : this(title, url, UrlLinkTarget.NewTab) { }

        public UrlLink(string title, string url, UrlLinkTarget target) : this()
        {
            Title = title;
            Url = url;
            Target = target;
        }

        /// <summary>
        /// The URL of the link.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Where the URL will be opened.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public UrlLinkTarget Target { get; set; } = UrlLinkTarget.NewTab;
    }
}
