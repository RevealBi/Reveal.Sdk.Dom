using Newtonsoft.Json;
using System;
using Reveal.Sdk.Dom.Filters;

namespace Reveal.Sdk.Dom.Visualizations
{
    public sealed class DateLinkFilter : LinkFilter
    {
        // Retains the wire defaults when reading legacy link JSON.
        [JsonConstructor]
        internal DateLinkFilter()
        {
            Name = "Date Filter";
            Value = "_date.Date Filter";
            TargetFilterId = "_date";
            Type = LinkFilterType.GlobalFilter;
        }

        /// <summary>Passes the source date filter's selection to a date filter in the linked dashboard.</summary>
        public DateLinkFilter(DashboardDateFilter sourceFilter, DashboardDateFilter targetFilter)
            : this(sourceFilter, (targetFilter ?? throw new ArgumentNullException(nameof(targetFilter))).Id)
        {
            Name = targetFilter.Title;
        }

        /// <summary>Passes the source date filter's selection to the specified filter ID in the linked dashboard.</summary>
        public DateLinkFilter(DashboardDateFilter sourceFilter, string targetFilterId)
        {
            if (sourceFilter == null)
                throw new ArgumentNullException(nameof(sourceFilter));
            if (string.IsNullOrWhiteSpace(sourceFilter.Id))
                throw new ArgumentException("The source filter must have an ID.", nameof(sourceFilter));
            if (string.IsNullOrWhiteSpace(targetFilterId))
                throw new ArgumentException("The target filter ID cannot be empty.", nameof(targetFilterId));

            Name = sourceFilter.Title;
            Value = $"{sourceFilter.Id}.{sourceFilter.Title}";
            TargetFilterId = targetFilterId;
            Type = LinkFilterType.GlobalFilter;
        }
    }
}
