using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MySqlDataSource : ProcessDataSource
    {
        public MySqlDataSource()
        {
            Provider = DataSourceProvider.MySQL;
        }
    }
}
