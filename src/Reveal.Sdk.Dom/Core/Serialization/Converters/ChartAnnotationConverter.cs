using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Visualizations;
using Reveal.Sdk.Dom.Visualizations.Settings;
using System;

namespace Reveal.Sdk.Dom.Core.Serialization.Converters
{
    internal sealed class ChartAnnotationConverter : CustomJsonConverter<ChartAnnotation>
    {
        public override ChartAnnotation ReadJson(JsonReader reader, Type objectType, ChartAnnotation existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);
            string annotationType = jObject["AnnotationType"]?.Value<string>();

            ChartAnnotation annotation;
            switch (annotationType)
            {
                case "Slice":
                    annotation = new ChartCategoryAnnotation();
                    break;
                case "Strip":
                    annotation = new ChartCategoryRangeAnnotation();
                    break;
                case "Point":
                    annotation = new ChartDataPointAnnotation();
                    break;
                default:
                    annotation = new ChartDataPointAnnotation();
                    jObject["AnnotationType"] = "Point";
                    break;
            }

            serializer.Populate(jObject.CreateReader(), annotation);
            return annotation;
        }
    }
}
