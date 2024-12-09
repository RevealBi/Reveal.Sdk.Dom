namespace Reveal.Sdk.Dom.Data
{
    public class MicrosoftAzureSqlServerDataSourceItem : MicrosoftSqlServerDataSourceItem
    {
        public MicrosoftAzureSqlServerDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        public MicrosoftAzureSqlServerDataSourceItem(string title, MicrosoftAzureSqlServerDataSource dataSource) :
            base(title, dataSource)
        { }

        public MicrosoftAzureSqlServerDataSourceItem(string title) :
            base(title, new MicrosoftAzureSqlServerDataSource())
        { }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<MicrosoftAzureSqlServerDataSource>(dataSource);
        }
    }
}
