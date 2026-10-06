using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Reveal.Sdk.Dom.Core.Utilities
{
    internal static class RdashDocumentValidator
    {
        internal static void Validate(RdashDocument document)
        {
            FixVisualizations(document);
            ReorderDashboardFilters(document);
        }

        static void FixVisualizations(RdashDocument document)
        {
            Dictionary<string, DataSource> dataSources = new Dictionary<string, DataSource>();

            foreach (var visualization in document.Visualizations)
            {
                if (visualization.DataDefinition is TabularDataDefinition tdd)
                {
                    if (tdd.DataSourceItem == null)
                        throw new Exception($"DataSourceItem for visualization {visualization.Title} is null.");

                    FixFields(tdd);
                    FixJoinedTables(tdd);
                    FixDataSources(document, tdd.DataSourceItem, dataSources);
                }
                //todo: handle XmlaDataDefinition
            }

            UpdateDocumentDataSources(document, dataSources);
        }

        private static void FixFields(TabularDataDefinition tdd)
        {
            if (tdd.Fields?.Count == 0)
                throw new Exception($"Fields for DataSourceItem {tdd.DataSourceItem.Title} is null or empty.");

            // Check if there are duplicates
            var hasDuplicates = tdd.Fields.GroupBy(f => f.FieldName).Any(g => g.Count() > 1);
            if (hasDuplicates)
            {
                tdd.Fields = tdd.Fields.GroupBy(f => f.FieldName).Select(g => g.First()).ToList();
            }
        }

        static void FixJoinedTables(TabularDataDefinition tdd)
        {
             // Check if there are duplicates
            var hasDuplicates = tdd.JoinTables.GroupBy(jt => jt.Alias).Any(g => g.Count() > 1);
            if (hasDuplicates)
            {
                tdd.JoinTables = tdd.JoinTables.GroupBy(jt => jt.Alias).Select(g => g.First()).ToList();
            }
        }

        static void FixDataSources(RdashDocument document, DataSourceItem dataSourceItem, Dictionary<string, DataSource> dataSources)
        {
            if (dataSourceItem.DataSource != null)
            {
                if (!dataSources.ContainsKey(dataSourceItem.DataSource.Id))
                    dataSources.Add(dataSourceItem.DataSource.Id, dataSourceItem.DataSource);

                if (dataSourceItem.ResourceItemDataSource != null && !dataSources.ContainsKey(dataSourceItem.ResourceItemDataSource.Id))
                    dataSources.Add(dataSourceItem.ResourceItemDataSource.Id, dataSourceItem.ResourceItemDataSource);
            }
            else
            {
                ValidateManuallyAddedDataSourceItem(document, dataSourceItem);
            }
        }

        static void ValidateManuallyAddedDataSourceItem(RdashDocument document, DataSourceItem dsi)
        {
            var ds = document.DataSources?.FirstOrDefault(x => x.Id == dsi.DataSourceId);
            if (ds == null)
                Trace.WriteLine($"Warning: Data source with id {dsi.DataSourceId} not found in the RdashDocument.DataSources collection.","warn");

            if (dsi.ResourceItem != null)
            {
                var rds = document.DataSources?.FirstOrDefault(x => x.Id == dsi.ResourceItem.DataSourceId);
                if (rds == null)
                    Trace.WriteLine($"Warning: ResourceItem with Data source id {dsi.ResourceItem.DataSourceId} not found in the RdashDocument.DataSources collection.", "warn");
            }
        }

        private static void UpdateDocumentDataSources(RdashDocument document, Dictionary<string, DataSource> dataSources)
        {
            var allDataSources = document.DataSources?.Union(dataSources.Values) ?? dataSources.Values;
            document.DataSources = allDataSources.ToList();
        }

        private static void ReorderDashboardFilters(RdashDocument document)
        {
            //make sure the DashboardDateFilter is the first item in the collection
            var dashboardDateFilter = document.Filters.FirstOrDefault(f => f is DashboardDateFilter);
            if (dashboardDateFilter != null)
            {
                document.Filters.Remove(dashboardDateFilter);
                document.Filters.Insert(0, dashboardDateFilter);
            }
        }
    }
}
