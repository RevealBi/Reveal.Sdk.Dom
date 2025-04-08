using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using System.Runtime.Serialization;

namespace Reveal.Sdk.Dom.Visualizations
{
    public abstract class ColumnBase : SchemaType
    {
        [JsonProperty("Axis")]
        internal ColumnAxis Axis { get; set; }

        //because the Axis does not exikst on the field directly, we have to implementsome kind of hack to make this work
        //unfortunately this will currently only work when creating new object instance and not when simply changing the axis title
        //need to figure out a better way to do this
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            SyncFieldFromAxis();
        }

        protected void SyncAxisFromField()
        {
            var field = GetDataField();
            if (field != null)
            {
                if (!string.IsNullOrEmpty(field.AxisTitle))
                {
                    if (Axis == null)
                        Axis = new ColumnAxis();
                    
                    Axis.Title = field.AxisTitle;
                }
            }
        }

        private void SyncFieldFromAxis()
        {
            var field = GetDataField();
            if (field != null && Axis != null)
            {
                field.AxisTitle = Axis.Title;
            }
        }

        protected abstract IDataField? GetDataField();
    }
}
