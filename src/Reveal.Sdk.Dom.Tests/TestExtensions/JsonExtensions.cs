using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reveal.Sdk.Dom.Tests.TestExtensions
{
    public static class JsonExtensions
    {
        public static JToken RemoveProperties(this JToken token, string[] properties)
        {
            JToken result = token.DeepClone();
            if (result is JObject obj)
            {
                foreach (var prop in properties)
                {
                    obj.Remove(prop);
                }

                var children = obj.Properties().ToList();
                foreach (var child in children)
                {
                    child.Value = RemoveProperties(child.Value, properties);
                }
            }
            else if (result is JArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    array[i] = RemoveProperties(array[i], properties);
                }
            }
            return result;
        }
    }
}
