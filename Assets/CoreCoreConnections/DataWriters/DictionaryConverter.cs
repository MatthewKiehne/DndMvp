using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace MirrorConverter
{
    public static class DictionaryConverter
    {
        public static void WriteDictionary<Value>(this NetworkWriter writer, Dictionary<string,Value> dict)
        {
            if(dict == null)
            {
                writer.WriteInt(-1);
                return;
            }

            writer.WriteInt(dict.Count);
            foreach (KeyValuePair<string,Value> item in dict) {
                writer.Write<string>(item.Key);
                writer.Write<Value>(item.Value);
            }
        }

        public static Dictionary<string,Value> ReadDictionary<Value>(this NetworkReader reader)
        {
            int count = reader.ReadInt();
            if(count == -1)
            {
                return null;
            }

            Dictionary<string,Value> result = new Dictionary<string, Value>();
            for( int i = 0; i < count; i++)
            {
                result.Add(reader.Read<string>(), reader.Read<Value>());
            }
            return result;
        }
    }
}
