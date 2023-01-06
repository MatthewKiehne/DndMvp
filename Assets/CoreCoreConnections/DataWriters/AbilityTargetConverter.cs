using System;
using System.Collections.Generic;
using DndCore.Ability;
using Mirror;
using UnityEngine;

namespace MirrorConverter
{
    public static class AbilityTargetConverter
    {
        public static void WriteAbilityTargetConverter(this NetworkWriter writer, AbilityTarget abilityTarget)
        {
            writer.WriteInt(abilityTarget.EntityIds.Count);
            foreach (Guid entityId in abilityTarget.EntityIds)
            {
                writer.Write<Guid>(entityId);
            }

            writer.WriteInt((int)abilityTarget.TargetType);
            writer.Write<Vector2Int>(abilityTarget.TargetPosition);
        }

        public static AbilityTarget ReadAbilityTargetConverter(this NetworkReader reader)
        {
            int entityCount = reader.ReadInt();
            List<Guid> entityIds = new List<Guid>();
            for(int i = 0; i < entityCount; i++)
            {
                entityIds.Add(reader.Read<Guid>());
            }

            AbilityTargetType t = (AbilityTargetType)reader.ReadInt();
            Vector2Int targetPos = reader.Read<Vector2Int>();

            AbilityTarget target = new AbilityTarget(t,targetPos);
            target.EntityIds = entityIds;

            return target;
        }
    }
}
