namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDataFilterBinding : Binding<DashboardDataFilterBindingTarget>
    {
        internal DashboardDataFilterBinding() { }

        public DashboardDataFilterBinding(DashboardDataFilter dataFilter)
            : this(dataFilter, dataFilter.FieldName) { }

        public DashboardDataFilterBinding(DashboardDataFilter dataFilter, string fieldName)
        {
            Source = new FieldBindingSource() { FieldName = fieldName };
            Operator = BindingOperatorType.Equals;
            Target = new DashboardDataFilterBindingTarget()
            {
                DashboardFilterId = dataFilter.Id,
                DashboardFilterFieldName = dataFilter.FieldName
            };
        }
    }
}
