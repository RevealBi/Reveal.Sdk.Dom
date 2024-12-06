using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MySQLDataSource : ProcessDataSource
    {
        public MySQLDataSource()
        {
            Provider = DataSourceProvider.MySQL;
        }
    }
}
