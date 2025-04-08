using System.Linq;

namespace Reveal.Sdk.Dom.Visualizations
{
    public static class IValuesExtensions
    {
        public static T SetValue<T>(this T visualization, string field)
            where T : IValues
        {
            return visualization.SetValue(new NumberDataField(field));
        }

        public static T SetValue<T>(this T visualization, NumberDataField field)
            where T : IValues
        {
            return visualization.SetValues(field);
        }

        public static T SetValues<T>(this T visualization, params string[] fields)
            where T : IValues
        {
            return visualization.SetValues(fields.Select(f => new NumberDataField(f)).ToArray());
        }

        public static T SetValues<T>(this T visualization, params NumberDataField[] fields)
            where T : IValues
        {
            visualization.Values.Clear();
            foreach (var field in fields)
            {
                visualization.Values.Add(new MeasureColumn
                {
                    DataField = field
                });
            }
            return visualization;
        }
    }
}
