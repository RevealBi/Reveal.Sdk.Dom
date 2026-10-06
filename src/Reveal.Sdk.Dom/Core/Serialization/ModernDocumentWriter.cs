using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace Reveal.Sdk.Dom.Core.Serialization
{
    // Complete the wire shapes produced by the DOM builders before writing a modern document.
    // This is deliberately not a general importer for legacy SDK dashboards.
    internal static class ModernDocumentWriter
    {
        internal static void Prepare(JObject document)
        {
            foreach (var widget in (document["Widgets"] as JArray ?? new JArray()).OfType<JObject>())
            {
                var data = widget["DataSpec"] as JObject;
                var spec = widget["VisualizationDataSpec"] as JObject;
                if (data != null)
                {
                    PrepareDateSettings(data["Fields"] as JArray);
                    PrepareDateSettings(data["TransposedFields"] as JArray);
                }
                if (spec != null)
                {
                    PrepareHierarchy(spec, data);
                    PrepareXmlaMembers(spec);
                    PrepareTextFormatting(widget["VisualizationSettings"] as JObject, spec);
                }
            }
        }

        private static void PrepareDateSettings(JArray fields)
        {
            foreach (var field in (fields ?? new JArray()).OfType<JObject>())
            {
                var filter = field["Filter"] as JObject;
                if (field["Settings"] != null || (string)filter?["_type"] != "DateTimeFilterType")
                    continue;
                var month = (int?)filter["DateFiscalYearStartMonth"] ?? 0;
                field["Settings"] = new JObject
                {
                    ["_type"] = "DateTimeFieldSettingsType",
                    ["DateFiscalYearStartMonth"] = month > 1 && month <= 12 ? month : 0,
                    ["DisplayInLocalTimeZone"] = (bool?)filter["DisplayInLocalTimeZone"] ?? false
                };
                if (month > 1 && month <= 12)
                    filter["DateFiscalYearStartMonth"] = 0;
            }
        }

        private static void PrepareHierarchy(JObject spec, JObject data)
        {
            // Format 0 gives a single date label an implicit hierarchy. Write the levels explicitly.
            if (spec["FormatVersion"] == null || (int)spec["FormatVersion"] != 0)
                return;
            spec["FormatVersion"] = 1;
            var rows = spec["Rows"] as JArray;
            var first = rows?.FirstOrDefault() as JObject;
            var date = first?["SummarizationField"] as JObject;
            if ((string)date?["_type"] != "SummarizationDateFieldType" || ((int?)spec["AdHocFields"] ?? 0) > 1)
                return;
            var field = (data?["Fields"] as JArray ?? new JArray()).OfType<JObject>()
                .FirstOrDefault(item => (string)item["FieldName"] == (string)date["FieldName"]);
            if (field == null)
                return;
            var type = (string)field["FieldType"];
            var levels = type == "Date" ? new[] { "Year", "Month", "Day" }
                : type == "Time" ? new[] { "Hour", "Minute" }
                : new[] { "Year", "Month", "Day", "Hour", "Minute" };
            var index = System.Array.IndexOf(levels, (string)date["DateAggregationType"] ?? "Year");
            if (index < 0 || index == levels.Length - 1)
                return;
            var expanded = new JArray(first.DeepClone());
            foreach (var level in levels.Skip(index + 1))
            {
                var column = (JObject)first.DeepClone();
                column["SummarizationField"]["DateAggregationType"] = level;
                expanded.Add(column);
            }
            spec["AdHocFields"] = expanded.Count;
            foreach (var row in rows.Skip(1))
                expanded.Add(row.DeepClone());
            spec["Rows"] = expanded;
        }

        private static void PrepareXmlaMembers(JObject spec)
        {
            foreach (var element in spec.Descendants().OfType<JProperty>()
                .Where(p => p.Name == "XmlaElement").Select(p => p.Value).OfType<JObject>().ToList())
            {
                var oldNames = element["DrillDownElements"] as JArray;
                if (oldNames == null || oldNames.Count == 0)
                    continue;
                var members = element["DrillDownMembers"] as JArray ?? new JArray();
                foreach (var name in oldNames.Values<string>())
                {
                    if (members.OfType<JObject>().Any(member => (string)member["UniqueName"] == name))
                        continue;
                    var match = Regex.Match(name, @"\[((?:[^\]]|\]\])*)\]$");
                    members.Add(new JObject { ["UniqueName"] = name,
                        ["Caption"] = match.Success ? match.Groups[1].Value.Replace("]]", "]") : name });
                }
                element["DrillDownMembers"] = members;
                element["DrillDownElements"] = new JArray();
            }
        }

        private static void PrepareTextFormatting(JObject settings, JObject spec)
        {
            if ((bool?)settings?["SingleValueFormattingEnabled"] != true)
                return;
            var value = (spec["Value"] as JArray)?.FirstOrDefault() as JObject;
            var measure = value?["SummarizationField"] as JObject ?? value?["XmlaMeasure"] as JObject;
            var bands = settings["GaugeBands"] as JArray;
            if (measure == null || measure["ConditionalFormatting"] != null || bands == null)
                return;
            var converted = (JArray)bands.DeepClone();
            foreach (var band in converted.OfType<JObject>())
                band["_type"] = "ConditionalFormattingBandType";
            var formatting = new JObject { ["Bands"] = converted };
            foreach (var bound in new[] { "Minimum", "Maximum" })
                if (settings[bound] != null)
                    formatting[bound] = settings[bound].DeepClone();
            measure["ConditionalFormatting"] = formatting;
        }
    }
}
