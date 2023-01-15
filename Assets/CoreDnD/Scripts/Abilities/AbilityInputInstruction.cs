using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DndCore.Ability
{
    public class AbilityInputInstruction
    {
        public AbilityTargetType AbilityTargetType { get; set; }
        public int Range { get; set; }
        public bool Optional { get; set; }
        public List<AbilityInputInstruction> Instructions { get; set; }

        public AbilityInputInstruction()
        {
            Instructions = new List<AbilityInputInstruction>();   
        }

        public AbilityInputInstruction(AbilityTargetType type, int range, bool optional = false)
        {
            AbilityTargetType = type;
            Range = range;
            Optional = optional;
            Instructions = new List<AbilityInputInstruction>();
        }
    }
}
