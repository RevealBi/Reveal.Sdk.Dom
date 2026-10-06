using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DateField : FieldBase<DateTimeFilter>
    {
        /// <summary>Date-field display and fiscal-calendar settings.</summary>
        public DateTimeFieldSettings Settings { get; set; }

        internal DateField() : this(string.Empty) { }
        public DateField(string fieldName) : base(fieldName)
        {
            ((IFieldDataType)this).DataType = DataType.Date;
        }
    }
}
