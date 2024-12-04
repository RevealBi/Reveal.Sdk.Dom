using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Data
{
    public class GoogleSheetsDataSource : DataSource
    {
        public GoogleSheetsDataSource()
        {
            Id = DataSourceIds.GSHEET;
            Provider = DataSourceProvider.GoogleSheets;
        }
    }
}
