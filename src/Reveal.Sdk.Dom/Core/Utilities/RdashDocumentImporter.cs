using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reveal.Sdk.Dom.Core.Utilities
{
    internal class RdashDocumentImporter
    {
        public static void Import(RdashDocument targetDocument, RdashDocument sourceDocument, IVisualization visualization = null, ImportOptions options = null)
        {
            if (visualization == null)
            {
                sourceDocument.Visualizations.ForEach(viz => ImportVisualization(targetDocument, sourceDocument, viz, options));
                return;
            }
            
            ImportVisualization(targetDocument, sourceDocument, visualization, options);
        }

        private static void ImportVisualization(RdashDocument targetDocument, RdashDocument sourceDocument, IVisualization viz, ImportOptions options)
        {
            var clonedViz = CloneUtility.Clone(viz);
            clonedViz.Id = Guid.NewGuid().ToString();

            // Process data source item
            if (clonedViz.DataDefinition?.DataSourceItem != null)
            {
                ProcessDataSourceItem(targetDocument, sourceDocument, clonedViz.DataDefinition.DataSourceItem);
            }

            // Process dashboard filters
            if (options?.IncludeDashboardFilters == true)
            {
                ProcessDashboardFilters(targetDocument, sourceDocument, clonedViz);
            }
            else
            {
                clonedViz.FilterBindings = new List<Binding>();
            }

            // Remove visualization-specific filters if required
            if (options == null || options.IncludeVisualizationFilters == false)
            {
                clonedViz.Filters = new List<VisualizationFilter>();
            }

            targetDocument.Visualizations.Add(clonedViz);
        }

        private static void ProcessDataSourceItem(RdashDocument targetDocument, RdashDocument sourceDocument, DataSourceItem dataSourceItem)
        {
            var dataSource = FindAndCloneDataSource(targetDocument, sourceDocument, dataSourceItem.DataSourceId);
            if (dataSource != null)
            {
                targetDocument.DataSources.Add(dataSource);

                if (dataSourceItem.ResourceItem != null)
                {
                    var resourceDataSource = FindAndCloneDataSource(targetDocument, sourceDocument, dataSourceItem.ResourceItem.DataSourceId);
                    if (resourceDataSource != null)
                    {
                        targetDocument.DataSources.Add(resourceDataSource);
                    }
                }
            }
        }

        private static DataSource FindAndCloneDataSource(RdashDocument targetDocument, RdashDocument sourceDocument, string dataSourceId)
        {
            if (string.IsNullOrEmpty(dataSourceId)) return null;

            // Check if the data source already exists in the target document
            if (targetDocument.DataSources.Any(ds => ds.Id == dataSourceId))
            {
                return null;
            }

            // Find the data source in the source document
            var dataSource = sourceDocument.DataSources.FirstOrDefault(ds => ds.Id == dataSourceId);
            return dataSource != null ? CloneUtility.Clone(dataSource) : null;
        }

        private static void ProcessDashboardFilters(RdashDocument targetDocument, RdashDocument sourceDocument, IVisualization clonedViz)
        {
            foreach (var binding in clonedViz.FilterBindings)
            {
                var filterId = GetFilterId(binding);
                if (!string.IsNullOrEmpty(filterId) && !targetDocument.Filters.Any(f => f.Id == filterId))
                {
                    var filter = sourceDocument.Filters.FirstOrDefault(f => f.Id == filterId);
                    if (filter != null)
                    {
                        targetDocument.Filters.Add(CloneUtility.Clone(filter));
                    }
                }
            }
        }

        private static string GetFilterId(Binding binding)
        {
            if (binding is DashboardDateFilterBinding dateBinding)
            {
                return dateBinding.Target?.DashboardFilterId;
            }
            else if (binding is DashboardDataFilterBinding dataBinding)
            {
                return dataBinding.Target?.DashboardFilterId;
            }
            return null;
        }
    }
}
