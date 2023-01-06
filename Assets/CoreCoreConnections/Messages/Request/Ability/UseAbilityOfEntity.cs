using DndCore.Ability;
using Mirror;
using System;
using System.Collections.Generic;

namespace Connection.Response
{
    public struct UseAbilityOfEntity : NetworkMessage
    {
        public Guid EntityId;
        public Guid AbilityId;
        public List<AbilityTarget> AbilityTargets;
    }
}
