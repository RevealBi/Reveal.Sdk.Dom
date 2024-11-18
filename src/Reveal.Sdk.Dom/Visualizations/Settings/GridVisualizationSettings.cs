using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations.Settings
{
    public sealed class GridVisualizationSettings : GridVisualizationSettingsBase
    {
        public GridVisualizationSettings()
        {
            SchemaTypeName = SchemaTypeNames.GridVisualizationSettingsType;
            VisualizationType = VisualizationTypes.GRID;
        }

        /// <summary>
        /// Gets or sets whether the grid should have paging enabled. 
        /// Paging is supported only when <c>ProcessDataOnServer</c> is set to <c>true</c> and is not compatible with Data Blending.
        /// Supported data sources include: Athena, BigQuery, MySQL, Oracle, PostgreSQL, SQL Server, and SyBase.
        /// </summary>
        [JsonProperty("PagedRows")]
        public bool IsPagingEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the number of rows to display per page.
        /// </summary>
        [JsonProperty("PagedRowsSize")]
        public int PageSize { get; set; } = 50;

        /// <summary>
        /// Gets or sets whether the first column in the grid is fixed.
        /// </summary>
        [JsonIgnore()]
        public bool IsFirstColumnFixed
        {
            get { return Style.FixedLeftColumns; }
            set { Style.FixedLeftColumns = value; }
        }
    }
}
