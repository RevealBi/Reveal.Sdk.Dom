using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DateTimeField : FieldBase<DateTimeFilter>
    {
        /// <summary>Date-field display and fiscal-calendar settings.</summary>
        public DateTimeFieldSettings Settings { get; set; }

        /// <summary>Date and time format of the field's values.</summary>
        public DateFormatting Formatting { get; set; }

        internal DateTimeField() : this(string.Empty) { }
        public DateTimeField(string fieldName) : base(fieldName)
        {
            ((IFieldDataType)this).DataType = DataType.DateTime;
        }
    }
}
