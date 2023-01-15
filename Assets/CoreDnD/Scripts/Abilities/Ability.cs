using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

namespace DndCore.Ability
{
    public class Ability
    {
        public Guid Id;
        public string Name;
        public string Description;
        public AbilityActionType ActionType;

        private Closure GetAbilityInstructionsFunction;
        private Closure ValidateAbilityTargetsFunction;
        private Closure UseAbilityFunction;


        public Ability(string name, string description, AbilityActionType actionType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ActionType = actionType;
        }

        public void SetAbilityInfo(Closure getAbilityInstructionFunction, Closure validateAbilityTargetsFunction, Closure useAbilityFunction)
        {
            GetAbilityInstructionsFunction = getAbilityInstructionFunction;
            ValidateAbilityTargetsFunction = validateAbilityTargetsFunction;
            UseAbilityFunction = useAbilityFunction;
        }

        public List<AbilityInputInstruction> GetAbilityInputInstructions()
        {
            DynValue luaResult = GetAbilityInstructionsFunction.Call();
            return luaResult.ToObject<List<AbilityInputInstruction>>();
        }

        public bool ValidateAbilityTargets(List<AbilityTarget> abilityTargets)
        {
            DynValue luaResult = ValidateAbilityTargetsFunction.Call(abilityTargets);
            return luaResult.Boolean;
        }

        public void UseAbility()
        {
            // figure this part out
            DynValue luaResult = UseAbilityFunction.Call();
        }
    }
}
