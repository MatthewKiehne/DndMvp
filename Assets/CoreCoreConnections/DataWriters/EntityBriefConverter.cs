using System;
using System.Collections.Generic;
using DndCore.Ability;
using Entities;
using Entities.Features;
using Mirror;

namespace MirrorConverter
{
    public static class EntityBriefConverter
    {
        public static void WriteEntityBriefConverter(this NetworkWriter writer, EntityBrief entityBrief)
        {
            // replace object with the correct type
            writer.Write<Guid>(entityBrief.Id);

            writer.WriteInt(entityBrief.Features.Count);
            foreach(Feature feature in entityBrief.Features)
            {
                writer.Write<Feature>(feature);
            }

            writer.WriteInt(entityBrief.Abilities.Count);
            foreach(AbilityBrief ability in entityBrief.Abilities)
            {
                writer.Write<AbilityBrief>(ability);
            }
        }

        public static EntityBrief ReadEntityBriefConverter(this NetworkReader reader)
        {
           Guid id = reader.Read<Guid>();

           List<Feature> features = new List<Feature>();
           int featureCount = reader.ReadInt();
           for(int i = 0; i < featureCount; i++)
           {
                features.Add(reader.Read<Feature>());
           }

           List<AbilityBrief> abilities = new List<AbilityBrief>();
           int abilityCount = reader.ReadInt();
           for(int i = 0; i < abilityCount; i++)
           {
                abilities.Add(reader.Read<AbilityBrief>());
           }

            Entity entity = new Entity(id);
            EntityBrief result = new EntityBrief(entity);
            result.Abilities = abilities;
            result.Features = features;

            return result;
        }
    }
}
