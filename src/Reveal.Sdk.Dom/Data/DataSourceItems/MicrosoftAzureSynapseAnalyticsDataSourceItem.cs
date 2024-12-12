namespace Reveal.Sdk.Dom.Data
{
    public class MicrosoftAzureSynapseAnalyticsDataSourceItem : MicrosoftSqlServerDataSourceItem
    {
        public MicrosoftAzureSynapseAnalyticsDataSourceItem(string title) :
            base(title, new MicrosoftAzureSynapseAnalyticsDataSource())
        { }

        public MicrosoftAzureSynapseAnalyticsDataSourceItem(string title, DataSource dataSource) :
            base(title, dataSource)
        { }

        public MicrosoftAzureSynapseAnalyticsDataSourceItem(string title, MicrosoftAzureSynapseAnalyticsDataSource dataSource)
            : base(title, dataSource) 
        { }

        protected override DataSource CreateDataSourceInstance(DataSource dataSource)
        {
            return Create<MicrosoftAzureSynapseAnalyticsDataSource>(dataSource);
        }
    }
}
