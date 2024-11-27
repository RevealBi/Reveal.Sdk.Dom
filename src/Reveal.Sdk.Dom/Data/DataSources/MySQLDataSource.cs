using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    internal class MySQLDataSource : ProcessDataSource
    {
        public MySQLDataSource()
        {
            Provider = DataSourceProvider.MySQL;
        }

        public string Username
        {
            get => Properties.GetValue<string>("Username");
            set => Properties.SetItem("Username", value);
        }

        public string Password
        {
            get => Properties.GetValue<string>("Password");
            set => Properties.SetItem("Password", value);
        }

        public int ConnectionTimeout
        {
            get => Properties.GetValue<int>("ConnectionTimeout");
            set => Properties.SetItem("ConnectionTimeout", value);
        }

        public string SslMode
        {
            get => Properties.GetValue<string>("SslMode");
            set => Properties.SetItem("SslMode", value);
        }

        public bool Pooling
        {
            get => Properties.GetValue<bool>("Pooling");
            set => Properties.SetItem("Pooling", value);
        }
    }

}
