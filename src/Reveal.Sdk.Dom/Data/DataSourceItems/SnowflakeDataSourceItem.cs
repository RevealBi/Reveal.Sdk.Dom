﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class SnowflakeDataSourceItem : SchemaDataSourceItem, IProcessDataOnServer
    {
        public SnowflakeDataSourceItem(string title, SnowflakeDataSource dataSource) :
            base(title, dataSource)
        { }

        public SnowflakeDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<SnowflakeDataSource>(dataSource);
        }
    }
}
