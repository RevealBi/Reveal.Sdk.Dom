using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class NumberField : FieldBase<NumberFilter>
    {
        internal NumberField() : this(string.Empty) { }
        public NumberField(string fieldName) : base(fieldName)
        {
            ((IFieldDataType)this).DataType = DataType.Number;
        }

        public NumberFormatting Formatting { get; set; }
    }
}
