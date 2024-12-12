namespace Reveal.Sdk.Dom.Data
{
    public class AmazonRedshiftDataSourceItem : FunctionDataSourceItem
    {
        public AmazonRedshiftDataSourceItem(string title) 
            : base(title, new AmazonRedshiftDataSource()) 
        { }

        public AmazonRedshiftDataSourceItem(string title, DataSource dataSource) : 
            base(title, dataSource)
        { }

        public AmazonRedshiftDataSourceItem(string title, AmazonRedshiftDataSource dataSource) :
            base(title, dataSource)
        { }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<AmazonRedshiftDataSource>(dataSource);
        }
    }
}
