using System;
using System.Collections.Generic;
using UnityEngine;

namespace DndCore.Ability
{
    public class AbilityTarget
    {
        public List<Guid> EntityIds;
        public AbilityTargetType TargetType;
        public Vector2Int TargetPosition;

        public AbilityTarget()
        {
            EntityIds = new List<Guid>();
        }

        public AbilityTarget(AbilityTargetType targetType, Vector2Int targetPosition)
        {
            EntityIds = new List<Guid>();
            TargetType = targetType;
            TargetPosition = targetPosition;
        }
    }
}
