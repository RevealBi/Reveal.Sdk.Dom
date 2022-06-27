﻿using Reveal.Sdk.Dom.Data;
using Reveal.Sdk.Dom.Core.Serialization;
using Reveal.Sdk.Dom.Filters;
using Reveal.Sdk.Dom.Visualizations;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Variables;

namespace Reveal.Sdk.Dom
{
    public class DashboardDocument
    {
        public DashboardDocument() : this("New Dashboard") { }

        public DashboardDocument(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        [JsonProperty("ThemeName")]
        public string Theme { get; set; }

        [JsonProperty]
        public string CreatedWith { get; private set; } = GlobalConstants.DashboardDocument.CreatedWith;

        [JsonProperty]
        public string SavedWith { get; internal set; } = string.Empty;

        [JsonProperty]
        public int FormatVersion { get; private set; }
        public bool UseAutoLayout { get; set; } = true;
        public int? AutoRefreshInterval { get; set; }
        public string PasswordHash { get; set; }
        public string Tags { get; set; }

        [JsonProperty]
        public List<DataSource> DataSources { get; internal set; } = new List<DataSource>();

        [JsonProperty("GlobalFilters")]
        public List<DashboardFilter> Filters { get; internal set; } = new List<DashboardFilter>();

        [JsonProperty]
        internal List<GlobalVariable> GlobalVariables { get; set; } = new List<GlobalVariable>();

        [JsonProperty("Widgets")]
        public List<IVisualization> Visualizations { get; internal set; } = new List<IVisualization>();

        public static DashboardDocument Load(string filePath)
        {
            return RdashSerializer.Load(filePath);
        }

        /// <summary>
        /// Saves the DashboardDocument as an RDASH file.
        /// </summary>
        /// <param name="filePath">The file path to save the Dashboard (must include the .rdash extensions)</param>
        public void Save(string filePath)
        {
            RdashSerializer.Save(this, filePath);
        }

        /// <summary>
        /// Converts the DashboardDocument to a JSON string.
        /// </summary>
        /// <returns>A JSON string representing the DashboardDocument</returns>
        public string ToJsonString()
        {
            return RdashSerializer.Serialize(this);
        }
    }
}
