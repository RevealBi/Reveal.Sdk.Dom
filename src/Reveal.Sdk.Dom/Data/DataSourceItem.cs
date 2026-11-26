using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core;
using Reveal.Sdk.Dom.Core.Constants;
using Reveal.Sdk.Dom.Core.Utilities;
using Reveal.Sdk.Dom.Visualizations;
using System;
using System.Collections.Generic;

namespace Reveal.Sdk.Dom.Data
{
    public class DataSourceItem : SchemaType
    {
        public DataSourceItem(string title, DataSource dataSource) : this()
        {
            Initialize(dataSource ?? new DataSource(), title);
        }

        public DataSourceItem()
        {
            SchemaTypeName = SchemaTypeNames.DataSourceItemType;
        }

        private string _id = Guid.NewGuid().ToString();
        public string Id
        {
            get => _id;
            set
            {
                _id = string.IsNullOrEmpty(value) ? Guid.NewGuid().ToString() : value;
                if (ResourceItem != null) //if we are dealing with a resource item, set the id on the resource item to be the same
                    ResourceItem.Id = _id;
            }
        }

        public string Title { get; set; }

        private string _subtitle;
        public string Subtitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                if (ResourceItem != null)
                    ResourceItem.Subtitle = value;
            }
        }

        [JsonProperty]
        internal string DataSourceId { get; set; }

        [JsonProperty]
        internal bool HasTabularData { get; set; } = true;

        [JsonProperty]
        internal bool HasAsset { get; set; }

        /// <summary>
        /// Gets a value indicating whether the data source item is an Xmla data source.
        /// </summary>
        [JsonIgnore]
        public bool IsXmla => !HasTabularData && !HasAsset;

        [JsonProperty]
        internal Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        [JsonProperty]
        internal Dictionary<string, object> Settings { get; set; }

        [JsonProperty]
        internal Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        [JsonProperty]
        internal DataSourceItem ResourceItem { get; set; }

        private List<IField> _fields = new List<IField>();
        [JsonIgnore]
        public List<IField> Fields
        {
            get => _fields;
            set
            {
                _fields = value;
                OnFieldsPropertyChanged(_fields);
            }
        }

        private DataSource _dataSource;
        /// <summary>
        /// The data source for the current DataSourceItem. This is set internally by a data source builder and is only used during the RdashDocumentValidator process.
        /// If this is null, then the DataSourceItem was manually added to the document and the DataSourceId property should be used to find the data source.
        /// </summary>
        [JsonIgnore]
        internal DataSource DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                DataSourceId = _dataSource?.Id;
            }
        }

        /// <summary>
        /// The data source for the ResourceItem. This is set internally by a data source builder and is only used during the RdashDocumentValidator process.
        /// If this is null, then the ResourceItem was manually added to the document and the ResourceItem.DataSourceId property should be used to find the data source.
        /// </summary>
        [JsonIgnore]
        internal DataSource ResourceItemDataSource { get; set; }

        /// <summary>
        /// This property is used to store the join tables for the current DataSourceItem. It is copied to the TabularDataDefinition during the RdashDocumentValidator process.
        /// </summary>
        [JsonIgnore]
        internal List<JoinTable> JoinTables { get; set; } = new List<JoinTable>();

        private void Initialize(DataSource dataSource, string title)
        {
            DataSource = CreateDataSourceInstance(dataSource);
            InitializeDataSource(DataSource, title);
            InitializeDataSourceItem(title);
        }

        protected virtual void InitializeDataSource(DataSource dataSource, string title)
        {
            DataSource = dataSource;
            DataSourceId = DataSource.Id;

            if (string.IsNullOrEmpty(DataSource.Title))
                DataSource.Title = title;
        }

        protected virtual DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return dataSource;
        }

        protected T Create<T>(DataSource dataSource) where T : DataSource, new()
        {
            if (dataSource is T specificDataSource)
                return specificDataSource;

            var newDataSource = new T
            {
                Id = dataSource.Id,
                Title = dataSource.Title,
                Subtitle = dataSource.Subtitle,
            };
            newDataSource.CopySettingsFrom(dataSource);
            return newDataSource;
        }

        protected virtual void InitializeDataSourceItem(string title)
        {
            Title = title;
        }

        protected virtual void OnFieldsPropertyChanged(List<IField> fields) { }

        public void Join(string alias, string leftJoinFieldName, string rightJoinFieldName, DataSourceItem dataSourceItem)
        {
            Join(alias, new List<JoinCondition>
            {
                new JoinCondition(leftJoinFieldName, rightJoinFieldName)
            }, dataSourceItem);
        }

        public void Join(string alias, IEnumerable<JoinCondition> joins, DataSourceItem dataSourceItem)
        {
            if (this is IProcessDataOnServer current)
                current.ProcessDataOnServer = false;

            if (dataSourceItem is IProcessDataOnServer dsi)
                dsi.ProcessDataOnServer = false;

            var additionalTable = new JoinTable(alias, dataSourceItem);
            foreach (var join in joins)
            {
                var formattedLeftFieldName = ValidateLeftJoinFieldName(join.LeftFieldName);
                var formattedRightFieldName = ValidateRightFieldName(join.RightFieldName, alias);

                var joinCondition = new JoinCondition()
                {
                    LeftFieldName = formattedLeftFieldName,
                    RightFieldName = formattedRightFieldName,
                };
                additionalTable.JoinConditions.Add(joinCondition);
            }

            foreach (var field in dataSourceItem.Fields.Clone())
            {
                field.FieldName = $"{alias}.{field.FieldName}";
                field.TableAlias = alias;
                Fields.Add(field);
            }

            JoinTables.Add(additionalTable);
        }

        private string ValidateLeftJoinFieldName(string fieldName)
        {
            // Check if the field name is already enclosed in brackets
            if (fieldName.StartsWith("[") && fieldName.EndsWith("]"))
            {
                return fieldName;
            }

            // Check if the field name matches the format "A.[FieldName]"
            var parts = fieldName.Split('.');
            if (parts.Length == 2 && parts[1].StartsWith("[") && parts[1].EndsWith("]"))
            {
                return fieldName;
            }

            // Check if the field name contains a dot
            if (fieldName.Contains("."))
            {
                // Split the field name by dot
                var formattedFieldName = $"{parts[0]}.[{parts[1]}]";
                return formattedFieldName;
            }
            else
            {
                // Return the field name enclosed in brackets
                return $"[{fieldName}]";
            }
        }

        private string ValidateRightFieldName(string fieldName, string alias)
        {
            // Remove any outer brackets for simplicity
            var trimmedFieldName = fieldName.Trim('[', ']');

            // Split the field name by '.'
            var parts = trimmedFieldName.Split('.');

            // Check if the field name is already in the format "Alias.[FieldName]"
            if (fieldName.StartsWith($"{alias}.[") && fieldName.EndsWith("]"))
            {
                return fieldName;
            }

            // Handle cases where there are two parts
            if (parts.Length == 2)
            {
                // If there are two parts and the first part matches the alias, format correctly
                if (parts[0] == alias)
                {
                    return $"{alias}.[{parts[1]}]";
                }
            }
            // Handle cases where there is only one part (no alias)
            else if (parts.Length == 1)
            {
                return $"{alias}.[{parts[0]}]";
            }

            throw new ArgumentException($"Invalid right field name format: {fieldName}");
        }

        protected void UpdateResourceItemDataSource(DataSource dataSource)
        {
            ResourceItemDataSource.Id = dataSource.Id;
            ResourceItemDataSource.Title = dataSource.Title ?? ResourceItem.Title;
            ResourceItemDataSource.Subtitle = dataSource.Subtitle ?? ResourceItem.Subtitle;
            ResourceItemDataSource.CopySettingsFrom(dataSource);
            ResourceItem.DataSourceId = ResourceItemDataSource.Id;
        }
    }
}
