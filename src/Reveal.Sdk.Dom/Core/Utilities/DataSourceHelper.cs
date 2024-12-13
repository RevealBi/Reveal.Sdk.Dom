using Newtonsoft.Json.Linq;
using Reveal.Sdk.Dom.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reveal.Sdk.Dom.Core.Utilities
{
    public class DataSourceHelper
    {
        public static string GetNonCanonicalUniqueDataSourceIdentifier(DataSourceProvider provider, Dictionary<string, object> properties)
        {
            var s = "provider=" + EnumUtil.GetEnumMemberAttrValue(provider);
            //s = s + "&properties=" + GetNonCanonicalUniqueIdentifierForDictionary(properties);
            return s;
        }

        public static string GetNonCanonicalUniqueIdentifierForDictionary(Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                return string.Empty;
            }

            // Sort the dictionary keys
            var allKeys = dictionary.Keys.ToList();
            allKeys.Sort(StringComparer.Ordinal);

            var s = new StringBuilder();

            foreach (var key in allKeys)
            {
                if (dictionary.TryGetValue(key, out var value) && value != null)
                {
                    if (s.Length > 0)
                    {
                        s.Append("&");
                    }
                    s.Append(key);
                    s.Append("=");
                    s.Append(GetNonCanonicalUniqueIdentifierForObject(value));
                }
            }

            return s.ToString();
        }

        private static string GetNonCanonicalUniqueIdentifierForObject(object o)
        {
            if (o == null)
            {
                return string.Empty;
            }
            else if (o is List<object> valuesList)
            {
                var s = new StringBuilder();

                for (int i = 0; i < valuesList.Count; i++)
                {
                    var listItemValue = GetNonCanonicalUniqueIdentifierForObject(valuesList[i]);

                    s.Append(listItemValue);

                    if (i < valuesList.Count - 1)
                    {
                        s.Append(",");
                    }
                }

                return s.ToString();
            }
            else if (o is Dictionary<string, object> dictionary)
            {
                return GetNonCanonicalUniqueIdentifierForDictionary(dictionary);
            }
            else if (o is JObject jObject)
            {
                return GetNonCanonicalUniqueIdentifierForJsonObject(jObject);
            }
            else
            {
                return o.ToString();
            }
        }

        public static string GetNonCanonicalUniqueIdentifierForJsonObject(JObject json)
        {
            if (json == null)
            {
                return "";
            }
            var s = "";
            var allKeys = json.Properties().Select(p => p.Name).ToList();

            var keyCount = allKeys.Count;
            for (var i = 0; i < keyCount; i++)
            {
                var key = (string)allKeys[i];
                var value = json[key];
                if (value != null)
                {
                    if (i > 0)
                    {
                        s = s + "&";
                    }
                    s = s + key;
                    s = s + "=" + GetNonCanonicalUniqueIdentifierForObject(value);
                }
            }
            return s;
        }
    }
}
