namespace Reveal.Sdk.Dom.Data
{
    public class OracleDataSourceItem : ProcedureDataSourceItem
    {
        public OracleDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<OracleDataSource>(dataSource);
        }
    }
}
