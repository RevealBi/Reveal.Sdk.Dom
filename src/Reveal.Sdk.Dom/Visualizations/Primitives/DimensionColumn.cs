using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DimensionColumn : ColumnBase
    {
        IDimensionDataField _dataField;

        public DimensionColumn()
        {
            SchemaTypeName = SchemaTypeNames.DimensionColumnSpecType;
        }
        
        public DimensionColumn(IDimensionDataField dataField) : this()
        {            
            DataField = dataField;
        }

        /// <summary>
        /// Gets or sets the <see cref="DataField"/>. Choose from the <see cref="DateDataField"/> or the <see cref="TextDataField"/>.
        /// </summary>
        [JsonProperty("SummarizationField")]
        public IDimensionDataField DataField
        {
            get => _dataField;
            set
            {
                _dataField = value;
                SyncAxisFromField();
            }
        }

        [JsonProperty("XmlaElement")]
        public XmlaDimensionElement XmlaElement { get; set; }

        protected override IDataField GetDataField()
        {
            return DataField;
        }
    }
}
