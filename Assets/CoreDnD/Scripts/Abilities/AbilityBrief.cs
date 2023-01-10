using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DndCore.Ability
{
    public class AbilityBrief
    {
        public Guid Id;
        public string Name;
        public string Description;
        public AbilityActionType ActionType;
        public List<AbilityInputInstruction> Instructions;

        public AbilityBrief()
        {
            Instructions = new List<AbilityInputInstruction>();
        }

        public AbilityBrief(Ability ability)
        {
            Id = ability.Id;
            Name = ability.Name;
            Description = ability.Description;
            ActionType = ability.ActionType;
            Instructions = ability.Instructions;
        }
    }
}
