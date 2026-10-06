using Reveal.Sdk.Dom.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Reveal.Sdk.Dom.Core.Utilities
{
    internal static class DocumentCompatibility
    {
        private static readonly ConditionalWeakTable<RdashDocument, HashSet<string>> Reported =
            new ConditionalWeakTable<RdashDocument, HashSet<string>>();

        internal static void CheckDateIds(RdashDocument document)
        {
            if (document?.FormatVersion < 7 && document.Filters?.OfType<DashboardDateFilter>().Any(filter =>
                filter.Id != "_date" && filter.Id?.StartsWith("xFiltering_", StringComparison.Ordinal) != true) == true)
                WarnOnce(document, "legacy-date-ids", "RdashCompatibility: This legacy document contains date-filter IDs that an SDK migration may rewrite. The DOM preserves them. If filtering or links behave differently, save with a current Reveal SDK and reload.");
        }

        internal static void CheckImport(RdashDocument target, RdashDocument source)
        {
            if (target.FormatVersion >= 7 && source.FormatVersion < 7)
                WarnOnce(target, "legacy-import", "RdashCompatibility: Importing legacy visualizations into a modern document on a best-effort basis. If rendering, date settings, or links differ, save the source with a current Reveal SDK and reload before importing.");
        }

        private static void WarnOnce(RdashDocument document, string code, string message)
        {
            var reported = Reported.GetOrCreateValue(document);
            lock (reported)
            {
                if (reported.Add(code)) Trace.WriteLine(message, "warn");
            }
        }
    }
}
