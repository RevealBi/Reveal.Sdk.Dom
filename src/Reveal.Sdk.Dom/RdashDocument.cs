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
using System;
using Reveal.Sdk.Dom.Core;
using System.IO;
using Reveal.Sdk.Dom.Core.Utilities;

namespace Reveal.Sdk.Dom
{
    public sealed class RdashDocument : IAIMetadata
    {
        private VisualizationCollection _visualizations;

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
            _visualizations = new VisualizationCollection(this);
        }

        /// <summary>
        /// Gets or sets the AI metadata for the dashboard. This is not meant for public use.
        /// </summary>
        [JsonProperty("AI")]
        AIProperties IAIMetadata.AIProperties { get; set; }

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
        public VisualizationCollection Visualizations
        {
            get => _visualizations;
            internal set
            {
                _visualizations = value;
                _visualizations.Parent = this;
            }
        }

        /// <summary>
        /// Import all visualizations from another document.
        /// </summary>
        /// <param name="document">The <see cref="RdashDocument"/> containing the visualizations to import.</param>
        /// <param name="options">The import options.</param>
        /// <exception cref="ArgumentNullException">Thrown if the document is null</exception>
        public void Import(RdashDocument document, ImportOptions options = null)
        {
            if (document == null)
                throw new ArgumentNullException();

            RdashDocumentImporter.Import(this, document, null, options);
        }

        /// <summary>
        /// Import a visualization from another document.
        /// </summary>
        /// <param name="document">The <see cref="RdashDocument"/> containing the visualization to import.</param>
        /// <param name="visualizationId">The ID of the visualization to import.</param>
        /// <param name="options">The import options.</param>
        /// <exception cref="ArgumentNullException">Thrown if the document is null</exception>
        /// <exception cref="ArgumentException">Thrown if the visualizationId is null or empty.</exception>
        public void Import(RdashDocument document, string visualizationId, ImportOptions options = null)
        {
            if (document == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(visualizationId))
                throw new ArgumentException("Value cannot be null or empty.", nameof(visualizationId));

            var visualization = document.Visualizations.Find(v => v.Id == visualizationId);
            if (visualization != null)
                RdashDocumentImporter.Import(this, document, visualization, options);

        }

        /// <summary>
        /// Import a visualization from another document.
        /// </summary>
        /// <param name="document">The <see cref="RdashDocument"/> containing the visualization to import.</param>
        /// <param name="visualization">The visualization.</param>
        /// <param name="options">The import options.</param>
        /// <exception cref="ArgumentNullException">Thrown if the document or visualization is null.</exception>
        public void Import(RdashDocument document, IVisualization visualization, ImportOptions options = null)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (visualization == null)
                throw new ArgumentNullException(nameof(visualization));

            RdashDocumentImporter.Import(this, document, visualization, options);
        }

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
        /// Creates an <see cref="RdashDocument"/> from a .rdash file stream.
        /// </summary>
        /// <param name="stream">The dashboard file stream</param>
        /// <returns></returns>
        public static RdashDocument Load(Stream stream)
        {
            return RdashSerializer.Load(stream);
        }

        /// <summary>
        /// Creates an <see cref="RdashDocument"/> from a JSON string.
        /// </summary>
        /// <param name="json">A JSON string representing the .rdash file.</param>
        /// <returns></returns>
        public static RdashDocument LoadFromJson(string json)
        {
            return RdashSerializer.Deserialize(json);
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
            return RdashSerializer.SerializeDocument(this);
        }

        /// <summary>
        /// Validates the <see cref="RdashDocument"/> to ensure it's in a valid state for serialization.
        /// </summary>
        public void Validate()
        {
            RdashDocumentValidator.Validate(this);
        }
    }
}
