namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilterBinding : Binding<DashboardDateFilterBindingTarget>
    {
        internal DashboardDateFilterBinding() : this(string.Empty) { }

        public DashboardDateFilterBinding(string fieldName)
        {
            Operator = BindingOperatorType.Between;
            Source = new FieldBindingSource() { FieldName = fieldName };
            Target = new DashboardDateFilterBindingTarget();
        }
    }
}
