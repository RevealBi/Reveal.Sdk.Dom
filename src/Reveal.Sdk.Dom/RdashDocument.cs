using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Variables;
using Newtonsoft.Json.Converters;

namespace Reveal.Sdk.Dom
{
    public sealed class RdashDocument
    {
        /// <summary>
        /// Creates a new instance of an <see cref="RdashDocument"/>.
        /// </summary>
        public RdashDocument() : this("New Dashboard") { }

        /// <summary>
        /// Creates a new instance of an <see cref="RdashDocument"/>.
        /// </summary>
        /// <param name="title">The title of the dashboard.</param>
        public RdashDocument(string title)
        {
            Title = title;
        }

        /// <summary>
        /// Gets or sets the title of the dashboard.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the dashboard.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the theme the dashboard will apply to all visualizations.
        /// </summary>
        [JsonProperty("ThemeName")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Theme Theme { get; set; } = Theme.Mountain;

        /// <summary>
        /// Gets the name of the API that created the .rdash file.
        /// </summary>
        [JsonProperty]
        public string CreatedWith { get; private set; } = GlobalConstants.RdashDocument.CreatedWith;

        /// <summary>
        /// Gets the name of the API that last saved the .rdash file.
        /// </summary>
        [JsonProperty]
        public string SavedWith { get; internal set; } = string.Empty;

        [JsonProperty]
        internal int FormatVersion { get; set; } = 6;

        /// <summary>
        /// Gets or sets whether the viewer displaying the dashboard will automatically layout visualizations, or use an absolute layout controlled by each visualization's ColumnSpan and RowSpan properties. True by default.
        /// </summary>
        public bool UseAutoLayout { get; set; } = true;

        [JsonProperty]
        internal string Tags { get; set; }

        /// <summary>
        /// Gets the collection of data sources available for creating visualizations.
        /// </summary>
        [JsonProperty]
        public List<DataSource> DataSources { get; internal set; } = new List<DataSource>();

        /// <summary>
        /// Gets the collection of dashboard filters that can bound to any visualization using the visualization's FilterBindings property.
        /// </summary>
        [JsonProperty("GlobalFilters")]
        public List<DashboardFilter> Filters { get; internal set; } = new List<DashboardFilter>();

        /// <summary>
        /// TODO: implement
        /// </summary>
        [JsonProperty]
        internal List<GlobalVariable> GlobalVariables { get; set; } = new List<GlobalVariable>();

        /// <summary>
        /// Gets the collection of visualizations that are displayed in the dashboard.
        /// </summary>
        [JsonProperty("Widgets")]
        public List<IVisualization> Visualizations { get; internal set; } = new List<IVisualization>();

        /// <summary>
        /// Creates an <see cref="RdashDocument"/> from a .rdash file.
        /// </summary>
        /// <param name="filePath">The file path to the dashboard file (.rdash).</param>
        /// <returns>The <see cref="RdashDocument"/> representing the contents of the loaded .rdash file.</returns>
        public static RdashDocument Load(string filePath)
        {
            return RdashSerializer.Load(filePath);
        }

        /// <summary>
        /// Saves the <see cref="RdashDocument"/> as a .rdash file.
        /// </summary>
        /// <param name="filePath">The file path to save the <see cref="RdashDocument"/> (must include the .rdash extensions).</param>
        public void Save(string filePath)
        {
            RdashSerializer.Save(this, filePath);            
        }

        /// <summary>
        /// Converts the <see cref="RdashDocument"/> to a JSON string.
        /// </summary>
        /// <returns>A JSON string representing the <see cref="RdashDocument"/></returns>
        public string ToJsonString()
        {
            return RdashSerializer.Serialize(this);
        }
    }
}
