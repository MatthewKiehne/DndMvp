using System;
using System.Collections.Generic;

namespace DndCore.Ability
{
    public class AbilityPlayerInput
    {
        public Guid SourceEntityId;
        public Guid AbilityId;
        public List<AbilityTarget> Targets;

        public AbilityPlayerInput(Guid sourceEntityId, Guid abilityId)
        {
            SourceEntityId = sourceEntityId;
            AbilityId = abilityId;
            Targets = new List<AbilityTarget>();
        }
    }
}
