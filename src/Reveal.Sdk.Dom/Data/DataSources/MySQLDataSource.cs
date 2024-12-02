using Reveal.Sdk.Dom.Core.Extensions;

namespace Reveal.Sdk.Dom.Data
{
    public class MySQLDataSource : ProcessDataSource
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
    }
}
