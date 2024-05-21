﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class PostgreSqlDataSourceItem : FunctionDataSourceItem
    {
        public PostgreSqlDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        [JsonIgnore]
        public bool ProcessDataOnServer
        {
            get => Properties.GetValue<bool>("ServerAggregation");
            set => Properties.SetItem("ServerAggregation", value);
        }
    }
}
