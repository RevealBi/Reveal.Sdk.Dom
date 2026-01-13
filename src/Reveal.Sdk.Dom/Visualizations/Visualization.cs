using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class Visualization<TSettings> : Visualization, ISettingsProvider<TSettings>
        where TSettings : VisualizationSettings, new()
    {
        protected Visualization(string title, DataSourceItem dataSourceItem) 
            : base(title, dataSourceItem) { }

        [JsonProperty("ActionsModel", Order = 10)]
        public VisualizationLinker Linker { get; set; }

        [JsonProperty("VisualizationSettings", Order = 5)]
        public TSettings Settings { get; internal set; } = new TSettings();
    }

    public abstract class Visualization : IVisualization, IParentDocument, IAIMetadata
    {
        protected Visualization(string title, DataSourceItem dataSourceItem)
        {
            Title = title;
            InitializeDataDefinition(dataSourceItem);
        }

        [JsonProperty(Order = 0)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("CustomBackgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonIgnore]
        public ChartType ChartType { get; protected set; } = ChartType.Unknown;

        [JsonProperty(Order = 1)]
        public string Title { get; set; }

        [JsonProperty(Order = 2)]
        public bool IsTitleVisible { get; set; } = true;

        [JsonProperty(Order = 3)]
        public int ColumnSpan { get; set; }

        [JsonProperty(Order = 4)]
        public int RowSpan { get; set; }

        [JsonIgnore]
        RdashDocument IParentDocument.Document { get; set; }

        [JsonProperty("DataSpec", Order = 6)]
        public IDataDefinition DataDefinition { get; internal set; }

        /// <summary>
        /// Gets or sets the description of the visualization.
        /// </summary>
        public string Description { get; set; }

        [JsonIgnore]
        public List<Binding> FilterBindings
        {
            get { return ((DataDefinitionBase)DataDefinition).Bindings.Bindings; }
            set { ((DataDefinitionBase)DataDefinition).Bindings.Bindings = value; }
        }

        //todo: is it possible to create a Filters property that can properly handle both Tabular and Xmla data specs?
        [JsonIgnore]
        public List<VisualizationFilter> Filters
        {
            get 
            {
                if (DataDefinition is TabularDataDefinition tdd)
                    return tdd.QuickFilters;
                else
                    return new List<VisualizationFilter>(); 
                
                //todo: implement this for Xmla data specs
            }
            set
            {
                if (DataDefinition is TabularDataDefinition tdd)
                    tdd.QuickFilters = value;

                //todo: implement this for Xmla data specs
            }
        }

        /// <summary>
        /// Gets the data source item for the visualization.
        /// </summary>
        /// <returns>The <see cref="DataSourceItem"/></returns>
        public DataSourceItem GetDataSourceItem()
        {
            var dataSourceItem = DataDefinition.DataSourceItem;
            if (DataDefinition is TabularDataDefinition tdd)
            {
                dataSourceItem.Fields = tdd.Fields.Clone();
            }

            var document = ((IParentDocument)this).Document;
            if (document != null)
            {
                dataSourceItem.DataSource = document.DataSources.FirstOrDefault(x => x.Id == dataSourceItem.DataSourceId);
                if (dataSourceItem.ResourceItem != null)
                    dataSourceItem.ResourceItemDataSource = document.DataSources.FirstOrDefault(x => x.Id == dataSourceItem.ResourceItem.DataSourceId);
            }

            return dataSourceItem;
        }

        /// <summary>
        /// Updates the data source item and available fields for the visualization.
        /// </summary>
        /// <param name="dataSourceItem">The <see cref="DataSourceItem"/>.</param>
        public void UpdateDataSourceItem(DataSourceItem dataSourceItem)
        {
            if (DataDefinition == null || dataSourceItem == null)
                return;

            ((DataDefinitionBase)DataDefinition).DataSourceItem = dataSourceItem;
            if (DataDefinition is TabularDataDefinition tdd)
            {
                tdd.Fields = dataSourceItem.Fields.Clone();
                
                if(tdd.DataSourceItem.JoinTables != null && tdd.DataSourceItem.JoinTables.Count > 0)
                {
                    tdd.JoinTables = tdd.DataSourceItem.JoinTables.Clone();
                }
            }
        }

        protected virtual void InitializeDataDefinition(DataSourceItem dataSourceItem)
        {
            if (dataSourceItem != null)
            {
                if (dataSourceItem.HasTabularData)
                {
                    DataDefinition = new TabularDataDefinition();
                }
                else
                {
                    DataDefinition = new XmlaDataDefinition();
                }

                UpdateDataSourceItem(dataSourceItem);
            }
        }

        /// <summary>
        /// Gets or sets the AI metadata for the visualization. This is not meant for public use.
        /// </summary>
        [JsonProperty("AI")]
        AIProperties IAIMetadata.AIProperties { get; set; }
    }
}