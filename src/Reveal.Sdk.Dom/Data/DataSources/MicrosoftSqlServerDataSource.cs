﻿using Newtonsoft.Json;
using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MicrosoftSqlServerDataSource : SchemaDataSource
    {
        public MicrosoftSqlServerDataSource()
        {
            Provider = DataSourceProvider.MicrosoftSqlServer;
        }

        [JsonIgnore]
        public bool Encrypt
        {
            get => Properties.GetValue<bool>("Encrypt");
            set => Properties.SetItem("Encrypt", value);
        }

        internal static MicrosoftSqlServerDataSource Create(DataSource dataSource)
        {
            return new MicrosoftSqlServerDataSource()
            {
                Id = dataSource.Id,
                Title = dataSource.Title,
                Subtitle = dataSource.Subtitle,
            };
        }
    }
}
