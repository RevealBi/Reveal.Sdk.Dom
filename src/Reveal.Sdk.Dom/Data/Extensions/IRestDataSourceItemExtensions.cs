namespace Reveal.Sdk.Dom.Data
{
    public static class IRestDataSourceItemExtensions
    {
        public static T SetUrl<T>(this T dsi, string url) where T : RestDataSourceItem
        {
            dsi.Url = url;
            return dsi;
        }

        public static T SetIsAnonymous<T>(this T dsi, bool isAnonymous) where T : RestDataSourceItem
        {
            dsi.IsAnonymous = isAnonymous;
            return dsi;
        }

        public static T WithExcel<T>(this T dsi, string sheetName) where T : RestDataSourceItem
        {
            dsi.UseExcel(sheetName);
            return dsi;
        }

        public static T WithCsv<T>(this T dsi) where T : RestDataSourceItem
        {
            dsi.UseCsv();
            return dsi;
        }
    }
}
