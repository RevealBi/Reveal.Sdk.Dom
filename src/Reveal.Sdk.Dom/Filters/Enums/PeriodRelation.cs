namespace Reveal.Sdk.Dom.Filters
{
    // Mirrors the engine's Infragistics.ReportPlus.DashboardModel.PeriodRelation. Member names are
    // serialized verbatim (StringEnumConverter) into a DateRule's "Relation", so they must match the
    // engine spelling exactly.
    public enum PeriodRelation
    {
        /// <summary>All time.</summary>
        All,
        /// <summary>Last [period] = 1 [period] ago until today.</summary>
        Last,
        /// <summary>Previous [period] = beginning of last [period] until end of last [period].</summary>
        Previous,
        /// <summary>[period] to date = beginning of current [period] until today.</summary>
        ToDate,
        /// <summary>This [period] = beginning of current [period] until end of current [period].</summary>
        This,
        /// <summary>Next [period] = beginning of next [period] until end of next [period].</summary>
        Next
    }
}
