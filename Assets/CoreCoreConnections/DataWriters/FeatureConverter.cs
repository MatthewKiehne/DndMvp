using System;
using System.Collections.Generic;
using Entities.Features;
using Mirror;

namespace MirrorConverter
{
    public static class FeatureConverter
    {
        public static void WriteFeatureConverter(this NetworkWriter writer, Feature feature)
        {
            writer.Write<Guid>(feature.Id);
            writer.WriteString(feature.Name);

            writer.WriteInt(feature.Conditions.Count);
            foreach (Condition condition in feature.Conditions)
            {
                writer.Write<Condition>(condition);
            }
        }

        public static Feature ReadFeatureConverter(this NetworkReader reader)
        {
            Guid id = reader.Read<Guid>();
            string name = reader.ReadString();

            List<Condition> conditions = new List<Condition>();
            int conditionCount = reader.ReadInt();
            for (int i = 0; i < conditionCount; i++)
            {
                conditions.Add(reader.Read<Condition>());
            }

            return new Feature(name)
            {
                Id = id,
                Conditions = conditions
            };
        }
    }
}
