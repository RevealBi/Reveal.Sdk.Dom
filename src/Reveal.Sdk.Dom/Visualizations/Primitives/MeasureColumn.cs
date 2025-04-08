using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class MeasureColumn : ColumnBase
    {
        NumberDataField _dataField;

        public MeasureColumn()
        {
            SchemaTypeName = SchemaTypeNames.MeasureColumnSpecType;
        }
        
        public MeasureColumn(NumberDataField dataField) : this()
        {            
            DataField = dataField;
        }
        
        /// <summary>
        /// Gets or sets the <see cref="DataField"/>.
        /// </summary>
        [JsonProperty("SummarizationField")]
        public NumberDataField DataField
        {
            get => _dataField;
            set
            {
                _dataField = value;
                SyncAxisFromField();
            }
        }

        [JsonProperty("XmlaMeasure")]
        public XmlaMeasure XmlaMeasure { get; set; }

        protected override IDataField GetDataField()
        {
            return DataField;
        }
    }
}