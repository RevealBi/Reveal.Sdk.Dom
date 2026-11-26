using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class TimeField : FieldBase<TimeFilter>
    {
        internal TimeField() : this(string.Empty) { }
        public TimeField(string fieldName) : base(fieldName)
        {
            ((IFieldDataType)this).DataType = DataType.Time;
        }

        /// <summary>Time format of the field's values.</summary>
        public DateFormatting Formatting { get; set; }
    }
}
