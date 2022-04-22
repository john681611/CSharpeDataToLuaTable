using System.Collections;

namespace LuaTableSerialiser
{
    internal class Serialiser
    {
        internal static object ConvertType(object item, int nesting = 1)
        {
            return item switch
            {
                string value => EscapeString(value),
                int value => value,
                float value => value,
                bool value => value.ToString().ToLower(),
                double value => value,
                IList value => ListToLua(value, nesting),
                IDictionary value => DictToLua(value, nesting),
                _ => TryToLuaString(item)
            };
        }

        private static object TryToLuaString(object item)
        {
            try
            {
                return item.GetType().GetMethod("ToLuaString").Invoke(item, null);
            }
            catch (System.Exception e)
            {

                throw new ArgumentOutOfRangeException($"Not expected value Type value: {item}", e);
            }
            
        }
        
        private static string ConvertKey(object key) => key switch
        {
            string => $"[\"{key}\"]",
            int => $"[{key}]",
            _ => throw new ArgumentOutOfRangeException(nameof(key), $"Not expected key Type value: {key}")
        };
        
        private static string DictToLua<T>(T data, int nesting) where T : IDictionary
        {
            var str = "{";
            foreach (DictionaryEntry item in data)
            {
                if (item.Value is null)
                    continue;
                str += $"\n{Utils.GetNesting(nesting)}{ConvertKey(item.Key)} = {ConvertType(item.Value, nesting + 1)},";
            }
            return $"{str}\n{Utils.GetNesting(nesting)}}}";
        }

        private static string ListToLua<T>(T data, int nesting) where T : IList
        {
            var str = "{";
            var index = 1;
            foreach (var item in data)
            {
                str += $"\n{Utils.GetNesting(nesting)}{ConvertKey(index)} = {ConvertType(item, nesting + 1)},";
                index++;
            }
            return $"{str}\n{Utils.GetNesting(nesting)}}}";
        }

        private static string EscapeString(string data)
        {
            var value = data
                .Replace("\\", "\\\\")
                .Replace("\t","\\t")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\"", "\\\"")
                .Replace("\'", "\\\'")
                .Replace("[", "\\[")
                .Replace("]", "\\]");

            return $"\"{value}\"";
        }
    }
}