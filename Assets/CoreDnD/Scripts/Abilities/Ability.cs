using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DndCore.Ability
{
    public class Ability
    {
        public Guid Id;
        public string Name;
        public string Description;
        public AbilityActionType ActionType;
        public List<AbilityInputInstruction> Instructions;

        public Ability(string name, string description, AbilityActionType actionType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ActionType = actionType;
            Instructions = new List<AbilityInputInstruction>();
        }
    }
}
