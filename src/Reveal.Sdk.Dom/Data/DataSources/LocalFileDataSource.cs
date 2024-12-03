using Reveal.Sdk.Dom.Core.Constants;

namespace Reveal.Sdk.Dom.Data
{
    public class LocalFileDataSource : DataSource
    {
        public LocalFileDataSource()
        {
            Id = DataSourceIds.LOCALFILE;
            Provider = DataSourceProvider.LocalFile;
        }
    }
}
