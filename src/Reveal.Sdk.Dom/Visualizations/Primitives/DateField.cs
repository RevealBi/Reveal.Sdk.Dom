using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DateField : FieldBase<DateTimeFilter>
    {
        /// <summary>Date-field display and fiscal-calendar settings.</summary>
        public DateTimeFieldSettings Settings { get; set; }

        /// <summary>Date format of the field's values.</summary>
        public DateFormatting Formatting { get; set; }

        internal DateField() : this(string.Empty) { }
        public DateField(string fieldName) : base(fieldName)
        {
            ((IFieldDataType)this).DataType = DataType.Date;
        }
    }
}
