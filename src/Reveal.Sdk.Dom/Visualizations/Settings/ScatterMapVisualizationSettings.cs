using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class ScatterMapVisualizationSettings : MapVisualizationSettingsBase
    {
        public ScatterMapVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.ScatterMapVisualizationSettingsType;
            VisualizationType = VisualizationTypes.SCATTER_MAP;
        }

        /// <summary>
        /// Gets the styling rules for each range of data - upper, middle, and lower. Only applies when <see cref="ColorMode"/> is set to <see cref="MapColorMode.ConditionalFormatting"/>.
        /// </summary>
        [JsonProperty]
        public MapConditionalFormatting ConditionalFormatting { get; internal set; } = new MapConditionalFormatting();

        /// <summary>
        /// Gets or sets how the markers will be colored. Only applied when setting colors by value.
        /// </summary>
        [JsonProperty("ColorizationMode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MapColorMode ColorMode { get; set; } = MapColorMode.RangeOfValues;

        /// <summary>
        /// Gets or sets the zoom level at which image tiles will appear.
        /// </summary>
        [JsonProperty("ZoomThreshold")]
        public int ImageTileZoomLevel { get; set; } = 3;

        /// <summary>
        /// Gets or sets whether to show image tiles from one of the supported image tile providers. Image tile providers include Bing, ESRI, and MapBox.
        /// </summary>
        [JsonProperty("ShowTileSource")]
        public bool ShowImageTiles { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to automatically create a unique marker for each category by using combinations between colors and symbols such as squares, triangles, dots, stars, octagons, etc.
        /// Only applied when setting colors by category.
        /// </summary>
        [JsonProperty]
        public bool UseDifferentMarkers { get; set; }

        /// <summary>
        /// Gets the zoom and position of the map. While its size is 0x0, it isn't saved and Reveal shows the whole map.
        /// </summary>
        [JsonProperty("ZoomRectangle")]
        public MapZoomRectangle Zoom { get; internal set; } = new MapZoomRectangle();

        // Reveal draws a 0x0 zoom area as an empty map; leaving it out shows the whole map.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeZoom() => Zoom != null && (Zoom.DegreesLongitude != 0 || Zoom.DegreesLatitude != 0);
    }
}
