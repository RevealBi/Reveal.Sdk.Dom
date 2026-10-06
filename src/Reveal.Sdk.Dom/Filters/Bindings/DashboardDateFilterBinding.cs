using Newtonsoft.Json;
using System;

namespace Reveal.Sdk.Dom.Filters
{
    public sealed class DashboardDateFilterBinding : Binding<DashboardDateFilterBindingTarget>
    {
        [JsonConstructor]
        internal DashboardDateFilterBinding()
        {
            Operator = BindingOperatorType.Between;
            Source = new FieldBindingSource();
            Target = new DashboardDateFilterBindingTarget();
        }

        /// <summary>Connects a visualization field to the specified dashboard date filter.</summary>
        public DashboardDateFilterBinding(DashboardDateFilter dateFilter, string fieldName = "Date") : this()
        {
            if (dateFilter == null)
                throw new ArgumentNullException(nameof(dateFilter));

            Source = new FieldBindingSource() { FieldName = fieldName };
            Target.DashboardFilterId = dateFilter.Id;
        }
    }
}
